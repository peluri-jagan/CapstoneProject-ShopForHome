// Frontend/src/app/components/wishlist/wishlist.component.ts
import { Component, OnInit } from '@angular/core';
import { WishlistService } from '../../services/wishlist.service';
import { WishlistItem } from '../../../models/wishlist-item.model';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html'
})
export class WishlistComponent implements OnInit {
  wishlist: WishlistItem[] = [];
  isLoading = false;

  constructor(private wishlistService: WishlistService) {}

  ngOnInit(): void {
    this.loadWishlist();
  }

  loadWishlist() {
    this.isLoading = true;
    this.wishlistService.getWishlist().subscribe({
      next: (items) => {
        this.wishlist = items;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }

  removeFromWishlist(productId: number) {
    this.wishlistService.removeFromWishlist(productId).subscribe(() => {
      this.wishlist = this.wishlist.filter(i => i.productId !== productId);
    });
  }
}
