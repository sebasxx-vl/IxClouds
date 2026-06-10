import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Product, CreateProductRequest, UpdateProductRequest, SearchProductRequest } from '../models/product.model';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private apiUrl = `${environment.apiUrl}/products`;

  constructor(private http: HttpClient) {}

  getAllProducts(): Observable<Product[]> {
  return this.http.get<any>(this.apiUrl).pipe(
    map(response => {
      if (Array.isArray(response)) return response;
      if (response?.items) return response.items;
      return [];
    })
  );
}

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  createProduct(product: CreateProductRequest): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  updateProduct(id: number, product: UpdateProductRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  searchProducts(params: SearchProductRequest): Observable<Product[]> {
    return this.http.get<{ items: Product[], totalCount: number }>(`${this.apiUrl}`, { params: params as any }).pipe(
      map(response => response.items)
    );
  }

  getLowStockProducts(): Observable<Product[]> {
    return this.http.get<{ items: Product[], totalCount: number }>(`${this.apiUrl}?lowStockOnly=true`).pipe(
      map(response => response.items)
    );
  }
}