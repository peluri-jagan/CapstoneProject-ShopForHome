import { Component, OnDestroy, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CommonModule } from '@angular/common';

import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true, // 👈 important
  imports: [CommonModule, ReactiveFormsModule], // 👈 add forms + common directives
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'], // 👈 optional but recommended
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnDestroy {
  loginForm: FormGroup;
  error = '';
  private subscription: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.error = 'Please fill in all required fields.';
      return;
    }

    const { username, password } = this.loginForm.value;
    this.subscription = this.authService.login(username, password).subscribe({
      next: () => this.router.navigate(['/shop']),
      error: err => {
        this.error = err.error?.message || 'Login failed. Please try again.';
      }
    });
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }
}
