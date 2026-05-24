import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { ProductFormComponent } from '../product-form/product-form.component';
@Component({
  selector: 'app-inventory',
  standalone: true,
  imports: [CommonModule, FormsModule, MatCardModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatInputModule, MatSelectModule, MatProgressSpinnerModule, MatDialogModule],
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  loading = true;
  searchTerm = '';
  selectedStatus = 'all';
  constructor(private productService: ProductService, private dialog: MatDialog) {}
  ngOnInit(): void { this.loadProducts(); }
  loadProducts(): void {
    this.productService.getAllProducts().subscribe({
      next: (data) => { this.products = data; this.applyFilters(); this.loading = false; },
      error: () => { this.loading = false; }
    });
  }
  applyFilters(): void {
    this.filteredProducts = this.products.filter(p => {
      const matchSearch = !this.searchTerm || p.brand.toLowerCase().includes(this.searchTerm.toLowerCase()) || p.phoneModel.toLowerCase().includes(this.searchTerm.toLowerCase()) || p.material.toLowerCase().includes(this.searchTerm.toLowerCase());
      const matchStatus = this.selectedStatus === 'all' || (this.selectedStatus === 'critical' && p.stock <= 2) || (this.selectedStatus === 'low' && p.stock > 2 && p.stock <= 10) || (this.selectedStatus === 'normal' && p.stock > 10);
      return matchSearch && matchStatus;
    });
  }
  openProductForm(product?: Product): void {
    const ref = this.dialog.open(ProductFormComponent, { width: '600px', data: product || null });
    ref.afterClosed().subscribe(result => { if (result) this.loadProducts(); });
  }
  deleteProduct(id: number): void {
    if (confirm('Eliminar este producto?')) {
      this.productService.deleteProduct(id).subscribe(() => this.loadProducts());
    }
  }
  getStockClass(stock: number): string {
    if (stock <= 2) return 'critical';
    if (stock <= 10) return 'low';
    return 'normal';
  }
}
