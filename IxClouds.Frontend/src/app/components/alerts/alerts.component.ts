import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-alerts',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatButtonModule, MatProgressSpinnerModule],
  templateUrl: './alerts.component.html',
  styleUrls: ['./alerts.component.css']
})
export class AlertsComponent implements OnInit {
  criticalProducts: Product[] = [];
  lowStockProducts: Product[] = [];
  loading = true;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService.getLowStockProducts().subscribe({
      next: (products) => {
        this.criticalProducts = products.filter(p => p.stockQuantity <= 2);
        this.lowStockProducts = products.filter(p => p.stockQuantity > 2 && p.stockQuantity <= 10);
        this.loading = false;
      },
      error: () => { this.loading = false; }
    });
  }
}