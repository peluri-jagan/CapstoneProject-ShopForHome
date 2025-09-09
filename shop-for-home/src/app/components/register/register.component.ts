import { Component, ChangeDetectionStrategy } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true, // 👈 standalone component
  imports: [CommonModule, ReactiveFormsModule], // 👈 required modules
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'], // optional but recommended
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegisterComponent {
  registerForm: FormGroup;
  error = '';
  success = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.registerForm.invalid) return;

    const { username, email, password } = this.registerForm.value;
    this.authService.register(username, email, password).subscribe({
      next: () => {
        this.success = 'Registration successful! Please login.';
        this.error = '';
        this.registerForm.reset();
      },
      error: err => {
        this.error = err.error || 'Registration failed';
        this.success = '';
      }
    });
  }
}
