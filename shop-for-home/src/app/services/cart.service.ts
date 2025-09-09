import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CartItem } from '../../models/cart-item.model';
import { Observable } from 'rxjs';

export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  imageUrl: string;
}

@Injectable({
  providedIn: 'root'
})



export class CartService {
  private apiUrl = 'http://localhost:5263/api/Cart';

  constructor(private http: HttpClient) {}

  // Helper to include JWT token in headers
  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('shopforhome_token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  getCartItems(): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(this.apiUrl, { headers: this.getAuthHeaders() });
  }

addToCart(product: any, quantity: number = 1): Observable<any> {
    const payload = {
      productId: product.id,
      quantity: quantity,
      name: product.name,
      description: product.description,
      price: product.price,
      imageUrl: product.imageUrl
    };

    return this.http.post<void>(this.apiUrl, payload, { headers: this.getAuthHeaders() });
  }


  updateCart(productId: number, quantity: number): Observable<void> {
    return this.http.put<void>(this.apiUrl, { productId, quantity }, { headers: this.getAuthHeaders() });
  }

  removeFromCart(productId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${productId}`, { headers: this.getAuthHeaders() });
  }
}
