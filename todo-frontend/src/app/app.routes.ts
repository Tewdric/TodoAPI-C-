// src/app/app.routes.ts
import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { TaskListComponent } from './components/task-list/task-list';
import { RegisterComponent } from './components/register/register';
import { authGuard } from './guards/auth-guard'; 

export const routes: Routes = [

    { path: '', redirectTo: '/login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { 
      path: 'tasks', 
      component: TaskListComponent,
      canActivate: [authGuard] 
    }
];