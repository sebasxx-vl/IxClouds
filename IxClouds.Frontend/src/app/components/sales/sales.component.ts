import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { SaleService } from '../../services/sale.service';
import { Sale } from '../../models/sale.model';
import { SaleFormComponent } from '../sale-form/sale-form.component';
@Component({
  selector: 'app-sales',
  standalone: true,
  imports: [CommonModule, FormsModule, MatTableModule, MatButtonModule, MatIconModule, MatFormFieldModule, MatInputModule, MatProgressSpinnerModule, MatDialogModule],
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.css']
})
export class SalesComponent implements OnInit {
  sales: Sale[] = [];
  filteredSales: Sale[] = [];
  loading = true;
  searchTerm = '';
  displayedColumns = ['id', 'date', 'items', 'total', 'actions'];
  constructor(private saleService: SaleService, private dialog: MatDialog) {}
  ngOnInit(): void { this.loadSales(); }
  loadSales(): void {
    this.saleService.getAllSales().subscribe({
      next: (data) => { this.sales = data; this.filteredSales = data; this.loading = false; },
      error: () => { this.loading = false; }
    });
  }
  applyFilters(): void { this.filteredSales = this.sales.filter(s => s.id.toString().includes(this.searchTerm)); }
  openSaleForm(): void {
    const ref = this.dialog.open(SaleFormComponent, { width: '700px', maxHeight: '90vh' });
    ref.afterClosed().subscribe(result => { if (result) this.loadSales(); });
  }
  formatDate(date: Date): string { return new Date(date).toLocaleString('es-CO'); }

  viewSale(sale:Sale): void {
    console.log(sale);
  }

  get grandTotal(): number {
    return this.filteredSales.reduce((sum, s) => sum + s.total, 0);
  }
}
