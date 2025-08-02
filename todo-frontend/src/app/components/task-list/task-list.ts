import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth';
import { TaskService } from '../../services/task';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './task-list.html',
  styleUrl: './task-list.scss'
})
export class TaskListComponent implements OnInit {
  tasks: any[] = [];
  newTaskDescription: string = '';

  isEditModalVisible: boolean = false;
  currentTaskToEdit: any = null;

  constructor(
    private taskService: TaskService,
    private authService: AuthService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getTasks().subscribe({
      next: (data) => {
        this.tasks = data;
        console.log('Tarefas carregadas:', this.tasks);
      },
      error: (err) => {
        console.error('Erro ao carregar tarefas:', err);
        if (err.status === 401) {
          alert('Sua sessão expirou. Por favor, faça login novamente.');
          this.logout();
        }
      }
    });
  }

  addTask(): void {
    if (this.newTaskDescription.trim() === '') {
      return;
    }

    const taskData = { description: this.newTaskDescription };

    this.taskService.createTask(taskData).subscribe({
      next: (newTask) => {
        this.tasks.push(newTask);
        this.newTaskDescription = '';
        console.log('Tarefa criada com sucesso!', newTask);
      },
      error: (err) => {
        console.error('Erro ao criar tarefa:', err);
      }
    });
  }

  deleteTask(taskId: number): void {
    this.taskService.deleteTask(taskId).subscribe({
      next: () => {
        this.tasks = this.tasks.filter(task => task.id !== taskId);
        console.log('Tarefa excluída com sucesso!');
      },
      error: (err) => {
        console.error('Erro ao excluir tarefa:', err);
      }
    });
  }

  toggleTaskStatus(task: any): void {
    const updatedTask = { ...task, isDone: !task.isDone };

    this.taskService.updateTask(task.id, updatedTask).subscribe({
      next: () => {
        task.isDone = updatedTask.isDone;
        console.log('Status da tarefa atualizado!');
      },
      error: (err) => {
        console.error('Erro ao atualizar status da tarefa:', err);
      }
    });
  }

  openEditModal(task: any): void {
    this.currentTaskToEdit = { ...task };
    this.isEditModalVisible = true;
  }

  closeEditModal(): void {
    this.isEditModalVisible = false;
    this.currentTaskToEdit = null;
  }

  saveEdit(): void {
    if (!this.currentTaskToEdit) return;

    this.taskService.updateTask(this.currentTaskToEdit.id, this.currentTaskToEdit).subscribe({
      next: () => {
        const index = this.tasks.findIndex(task => task.id === this.currentTaskToEdit.id);
        if (index !== -1) {
          this.tasks[index] = this.currentTaskToEdit;
        }
        this.closeEditModal();
        console.log('Tarefa atualizada com sucesso!');
      },
      error: (err) => {
        console.error('Erro ao atualizar tarefa:', err);
        alert('Não foi possível salvar as alterações.');
      }
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}