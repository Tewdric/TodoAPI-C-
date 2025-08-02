// src/app/services/auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5090';

  constructor(private http: HttpClient) { }

  login(loginData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth/login`, loginData);
  }

  register(registerData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/auth/register`, registerData);
  }
  
  logout(): void {

    localStorage.removeItem('authToken');
  }
}