// Frontend/src/app/services/coupon.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Coupon } from '../../models/coupon.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CouponService {
  private apiUrl = 'https://localhost:5001/api/coupons';

  constructor(private http: HttpClient) {}

  getCoupons(): Observable<Coupon[]> {
    return this.http.get<Coupon[]>(this.apiUrl);
  }

  createCoupon(coupon: Coupon): Observable<Coupon> {
    return this.http.post<Coupon>(this.apiUrl, coupon);
  }

  assignCoupon(couponId: number, userIds: number[]): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/assign`, { couponId, userIds });
  }
}
