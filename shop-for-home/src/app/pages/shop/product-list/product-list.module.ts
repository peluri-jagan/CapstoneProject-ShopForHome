import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from '../../../components/product-list/product-list.component';
import { RouterModule } from '@angular/router';  // <--- Import RouterModule here
@NgModule({
  declarations: [ProductListComponent],
  imports: [
    CommonModule,
    RouterModule  // <--- Add RouterModule here to use routerLink in templates
  ],
  exports: [ProductListComponent]
})
export class ProductListModule { }
     