// Frontend/src/app/components/cart/cart.component.ts
import { Component, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { CartItem } from '../../../models/cart-item.model';
import { CurrencyPipe,CommonModule } from '@angular/common';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';
@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  standalone: true,
  styleUrls: ['./cart.component.scss'], 
 imports: [CommonModule,CurrencyPipe], 
})
export class CartComponent implements OnInit {
  cartItems: CartItem[] = [];
  isLoading = false;


  constructor(private cartService: CartService,private router:Router) {}

  ngOnInit(): void {
    this.loadCart();
  }

  loadCart() {
    this.isLoading = true;
    this.cartService.getCartItems().subscribe({
      next: (items) => {
        this.cartItems = items;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }

  increaseQuantity(item: CartItem) {
    this.updateQuantity(item, item.quantity + 1);
  }

  decreaseQuantity(item: CartItem) {
    if (item.quantity > 1) {
      this.updateQuantity(item, item.quantity - 1);
    }
  }

  updateQuantity(item: CartItem, quantity: number) {
    this.cartService.updateCart(item.productId, quantity).subscribe(() => {
      item.quantity = quantity;
    });
  }

  removeItem(productId: number) {
    this.cartService.removeFromCart(productId).subscribe(() => {
      this.cartItems = this.cartItems.filter(i => i.productId !== productId);
    });
  }



  buyNow() {
  if (this.cartItems.length === 0) {
    Swal.fire('Cart Empty', 'Add products to cart first', 'warning');
    return;
  }

  Swal.fire({
    title: 'Proceed to Checkout?',
    text: 'You will be redirected to the checkout page.',
    icon: 'success',
    confirmButtonText: 'OK'  // This is the button user clicks
  }).then((result) => {
    if (result.isConfirmed) {
      // Redirect to checkout after clicking OK
const cartData = JSON.stringify(this.cartItems);

      this.router.navigate(['/checkout'], {
        queryParams: { cart: cartData }
      });

}
  });
}



}
