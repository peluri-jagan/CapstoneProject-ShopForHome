// Frontend/src/app/services/product.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  create(selectedProduct: Product) {
      throw new Error('Method not implemented.');
  }
  getAll() {
      throw new Error('Method not implemented.');
  }
  private readonly apiUrl = 'http://localhost:5263/api/Products';

  constructor(private http: HttpClient) {}

  getProducts(filters?: { categoryId?: number; priceMin?: number; priceMax?: number; ratingMin?: number }): Observable<Product[]> {
    let params = new HttpParams();
    if (filters) {
      if (filters.categoryId) params = params.set('categoryId', filters.categoryId.toString());
      if (filters.priceMin) params = params.set('priceMin', filters.priceMin.toString());
      if (filters.priceMax) params = params.set('priceMax', filters.priceMax.toString());
      if (filters.ratingMin) params = params.set('ratingMin', filters.ratingMin.toString());
    }
    return this.http.get<Product[]>(this.apiUrl, { params });
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  updateProduct(id: number, product: Product): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}

export type { Product };
