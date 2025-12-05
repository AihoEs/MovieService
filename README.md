# 🎬 MovieService

A project on AspNetCore with movie search and details. Movie information is taken from the OMDb API. The project uses registration and JWT authentication, Fluent Validation, and Xunit tests. The project is built with a PostgreSQL database.

The project is a typical pet project; in some places, it may be difficult to read, or some elements may be incomplete.

---

## 🚀 Features

- 🔍 Search movies using the **OMDb API**  
- 🔐 Authentication & Authorization via **JWT**  
- 🛠 Middleware logging & timing requests  
- ✅ Input validation with **FluentValidation**  
- 🧪 Unit tests with **xUnit + Moq**  

---

## 💡 Tech Stack

- **.NET 7** / ASP.NET Core  
- **Entity Framework Core** + **PostgreSQL**  
- **JWT Authentication & Identity**  
- **Middleware & Filters**  
- **FluentValidation**  
- **xUnit** & **Moq** for testing  

---

## ⚡ Quickstart

1. Clone the repo:

```bash
git clone https://github.com/AihoEs/MovieService.git
cd MovieService
Set up your .env or appsettings.json with:

json
Копировать код
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=movieDb;Username=postgres;Password=postgres"
  },
  "Api": {
    "Key": "YOUR_OMDB_API_KEY"
  },
  "JwtSettings": {
    "Issuer": "http://localhost:5176",
    "Audience": "http://localhost:5176",
    "Key": "SuperSecretKeyThatYouNeedToReplace"
  }
}
