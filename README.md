# .NET Users CRUD API

A simple ASP.NET Core Web API project implementing basic CRUD operations for managing users.

This project was created to practice backend development with ASP.NET Core and understand routing, controllers, LINQ, working with a real database, and model validation.

---

## 🚀 Features

The API supports the following operations:

- ✅ Get all users
- ✅ Get user by ID
- ✅ Create a new user
- ✅ Update an existing user
- ✅ Delete a user

All operations are implemented using REST principles.

Additional behavior:

- ✅ Persisting users in a real database via Entity Framework Core
- ✅ Model validation via data annotations (e.g. `[Required]`, `[EmailAddress]`)
- ✅ Structured logging (`ILogger<UsersController>`) and HTTP request logging in Development (`AddHttpLogging` / `UseHttpLogging` in `Program.cs`)

---

## 🛠 Technologies Used

- .NET 8
- ASP.NET Core Web API
- LINQ
- Swagger (OpenAPI)
- Entity Framework Core
- SQL Server (via connection string `DefaultConnection`)

---

## 📂 Project Structure

UsersApi
│
├── Controllers  
│   └── UsersController.cs  
│
├── Data  
│   └── UsersDbContext.cs  
│
├── Models  
│   └── User.cs  
│
├── Migrations  
│
├── Program.cs  
├── appsettings.json  
├── appsettings.Development.json  
└── UsersApi.csproj  

- **Controllers** – Contains API endpoints  
- **Data** – EF Core `DbContext`  
- **Models** – Contains data models  
- **Migrations** – EF Core database schema migrations  
- **Program.cs** – Application configuration and middleware setup  

---

## 📌 API Endpoints

### Get all users
GET /api/users

### Get user by ID
GET /api/users/{id}

### Create user
POST /api/users

Example request body:

{
  "id": 1,
  "name": "Rustam",
  "email": "test@mail.com"
}

### Update user
PUT /api/users/{id}

### Delete user
DELETE /api/users/{id}

---

## ▶ How to Run the Project

Using .NET CLI:

dotnet restore  
dotnet run  

Swagger UI will be available at:

https://localhost:xxxx/swagger

(Port number may vary.)

---

## 🧠 Notes

- The project now uses a real SQL Server database through Entity Framework Core (see `UsersDbContext` and `DefaultConnection` in `appsettings.json`).
- **Logging:** `UsersController` uses `ILogger<UsersController>` for Information/Warning messages on CRUD operations. In Development, `HttpLoggingMiddleware` logs method, path, status code, and duration for each request (see `Program.cs` and `appsettings.Development.json` for `Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware`).
- Basic model validation is configured using data annotations on the `User` model (for example, required fields and email format).
- The purpose of this project is educational and focused on backend fundamentals.

---

## 📈 Future Improvements

- Add DTOs
- Add dependency injection with service layer
- Add authentication (JWT)

---

## 👨‍💻 Author

Rustam
 