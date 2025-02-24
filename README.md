# 💰 WalletAPI - API para Gerenciamento de Carteiras Digitais

## 📖 Sobre o Projeto
A **WalletAPI** é uma API RESTful desenvolvida em **C# com .NET 8**, projetada para **gerenciar carteiras digitais e transações financeiras**. 

A API permite que usuários criem contas, consultem saldos, adicionem dinheiro às suas carteiras e realizem transferências para outras contas. Além disso, oferece **autenticação JWT**, **padrões REST**, **persistência com PostgreSQL** e segue as **melhores práticas de SOLID**.

---

## 🚀 **Tecnologias Utilizadas**
- **.NET 8** - Framework principal  
- **Entity Framework Core** - ORM para acesso ao banco  
- **PostgreSQL** - Banco de dados relacional  
- **JWT (JSON Web Token)** - Autenticação segura  
- **FluentValidation** - Validação de entrada  
- **AutoMapper** - Mapeamento entre entidades e DTOs  
- **Docker + Docker Compose** - Containerização  
- **Swagger** - Documentação interativa da API  
- **xUnit + Moq** - Testes unitários e de integração  

---

## 📂 **Arquitetura do Projeto**
```
/WalletAPI
 ├── /Controllers         # Controladores da API
 │   ├── AuthController.cs
 │   ├── WalletController.cs
 │   ├── TransactionController.cs
 ├── /Application         # Regras de negócio
 │   ├── /Services
 │   │   ├── IAuthService.cs
 │   │   ├── AuthService.cs
 │   │   ├── IWalletService.cs
 │   │   ├── WalletService.cs
 │   │   ├── ITransactionService.cs
 │   │   ├── TransactionService.cs
 ├── /Domain             # Modelos e interfaces
 │   ├── /Models
 │   │   ├── User.cs
 │   │   ├── Wallet.cs
 │   │   ├── Transaction.cs
 │   ├── /Interfaces
 │   │   ├── IUserRepository.cs
 │   │   ├── IWalletRepository.cs
 │   │   ├── ITransactionRepository.cs
 ├── /Infrastructure      # Banco de dados e segurança
 │   ├── /Data
 │   │   ├── AppDbContext.cs
 │   ├── /Repositories
 │   │   ├── UserRepository.cs
 │   │   ├── WalletRepository.cs
 │   │   ├── TransactionRepository.cs
 │   ├── /Security
 │   │   ├── JwtService.cs
 ├── /Migrations
 ├── Program.cs
```

## 🔐 Autenticação e Segurança
A autenticação na API é feita utilizando JWT (JSON Web Token), garantindo que apenas usuários autenticados possam acessar determinadas rotas. O token é enviado no header Authorization no formato Bearer <token> em cada requisição protegida.


## 📌 Funcionalidades
### **1️⃣ Autenticação**
Criar um usuário: Permite registrar um novo usuário na plataforma.
Login: Retorna um token JWT para autenticação nas demais operações.
---
### **2️⃣ Gerenciamento de Carteira**
Consultar saldo: Obtém o saldo atual da carteira do usuário.
Adicionar saldo: Permite que um usuário adicione saldo à sua carteira.
---
### **3️⃣ Transações Financeiras**
Criar transferência: Um usuário pode transferir saldo para outra carteira.
Listar transferências: Consulta todas as transações realizadas, com filtro opcional por período de data.

---

## 📜 Requisitos e Execução
### **1️⃣ Pré-requisitos**
.NET 8 SDK instalado
Docker e Docker Compose (se optar por rodar em containers)
Banco de dados PostgreSQL configurado
### **2️⃣ Executando o Projeto**
Clone o repositório e instale as dependências:
```
git clone https://github.com/seu-usuario/WalletAPI.git
cd WalletAPI
dotnet restore
```
**Rodando com Docker:**
```
docker-compose up --build
```
**Rodando localmente (sem Docker):**
```
dotnet restore
dotnet ef database update
dotnet run
```
### **3️⃣ Configurar Banco de Dados**
**Edite o arquivo appsettings.json e configure a string de conexão com o PostgreSQL:**
```
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=wallet_db;Username=postgres;Password=senha"
}
```

### **4️⃣ Popular Banco de Dados**
**Para inserir dados fictícios na base, execute o script:**
```
dotnet run seed
```

## 🖥 API Endpoints
**A documentação da API pode ser acessada via Swagger:**
```
http://localhost:5000/swagger
```
## 🛠 Funcionalidades da API
### **1️⃣ Autenticação**
✔ **Criar um usuário** ```(POST /api/auth/register)```

✔ **Fazer login e obter token JWT** ```(POST /api/auth/login)```

### **2️⃣ Gerenciamento de Carteira**
✔ **Consultar saldo** ``` (GET /api/wallet/balance)```

✔ **Adicionar saldo** ``` (POST /api/wallet/deposit)```

### **3️⃣ Transações Financeiras**
✔ **Criar transferência entre usuários** ``` (POST /api/transaction/send)```

✔ **Listar transferências** ``` (GET /api/transaction/list)```

---

## ✅ Bônus Implementados
✔ **Arquitetura bem definida seguindo SOLID**

✔ **Autenticação JWT para segurança**

✔ **Banco de dados PostgreSQL**

✔ **Docker + Docker Compose para facilitar execução**

✔ **Testes automatizados (xUnit)**

✔ **Linter (StyleCop) para padronização de código**
