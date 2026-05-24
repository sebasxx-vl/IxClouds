import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Sale, CreateSaleRequest } from '../models/sale.model';
import { environment } from '../../environments/environment';
@Injectable({ providedIn: 'root' })
export class SaleService {
  private apiUrl = `${environment.apiUrl}/sales`;
  constructor(private http: HttpClient) {}
  createSale(sale: CreateSaleRequest): Observable<Sale> { return this.http.post<Sale>(this.apiUrl, sale); }
  getAllSales(): Observable<Sale[]> { return this.http.get<Sale[]>(this.apiUrl); }
  getSaleById(id: number): Observable<Sale> { return this.http.get<Sale>(`${this.apiUrl}/${id}`); }
}
