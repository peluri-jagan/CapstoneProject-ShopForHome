import { Component, OnInit } from '@angular/core';
import { RouterModule,Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'shop-for-home';
  isLoggedIn = false;
  role: string | null = null;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.isLoggedIn$.subscribe(status => {
      this.isLoggedIn = status;
      this.role = this.authService.getUserRole();
    });
  }

  logout(): void {
    this.authService.logout();
     this.router.navigate(['/login']);
  }
}
