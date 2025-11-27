import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { Product } from '../models/product.model';
import { ProductService } from '../services/product.service';
import { ProductSearch } from '../models/product-search.model';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { map, merge, startWith, switchMap } from 'rxjs';
import { MatSort } from '@angular/material/sort';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { AddProductComponent } from '../add-product/add-product.component';
import { ToastrService } from 'ngx-toastr';
import { EditProductComponent } from '../edit-product/edit-product.component';
import { ConfirmDeleteComponent } from '../confirm-delete/confirm-delete.component';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { AppModule } from '../app.module';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [AppModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements AfterViewInit {//? Angular Hook

  products: Product[];
  columnsToDisplay: string[];
  totalRecordCount: number;
  defaultPageSize: number;
  pageSizeOptions: number[];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  productDataSource: MatTableDataSource<Product>;

  searchText: FormControl;
  isFirstLoad: boolean;
  loading: boolean;

  constructor(
    private productService: ProductService,
    private dialog: MatDialog,
    private toastr: ToastrService,
    private router: Router,
    private userService: UserService) {

    if (!userService.isAuthenticated()) {
      router.navigate(['/login']);
    }

    this.products = [];

    this.columnsToDisplay = [
      'Id',
      'Name',
      'Price',
      'Actions'
    ];

    this.totalRecordCount = 0;
    this.defaultPageSize = 2;
    this.pageSizeOptions = [2, 5, 10, 15];

    this.searchText = new FormControl('');

    this.isFirstLoad = true;

    this.loading = false;
  }

  loadProducts() {
    this.loading = true;
    let pageSize = this.paginator.pageSize;
    let pageIndex = this.paginator.pageIndex + 1;
    let sort = `${this.sort.active} ${this.sort.direction}`;

    let searchData = new ProductSearch(this.searchText.value, pageSize, pageIndex, sort);

    this.productService.searchProducts(searchData)
      .pipe(map(response => {
        this.totalRecordCount = parseInt(response.headers.get('x-totalrecordcount')!);
        return response.body as Product[];
      }))
      .subscribe({
        next: (data) => {
          this.loading = false;
          this.productDataSource = new MatTableDataSource<Product>(data);
          if (this.isFirstLoad) {
            this.toastr.info('Loading Completed', 'Loading');
            this.isFirstLoad = false;
          }
        },
        error: (err) => {
          this.loading = false;
          this.toastr.error('Loading Failed', 'Loading');
        }
      });
  }

  ngAfterViewInit(): void {
    this.loadProducts();

    // this.paginator.page.subscribe(() => {
    //   this.loadProducts();
    // });

    // this.sort.sortChange.subscribe(() => {
    //   this.loadProducts();
    // });

    merge(this.sort.sortChange, this.paginator.page).subscribe(() => {
      this.loadProducts();
    });
  }

  search(event: any) {
    this.loadProducts();
  }

  add() {
    this.dialog.open(AddProductComponent, {
      width: '30%'
    }).afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.loadProducts();
      }
    });
  }

  edit(product: Product) {
    this.dialog.open(EditProductComponent, {
      width: '30%',
      data: product
    }).afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.loadProducts();
      }
    });
  }

  delete(product: Product) {
    this.dialog.open(ConfirmDeleteComponent, {
      width: '30%',
      data: `Are you sure want to delete product (${product.name})?`
    }).afterClosed().subscribe((result: boolean) => {
      if (result) {
        this.productService.deleteProduct(product.name).subscribe({
          next: () => {
            this.loadProducts();
            this.toastr.success('Delete Completed', 'Delete');
          },
          error: (err) => {
            this.toastr.error('Delete Failed', 'Delete');
          }
        })
      }
    });
  }
}
