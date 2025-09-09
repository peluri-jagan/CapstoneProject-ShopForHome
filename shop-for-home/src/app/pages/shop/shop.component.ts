import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../../models/product.model';
import { CartService } from '../../services/cart.service';
import { CartComponent } from '../../components/cart/cart.component';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  isLoading = false;
  errorMessage = '';
  
  // Example filter criteria
  filters = {
    categoryId: undefined as number | undefined,
    priceMin: undefined as number | undefined,
    priceMax: undefined as number | undefined,
    ratingMin: undefined as number | undefined
  };
searchTerm: any;
  filteredProducts: Product[] | undefined;

  constructor(private productService: ProductService, private cartService: CartService
      
    
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

 loadProducts(): void {
    this.isLoading = true;
    this.errorMessage = '';

    this.productService.getProducts().subscribe({
      next: (data) => {
        this.products = data;
        this.filteredProducts = data; // initialize filtered list
        this.isLoading = false;
      },
      error: () => {
        this.errorMessage = 'Failed to load products.';
        this.isLoading = false;
      }
    });
  }

  // Watch for searchTerm changes
  ngDoCheck() {
    if (this.searchTerm) {
      this.filteredProducts = this.products.filter(product =>
        product.name.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        (product.description && product.description.toLowerCase().includes(this.searchTerm.toLowerCase()))
      );
    } else {
      this.filteredProducts = this.products;
    }
  }


  // Example method to update filters and reload products
  applyFilters(newFilters: Partial<typeof this.filters>): void {
    this.filters = { ...this.filters, ...newFilters };
    this.loadProducts();
  }

  // Implement add to cart or other actions here
addToCart(product: any): void {
    const token = localStorage.getItem('token') || '';

    // Confirmation popup before adding
    Swal.fire({
      title: 'Are you sure?',
      text: `Do you want to add "${product.name}" to your cart?`,
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Yes, add it!',
      cancelButtonText: 'No, cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        // User clicked "Yes"
        this.cartService.addToCart(product, 1).subscribe({
          next: () => {
            Swal.fire({
              icon: 'success',
              title: 'Added!',
              text: `${product.name} has been added to your cart.`,
              timer: 1500,
              showConfirmButton: false
            });
            console.log('Add to cart clicked for product:', product);
          },
          error: (err) => {
            console.error('Failed to add product to cart', err);
            Swal.fire({
              icon: 'error',
              title: 'Failed',
              text: 'Failed to add to cart. Please login first.'
            });
          }
        });
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        // User clicked "Cancel"
        Swal.fire({
          icon: 'info',
          title: 'Cancelled',
          text: `"${product.name}" was not added to the cart.`,
          timer: 1200,
          showConfirmButton: false
        });
      }
    });
  }

}
