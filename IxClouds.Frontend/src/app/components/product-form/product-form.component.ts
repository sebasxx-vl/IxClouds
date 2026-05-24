import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {

  form: FormGroup;
  isEdit: boolean;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private dialogRef: MatDialogRef<ProductFormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Product | null
  ) {

    this.isEdit = !!data;

    this.form = this.fb.group({
      brand: ['', [Validators.required, Validators.maxLength(100)]],
      phoneModel: ['', [Validators.required, Validators.maxLength(100)]],
      gender: ['', Validators.maxLength(20)],
      material: ['', Validators.maxLength(50)],
      stock: [0, [Validators.required, Validators.min(0)]],
      purchasePrice: [0, [Validators.required, Validators.min(0.01)]],
      salePrice: [0, [Validators.required, Validators.min(0.01)]],
      imageUrl: ['']
    });
  }

  ngOnInit(): void {
    if (this.isEdit && this.data) {
      this.form.patchValue(this.data);
    }
  }

  onSubmit(): void {

    if (this.form.valid) {

      this.loading = true;

      if (this.isEdit && this.data) {

        this.productService.updateProduct(
          this.data.id,
          {
            ...this.form.value,
            id: this.data.id
          }
        ).subscribe({
          next: () => this.dialogRef.close(true),
          error: () => {
            alert('Error al guardar');
            this.loading = false;
          }
        });

      } else {

        this.productService.createProduct(
          this.form.value
        ).subscribe({
          next: () => this.dialogRef.close(true),
          error: () => {
            alert('Error al guardar');
            this.loading = false;
          }
        });

      }

    }

  }

  onCancel(): void {
    this.dialogRef.close();
  }

}
