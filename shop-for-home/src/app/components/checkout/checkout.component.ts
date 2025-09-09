import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class CheckoutComponent implements OnInit {

totalCost: number = 0;
cartData: any;
getTotal(): string|number {
throw new Error('Method not implemented.');
}
cartItems: any;

  // Add your checkout logic here

  constructor(private route:ActivatedRoute) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params: { [x: string]: string; }) => {
      if (params['cart']) {
        try {
          this.cartData = JSON.parse(params['cart']);

           this.totalCost = this.cartData.reduce(
        (sum: number, item: any) => sum + item.price * item.quantity,
        0
      );
        } catch (e) {
          console.error('Error parsing cart data', e);
        }
      }
    });

    console.log('Cart in checkout:', this.cartData);
  }






  checkoutData = {
    name: '',
    address: '',
    paymentMethod: ''
  };

  placeOrder() {
    // Validate form
    if (!this.checkoutData.name || !this.checkoutData.address || !this.checkoutData.paymentMethod) {
      Swal.fire('Missing Details', 'Please fill all the required fields.', 'warning');
      return;
    }

    // Show success alert
    Swal.fire({
      title: '🎉 Order Placed!',
      html: `
        <p><strong>Name:</strong> ${this.checkoutData.name}</p>
        <p><strong>Payment:</strong> ${this.checkoutData.paymentMethod.toUpperCase()}</p>
        <p>Your order has been placed successfully.</p>
      `,
      icon: 'success',
      confirmButtonText: 'OK',
      confirmButtonColor: '#28a745'
    }).then(() => {
      // Reset form after success
      this.checkoutData = { name: '', address: '', paymentMethod: '' };
    });
  }
  }

 
