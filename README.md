# Desafio de Lista de Tarefas (ToDo List) - Full-Stack

Este projeto é uma aplicação web completa (full-stack) desenvolvida como parte de um desafio técnico para uma vaga de Desenvolvedor Júnior. A aplicação permite que usuários se cadastrem, façam login e gerenciem suas próprias listas de tarefas.

---

## 🛠️ Tecnologias Utilizadas

* **Backend:**
    * .NET 8
    * ASP.NET Core Web API
    * Entity Framework Core 8
    * Autenticação com JWT (JSON Web Tokens)
    * Banco de Dados SQLite

* **Frontend:**
    * Angular 17+
    * TypeScript
    * HTML5 / SCSS
    * Angular Router
    * Angular Services & Interceptors

---

## ⚙️ Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina:
* [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) ou superior.
* [Node.js (LTS)](https://nodejs.org/en/) (que inclui o npm).
* [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`).
* Um editor de código como [Visual Studio Code](https://code.visualstudio.com/) ou Visual Studio 2022.

---

## 🚀 Como Executar o Projeto

O projeto é dividido em duas partes: o **backend (API)** e o **frontend (Aplicação Angular)**. Ambos precisam estar rodando simultaneamente.

### 1. Backend (API .NET)

```bash
# 1. Navegue para a pasta da API
cd TodoList

# 2. Restaure as dependências do .NET
dotnet restore

# 3. Aplique as migrações para criar o banco de dados SQLite
dotnet ef database update

# 4. Execute a API
dotnet run
```
✨ A API estará rodando em `http://localhost:5090` (verifique o terminal para a porta exata).

### 2. Frontend (Aplicação Angular)

*Abra um **novo terminal** para executar o frontend.*

```bash
# 1. Navegue para a pasta do frontend
cd todo-frontend

# 2. Instale as dependências do Node.js
npm install

# 3. Execute a aplicação Angular
ng serve
```
✨ A aplicação estará disponível em `http://localhost:4200`.

---

## 🧪 Como Testar

1.  Acesse `http://localhost:4200` no seu navegador.
2.  Você será direcionado para a tela de Login. Como é o primeiro acesso, clique no link **"Cadastre-se"**.
3.  Crie uma nova conta com um e-mail e senha. Após o sucesso, você será redirecionado de volta para a tela de Login.
4.  Faça o login com as credenciais que você acabou de criar.
5.  Você será levado para a página principal, onde poderá:
    * Adicionar novas tarefas.
    * Editar a descrição de tarefas existentes através do modal.
    * Marcar tarefas como concluídas clicando no checkbox.
    * Excluir tarefas.
    * Fazer logout para encerrar a sessão.