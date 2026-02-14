# .NET Users CRUD API

A simple ASP.NET Core Web API project implementing basic CRUD operations for managing users.

This project was created to practice backend development with ASP.NET Core and understand routing, controllers, LINQ, and in-memory data handling.

---

## 🚀 Features

The API supports the following operations:

- ✅ Get all users
- ✅ Get user by ID
- ✅ Create a new user
- ✅ Update an existing user
- ✅ Delete a user

All operations are implemented using REST principles.

---

## 🛠 Technologies Used

- .NET 8
- ASP.NET Core Web API
- LINQ
- Swagger (OpenAPI)
- In-memory data storage (`List<User>`)

---

## 📂 Project Structure

UsersApi
│
├── Controllers  
│   └── UsersController.cs  
│
├── Models  
│   └── User.cs  
│
├── Program.cs  
├── appsettings.json  
└── UsersApi.csproj  

- **Controllers** – Contains API endpoints  
- **Models** – Contains data models  
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

- This project uses in-memory storage (`List<User>`) instead of a database.
- Data will be reset every time the application restarts.
- The purpose of this project is educational and focused on backend fundamentals.

---

## 📈 Future Improvements

- Connect to a real database (Entity Framework + SQL Server / PostgreSQL)
- Add validation attributes
- Add logging
- Add DTOs
- Add dependency injection with service layer
- Add authentication (JWT)

---

## 👨‍💻 Author

Rustam
