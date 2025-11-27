import { AfterViewInit, Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../services/product.service';
import { Product } from '../models/product.model';
import { ToastrService } from 'ngx-toastr';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AppModule } from '../app.module';

@Component({
  selector: 'app-edit-product',
  standalone: true,
  imports:[AppModule],
  templateUrl: './edit-product.component.html',
  styleUrl: './edit-product.component.scss'
})
export class EditProductComponent implements AfterViewInit {
  productForm: FormGroup;
  isLoading: boolean;

  constructor(
    private productService: ProductService,
    private toastr: ToastrService,
    private dialogRef: MatDialogRef<EditProductComponent>,
    @Inject(MAT_DIALOG_DATA) public product: Product) {

    this.productForm = new FormGroup({
      id: new FormControl(''),
      name: new FormControl('', [Validators.required, Validators.minLength(3)]),
      price: new FormControl('', [Validators.required, Validators.pattern('\\d+')]),
      description: new FormControl(''),
    });

    this.isLoading = true;
  }

  ngAfterViewInit(): void {
    //? Development Env.
    setTimeout(() => {
      this.productService.getProductById(this.product.id).subscribe({
        next: (product: Product) => {
          this.isLoading = false;
          this.productForm.setValue(product);
        },
        error: (error: any) => {
          this.toastr.error('Loading Product data is failed', 'Edit');
        }
      });
    }, 2000);

    //? Production
    // this.productService.getProductById(this.productId).subscribe({
    //   next: (product: Product) => {
    //     this.isLoading = false;
    //     this.productForm.setValue(product);
    //   },
    //   error: (error: any) => {
    //     this.toastr.error('Loading Product data is failed', 'Edit');
    //   }
    // });
  }

  save() {
    if (this.productForm.invalid) {
      console.log(this.productForm.value);
      return;
    }

    //? Call API 
    const product = this.productForm.value as Product;

    this.productService.updateProduct(this.product.id, product).subscribe({
      next: () => {
        this.toastr.success('Saving Completed', 'Update');
        this.dialogRef.close(true);
      },
      error: (error: any) => {
        this.toastr.error('Saving Failed', 'Update');
      }
    });
  }

  cancel() {
    console.log('canel');
    this.dialogRef.close(false);
  }

  //? Computed
  get name(): FormControl {
    return this.productForm.get('name') as FormControl;
  }

  getNameError(): string {
    if (this.name.hasError('required')) {
      return 'Name is a required field';
    }

    if (this.name.hasError('minlength')) {
      return 'Name is a required field';
    }

    return 'other error message';
  }

}
