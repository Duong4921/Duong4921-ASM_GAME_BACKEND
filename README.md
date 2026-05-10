# 🎮 Game106 Backend API

> **ASP.NET Core 8** backend for the Game106 project — built as part of FPT University coursework, designed to demonstrate production-ready backend patterns.

[![.NET](https://img.shields.io/badge/.NET-8.0-purple?logo=dotnet)](https://dotnet.microsoft.com/)
[![JWT Auth](https://img.shields.io/badge/Auth-JWT%20Bearer-orange)](https://jwt.io/)
[![EF Core](https://img.shields.io/badge/ORM-EF%20Core-blue)](https://learn.microsoft.com/ef/core/)
[![Swagger](https://img.shields.io/badge/Docs-Swagger%20UI-green)](https://swagger.io/)

---

## ✨ Features

| Feature | Description |
|---|---|
| 🔐 **JWT Authentication** | Secure Bearer token auth with refresh flow |
| 👤 **ASP.NET Identity** | Full user management (register, login, roles) |
| 📧 **Email Service** | SMTP-based OTP & welcome email via Gmail |
| 🖥️ **MVC Admin Panel** | Razor-based web UI for admin management (Lab 7) |
| 🎮 **Game API** | REST endpoints for Unity game client integration |
| 🗄️ **Cloud Database** | SQL Server hosted on Somee.com |
| 📄 **Swagger UI** | Interactive API docs at `/swagger` |

---

## 🚀 Getting Started

### 1. Clone the repo
```bash
git clone https://github.com/Duong4921/ASM_GAME_BACKEND.git
cd ASM_GAME_BACKEND
```

### 2. Configure secrets (⚠️ Never commit real credentials!)

Copy the template and fill in your own values:
```bash
cp appsettings.json appsettings.Development.json
```

Then edit `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your-real-connection-string"
  },
  "MailSettings": {
    "SenderEmail": "your-email@gmail.com",
    "Password": "your-gmail-app-password"
  },
  "Jwt": {
    "Key": "your-super-secret-jwt-key-min-32-chars"
  }
}
```
> `appsettings.Development.json` is **gitignored** — it stays on your machine only.

### 3. Apply database migrations
```bash
dotnet ef database update
```

### 4. Run the app
```bash
dotnet run
```

Open **Swagger UI** at: `https://localhost:{port}/swagger`

---

## 📁 Project Structure

```
Game106.Backend/
├── Controllers/          # API + MVC controllers
│   ├── APIGameController.cs    # Game client REST API
│   ├── HomeController.cs       # MVC Admin login/register
│   ├── GameController.cs       # Game level CRUD
│   └── QuestionController.cs   # Quiz/question management
├── Models/               # Domain entities
├── DTO/                  # Data Transfer Objects
├── ViewModel/            # MVC ViewModels
├── Context/              # EF Core DbContext
├── Services/             # Email, Razor render services
├── Migrations/           # EF Core migration history
├── Views/                # Razor Views (MVC admin)
├── Templates/            # Email HTML templates
├── Lab_Submissions/      # Per-lab test cases & guides
└── appsettings.json      # Config template (no secrets)
```

---

## 🔑 API Endpoints (Key)

| Method | Endpoint | Auth | Description |
|---|---|---|---|
| `POST` | `/api/APIGame/register` | ❌ | Register new user |
| `POST` | `/api/APIGame/login` | ❌ | Login, get JWT token |
| `POST` | `/api/APIGame/forgot-password` | ❌ | Send OTP to email |
| `POST` | `/api/APIGame/verify-otp` | ❌ | Verify OTP code |
| `GET` | `/api/APIGame/profile` | ✅ JWT | Get user profile |
| `PUT` | `/api/APIGame/profile` | ✅ JWT | Update profile |
| `POST` | `/api/APIGame/save-result` | ✅ JWT | Save game level result |

Full interactive docs: **`/swagger`**

---

## 🛡️ Security Notes

- JWT keys and DB credentials are stored via **environment variables** or `appsettings.Development.json` (gitignored).
- Password hashing via **ASP.NET Identity** (PBKDF2).
- HTTPS enforced in production.

---

## 🧪 Lab Submissions

Each lab folder in `Lab_Submissions/` contains:
- `GUIDE.md` — task requirements & implementation notes
- `TestCase_LabX.http` — ready-to-run HTTP test cases (VS Code REST Client)

---

## 📚 Tech Stack

- **Runtime**: .NET 8 / ASP.NET Core
- **ORM**: Entity Framework Core 8
- **Database**: SQL Server (Somee cloud)
- **Auth**: JWT Bearer + ASP.NET Core Identity
- **Email**: MailKit / SMTP
- **Docs**: Swashbuckle (Swagger/OpenAPI)
- **Views**: Razor (MVC)
