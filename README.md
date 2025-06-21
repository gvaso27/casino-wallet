# 💰 Casino Wallet API

This is a .NET 8 Web API project for a **Casino Wallet System**, designed for managing users, checking balances, and performing wallet operations such as adding or withdrawing funds. It connects to a PostgreSQL database and provides Swagger UI for easy testing.

## ✨ Features

* Create users
* Get user details and balance
* Add funds to wallet
* Withdraw funds from wallet
* View user transaction history
* Swagger integration for interactive API documentation

---

## 📋 Prerequisites

* [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download)
* [PostgreSQL](https://www.postgresql.org/download/)
* IDE like [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

---

## ⚙️ Configuration

### 1. PostgreSQL Setup

Create a PostgreSQL database named `wallet` and a user:

```sql
CREATE DATABASE wallet;
CREATE USER dotnetuser WITH PASSWORD 'password';
GRANT ALL PRIVILEGES ON DATABASE wallet TO dotnetuser;
```

## 🛠️ Running the Application

### Back-End:

```bash
cd WalletApi
dotnet restore
dotnet build
dotnet run
```

### Front-End:

```bash
cd Front-End
xdg-open wallet-frontend.html
```

By default, the swagger API will be available at:

```
http://localhost:5216/swagger
```

---

## 🔧 API Endpoints

| Method | Route                      | Description                |
| ------ | -------------------------- | -------------------------- |
| POST   | `/api/user`                | Create a new user          |
| GET    | `/api/user/{id}`           | Get user details           |
| GET    | `/api/user/balance/{id}`   | Get user balance           |
| GET    | `/api/user`                | Get all users              |
| PUT    | `/api/user/add-funds`      | Add funds to a user        |
| PUT    | `/api/user/withdraw-funds` | Withdraw funds from a user |

📌 Swagger UI provides an easy way to test all the endpoints.

---
