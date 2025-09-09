// Frontend/src/app/components/admin/coupon-management/coupon-management.component.ts
import { Component, OnInit } from '@angular/core';
import { CouponService } from '../../../services/coupon.service';
import { Coupon } from '../../../../models/coupon.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-coupon-management',
  templateUrl: './coupon-management.component.html'
})
export class CouponManagementComponent implements OnInit {
  coupons: Coupon[] = [];
  couponForm: FormGroup;
  successMessage = '';
  errorMessage = '';

  constructor(private couponService: CouponService, private fb: FormBuilder) {
    this.couponForm = this.fb.group({
      code: ['', Validators.required],
      discountAmount: [0, Validators.min(0)],
      discountPercentage: [0, [Validators.min(0), Validators.max(100)]],
      validFrom: ['', Validators.required],
      validTo: ['', Validators.required],
      isActive: [true]
    });
  }

  ngOnInit(): void {
    this.loadCoupons();
  }

  loadCoupons(): void {
    this.couponService.getCoupons().subscribe(data => this.coupons = data);
  }

  onSubmit(): void {
    if (this.couponForm.invalid) return;

    this.couponService.createCoupon(this.couponForm.value).subscribe({
      next: () => {
        this.successMessage = 'Coupon created successfully';
        this.errorMessage = '';
        this.couponForm.reset({ isActive: true });
        this.loadCoupons();
      },
      error: () => {
        this.errorMessage = 'Failed to create coupon';
        this.successMessage = '';
      }
    });
  }
}
