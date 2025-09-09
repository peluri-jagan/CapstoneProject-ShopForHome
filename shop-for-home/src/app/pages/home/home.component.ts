import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../../models/product.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  featured: Product[] = [];
  isLoading = false;
  errorMessage = '';

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.isLoading = true;

    // Load 3 featured products
    this.productService.getProducts().subscribe({
      next: (products) => {
        this.featured = products.slice(0, 3);
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Failed to load featured products';
        this.isLoading = false;
      }
    });
  }
}
