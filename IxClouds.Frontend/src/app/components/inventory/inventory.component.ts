// inventory.component.ts — reemplaza el tuyo
import { Component, OnInit } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { ProductFormComponent } from '../product-form/product-form.component';

@Component({
  selector: 'app-inventory',
  standalone: true,
  imports: [
    CommonModule, CurrencyPipe, FormsModule,
    MatButtonModule, MatIconModule, MatProgressSpinnerModule, MatDialogModule
  ],
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  loading = true;
  searchTerm = '';
  selectedStatus = 'all';

  statusTabs = [
    { label: 'Todos',             value: 'all' },
    { label: 'Normal',            value: 'normal' },
    { label: 'Stock bajo',        value: 'low' },
    { label: 'Crítico',           value: 'critical' },
  ];

  constructor(
    private productService: ProductService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void { this.loadProducts(); }

  loadProducts(): void {
    this.loading = true;
    this.productService.getAllProducts().subscribe({
      next: (data) => { this.products = data; this.applyFilters(); this.loading = false; },
      error: () => { this.loading = false; }
    });
  }

  setStatus(value: string): void {
    this.selectedStatus = value;
    this.applyFilters();
  }

  applyFilters(): void {
    this.filteredProducts = this.products.filter(p => {
      const term = this.searchTerm.toLowerCase();
      const matchSearch = !term ||
        p.brand.toLowerCase().includes(term) ||
        p.phoneModel.toLowerCase().includes(term) ||
        p.material.toLowerCase().includes(term);

      const matchStatus =
        this.selectedStatus === 'all' ||
        (this.selectedStatus === 'critical' && p.stock <= 2) ||
        (this.selectedStatus === 'low'      && p.stock > 2 && p.stock <= 10) ||
        (this.selectedStatus === 'normal'   && p.stock > 10);

      return matchSearch && matchStatus;
    });
  }

  openProductForm(product?: Product): void {
    const ref = this.dialog.open(ProductFormComponent, {
      width: '600px',
      maxHeight: '90vh',
      data: product || null
    });
    ref.afterClosed().subscribe(result => { if (result) this.loadProducts(); });
  }

  deleteProduct(id: number): void {
    if (confirm('¿Eliminar este producto? Esta acción no se puede deshacer.')) {
      this.productService.deleteProduct(id).subscribe(() => this.loadProducts());
    }
  }

  onImgError(event: Event): void {
    (event.target as HTMLImageElement).src = 'assets/default-product.png';
  }

  getStockClass(stock: number): string {
    if (stock <= 2) return 'critical';
    if (stock <= 10) return 'low';
    return 'normal';
  }
}