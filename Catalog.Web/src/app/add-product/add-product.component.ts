import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../services/product.service';
import { Product } from '../models/product.model';
import { AppModule } from '../app.module';
import { MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-product',
  standalone: true,
  imports:[AppModule],
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.scss'
})
export class AddProductComponent {
  productForm: FormGroup;

  constructor(
    private productService: ProductService,
    private toastr: ToastrService,
    private dialogRef: MatDialogRef<AddProductComponent>) {

    this.productForm = new FormGroup({
      name: new FormControl('', [Validators.required, Validators.minLength(3)]),
      price: new FormControl('', [Validators.required, Validators.pattern('\\d+')]),
      description: new FormControl(''),
    });
  }

  save() {
    if (this.productForm.invalid) {
      console.log(this.productForm.value);
      return;
    }

    //? Call API 
    const product = this.productForm.value as Product;
    // this.productService.addProduct(product).subscribe(data => {
    //   console.log(data);
    // });
    this.productService.addProduct(product).subscribe({
      next: (product: Product) => {
        this.toastr.success('Saving Completed', 'AddNew');
        this.dialogRef.close(true);
      },
      error: (error: any) => {
        this.toastr.error('Saving Failed', 'AddNew');
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
