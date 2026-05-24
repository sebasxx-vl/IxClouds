import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, MatListModule, MatIconModule],
  template: `
    <mat-nav-list>
      <a mat-list-item routerLink="/dashboard" routerLinkActive="active-link"><mat-icon matListItemIcon>dashboard</mat-icon><span matListItemTitle>Dashboard</span></a>
      <a mat-list-item routerLink="/inventory" routerLinkActive="active-link"><mat-icon matListItemIcon>inventory</mat-icon><span matListItemTitle>Inventario</span></a>
      <a mat-list-item routerLink="/sales" routerLinkActive="active-link"><mat-icon matListItemIcon>point_of_sale</mat-icon><span matListItemTitle>Ventas</span></a>
      <a mat-list-item routerLink="/alerts" routerLinkActive="active-link"><mat-icon matListItemIcon>notifications</mat-icon><span matListItemTitle>Alertas de Stock</span></a>
    </mat-nav-list>
  `,
  styles: [`.active-link { background-color: rgba(0,0,0,0.08); }`]
})
export class SidebarComponent {}
