// src/app/components/admin/manage-products/manage-products.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  stockQuantity: number;
  rating: number;
  imageUrl: string;
  categoryId: number;
}

@Component({
  selector: 'app-manage-products',
  templateUrl: './manage-products.component.html',
  styleUrls: ['./manage-products.component.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class ManageProductsComponent implements OnInit {
createProduct() {
throw new Error('Method not implemented.');
}
cancelEdit() {
throw new Error('Method not implemented.');
}
  products: Product[] = [];
  
  apiUrl = 'http://localhost:5263/api/products';
message: any;
    selectedProduct: Product | null = null;         
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts(): void {
    this.http.get<Product[]>(this.apiUrl).subscribe((data) => {
      this.products = data;
    });
  }

  editProduct(product: Product): void {
    this.selectedProduct = { ...product };
  }

  updateProduct(): void {
    if (!this.selectedProduct) return;

    this.http
      .put(`${this.apiUrl}/${this.selectedProduct.id}`, this.selectedProduct)
      .subscribe(() => {
        this.getProducts();
        this.selectedProduct = null;
      });
  }

  deleteProduct(id: number): void {
    this.http.delete(`${this.apiUrl}/${id}`).subscribe(() => {
      this.getProducts();
    });
    
  }
 selectProduct(product?: Product): void {
    this.selectedProduct = product
      ? { ...product } // edit existing product
      : { id: 0, name: '', description: '', price: 0, stockQuantity: 0, rating: 0, imageUrl: '', categoryId: 0 }; // new product
  }


}
