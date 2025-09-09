// Frontend/src/app/models/product.model.ts

export interface Product {
  id: number;
  name: string;
  description?: string;
  price: number;
  stockQuantity: number;
  rating: number;
  imageUrl?: string;
  categoryId: number;
}
