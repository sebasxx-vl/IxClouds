import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { SaleService } from '../../services/sale.service';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-sale-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatDialogModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule],
  templateUrl: './sale-form.component.html',
  styleUrls: ['./sale-form.component.css']
})
export class SaleFormComponent implements OnInit {
  form: FormGroup;
  products: Product[] = [];
  submitting = false;

  constructor(
    private fb: FormBuilder,
    private saleService: SaleService,
    private productService: ProductService,
    private dialogRef: MatDialogRef<SaleFormComponent>
  ) {
    this.form = this.fb.group({ items: this.fb.array([]) });
  }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe({
      next: (data) => { this.products = data.filter(p => p.stockQuantity > 0); this.addItem(); },
      error: () => alert('Error al cargar productos')
    });
  }

  get items(): FormArray { return this.form.get('items') as FormArray; }

  addItem(): void {
    this.items.push(this.fb.group({
      productId: ['', Validators.required],
      quantity: [1, [Validators.required, Validators.min(1)]]
    }));
  }

  removeItem(i: number): void { this.items.removeAt(i); }

  getSubtotal(): number {
    return this.items.controls.reduce((sum, item) => {
      const product = this.products.find(p => p.id === item.get('productId')?.value);
      return sum + (product ? product.price * (item.get('quantity')?.value || 0) : 0);
    }, 0);
  }

  onSubmit(): void {
    if (this.form.valid && this.items.length > 0) {
      this.submitting = true;
      this.saleService.createSale({ items: this.items.value }).subscribe({
        next: (sale) => this.dialogRef.close(sale),
        error: () => { alert('Error al registrar venta'); this.submitting = false; }
      });
    }
  }

  onCancel(): void { this.dialogRef.close(); }
}