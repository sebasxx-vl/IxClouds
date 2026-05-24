// sidebar.component.ts — reemplaza el tuyo completo
import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, MatIconModule],
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  alertCount = 0;

  navItems = [
    { label: 'Dashboard',   icon: 'dashboard',       route: '/dashboard' },
    { label: 'Inventario',  icon: 'inventory_2',      route: '/inventory' },
    { label: 'Ventas',      icon: 'receipt_long',     route: '/sales' },
    { label: 'Alertas',     icon: 'notifications',    route: '/alerts',   badge: true },
  ];

  reportItems = [
    { label: 'Estadísticas', icon: 'bar_chart',   route: '/estadisticas' },
    { label: 'Exportar',     icon: 'file_download', route: '/reportes' },
  ];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService.getLowStockProducts().subscribe({
      next: (products) => { this.alertCount = products.length; },
      error: () => {}
    });
  }
}