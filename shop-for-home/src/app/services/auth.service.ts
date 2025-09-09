// Frontend/src/app/services/auth.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';

interface LoginResponse {
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl = 'http://localhost:5263/api/Auth';
  private tokenKey = 'shopforhome_token';
  private _isLoggedIn = new BehaviorSubject<boolean>(!!localStorage.getItem(this.tokenKey));

  isLoggedIn$ = this._isLoggedIn.asObservable();

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, { username, password })
      .pipe(tap(res => {
        localStorage.setItem(this.tokenKey, res.token);
        this._isLoggedIn.next(true);
      }));
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this._isLoggedIn.next(false);
  }

  register(username: string, email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, { username, email, password });
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
  getUserRole(): string | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const payload = JSON.parse(atob(token.split('.')[1])); // decode JWT payload
      return payload.role || payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null;
    } catch (e) {
      console.error('Error decoding token:', e);
      return null;
    }
  }
}
