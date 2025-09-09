// Frontend/src/app/models/sales-report.model.ts
export interface SalesReport {
  totalOrders: number;
  totalRevenue: number;
  orders: OrderSummary[];
}

export interface OrderSummary {
  orderId: number;
  username: string;
  totalAmount: number;
  orderDate: string;
}
