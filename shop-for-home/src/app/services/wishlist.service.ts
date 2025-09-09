// Frontend/src/app/services/wishlist.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { WishlistItem } from '../../models/wishlist-item.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private apiUrl = 'https://localhost:5001/api/wishlist';

  constructor(private http: HttpClient) {}

  getWishlist(): Observable<WishlistItem[]> {
    return this.http.get<WishlistItem[]>(this.apiUrl);
  }

  addToWishlist(productId: number): Observable<void> {
    return this.http.post<void>(this.apiUrl, { productId });
  }

  removeFromWishlist(productId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${productId}`);
  }
}
