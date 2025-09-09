export interface CartItem {
  productId: number;
  name: string;
  description: string;
  price: number;
  quantity: number;
  imageUrl?: string; // optional property
}
