import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopRoutingModule } from './shop-routing.module';
import { ShopComponent } from './shop.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';   // ✅ Import FormsModule

@NgModule({
  declarations: [ShopComponent],
  imports: [
    FormsModule,
    CommonModule,
    ShopRoutingModule,
    HttpClientModule  // For API calls
  ]
})
export class ShopModule { }
