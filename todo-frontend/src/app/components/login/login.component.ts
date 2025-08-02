import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    FormsModule, 
    CommonModule,
    RouterLink
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})

export class LoginComponent {
  loginData = { email: '', password: '' };

  // Injetamos o Router aqui
  constructor(private authService: AuthService, private router: Router) {}

  onSubmit(): void {
    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        
        localStorage.setItem('authToken', response.token);

        // NAVEGA PARA A PÁGINA DE TAREFAS
        this.router.navigate(['/tasks']); 
      },
      error: (err) => {
        console.error('Erro no login:', err);
        alert('Erro no login: ' + err.error);
      }
    });
  }
}