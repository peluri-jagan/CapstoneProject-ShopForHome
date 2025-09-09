import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { CartComponent } from './components/cart/cart.component';
import { HomeComponent } from './pages/home/home.component';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { BulkUploadComponent } from './components/admin/bulk-upload/bulk-upload.component';
import { SalesReportComponent } from './components/admin/sales-report/sales-report.component';
import { ManageProductsComponent } from './components/admin/manage-products/manage-products.component';
export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' }, 
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'shop',
    loadChildren: () => import('./pages/shop/shop.module').then(m => m.ShopModule),
    // add guards as needed
  },
   { path: 'cart', component: CartComponent },
   {path: 'checkout', component: CheckoutComponent},
   {path: 'admin/bulk-upload', component: BulkUploadComponent},
   {path: 'admin/sales-report', component: SalesReportComponent},
   {path: 'admin/manage-products', component: ManageProductsComponent},
  { path: '**', redirectTo: '' },
 
];
