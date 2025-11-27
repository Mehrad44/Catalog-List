import { AfterViewInit, Component, ViewChild, viewChild } from '@angular/core';
import { AppModule } from '../app.module';
import { Product } from '../models/product.model';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ProductService } from '../services/ProductService';
import { ProductSearchRequest } from '../models/product-search-request';
import { FormControl } from '@angular/forms';
import { CdkVirtualScrollableElement } from "@angular/cdk/scrolling";
import { MatPaginator } from '@angular/material/paginator';
import { map } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { A11yModule } from "@angular/cdk/a11y";
import { AddProduct } from '../add-product/add-product';

@Component({
  selector: 'app-product-list',
  imports: [AppModule, A11yModule],
  templateUrl: './product-list.html',
  styleUrl: './product-list.scss'
})
export class ProductList implements AfterViewInit {
  productDataSource : MatTableDataSource<Product>;
  columnsToDisplay : string[] ;
 @ViewChild(MatSort)
  sort: MatSort;
  @ViewChild(MatPaginator) paginator : MatPaginator;
  pageSizeOptions : number[];
  defaultPageSize : number ;
  totalRecordCount : number;
  serachText: FormControl;
  loading: boolean;
  private totalRecordCountHeaderName : string = 'x-totalrecordcount';
  isFirstLoad : boolean;



  constructor(private productService: ProductService,
    private toastrService: ToastrService,
    private dialogService : MatDialog,
  ){
    // let data: Product[] =
    // [
    //   new Product("2","tablet",200,"sample tabled")
    // ];
    // let data = ProductService.search();
    // this.productDataSource = new MatTableDataSource(data);
    let data : Product[] = [];
    this.productDataSource = new MatTableDataSource(data);


    this.columnsToDisplay = ['Id' , 'Name' , 'Price' , 'Actions'];

    this.productDataSource.sort = this.sort;

    this.pageSizeOptions =  [2,5,10,15,20];

    this.defaultPageSize = 2;

    this.totalRecordCount = 0; 




    this.serachText = new FormControl('');

    this.loading = false;

    this.isFirstLoad = true;

 


  }
  ngAfterViewInit(): void {
        this.loeadProduct();

        this.sort.sortChange.subscribe(()=>{
          this.loeadProduct();
        });
        this.paginator.page.subscribe(()=>{
          this.loeadProduct();
        });
  }

  loeadProduct(){
    this.loading = true;
    var text = this.serachText.value;
     
    var sort = `${this.sort.active} , ${this.sort.direction}`;

    var pageSize = this.paginator.pageSize;
    let pageIndex = this.paginator.pageIndex + 1 ;


         var request = new ProductSearchRequest(text,sort,3,1);
   this.productService.search(request).pipe(map(response =>{
    this.totalRecordCount = parseInt( response.headers.get(this.totalRecordCountHeaderName)!);
    console.log( response.headers.get(this.totalRecordCountHeaderName));
    return response.body as Product[] ;
   }))
   .subscribe({
      next: (data : Product[]) =>{
        console.log(data);
        this.productDataSource = new MatTableDataSource(data);
        this.loading = false;
        if(this.isFirstLoad){
                      this.toastrService.info('Loading Completed','Loading');

                      this.isFirstLoad = false;

        }

      },
      error : (err : any)=>{
        console.log(err);
        this.loading = false;
            this.toastrService.info('Loading Completed','Failed');

      }
    });

  }

  search(){
    this.loeadProduct();

    this.sort.sortChange.subscribe(()=>{

    });
  }

  add(){
    this.dialogService.open(AddProduct , {
      width: '30%',
      
    });
  }
}
