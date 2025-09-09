// Frontend/src/app/models/coupon.model.ts
export interface Coupon {
  id?: number;
  code: string;
  discountAmount: number;
  discountPercentage: number;
  validFrom: string;  // ISO date string
  validTo: string;    // ISO date string
  isActive: boolean;
}
