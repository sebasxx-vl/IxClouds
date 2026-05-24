import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product, CreateProductRequest, UpdateProductRequest, SearchProductRequest } from '../models/product.model';
import { environment } from '../../environments/environment';
@Injectable({ providedIn: 'root' })
export class ProductService {
  private apiUrl = `${environment.apiUrl}/products`;
  constructor(private http: HttpClient) {}
  getAllProducts(): Observable<Product[]> { return this.http.get<Product[]>(this.apiUrl); }
  getProductById(id: number): Observable<Product> { return this.http.get<Product>(`${this.apiUrl}/${id}`); }
  createProduct(product: CreateProductRequest): Observable<Product> { return this.http.post<Product>(this.apiUrl, product); }
  updateProduct(id: number, product: UpdateProductRequest): Observable<void> { return this.http.put<void>(`${this.apiUrl}/${id}`, product); }
  deleteProduct(id: number): Observable<void> { return this.http.delete<void>(`${this.apiUrl}/${id}`); }
  searchProducts(params: SearchProductRequest): Observable<Product[]> { return this.http.get<Product[]>(`${this.apiUrl}/search`, { params: params as any }); }
  getLowStockProducts(): Observable<Product[]> { return this.http.get<Product[]>(`${this.apiUrl}/low-stock`); }
}
