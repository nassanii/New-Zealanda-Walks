NZWalks API
Overview

The NZWalks API is a RESTful web service built using ASP.NET Core 8 and Entity Framework Core. It provides endpoints to manage regions, walks, and user authentication for a fictional New Zealand hiking application.

Features

CRUD Operations: Manage regions and walks.

Authentication & Authorization: Implemented using JWT tokens and ASP.NET Core Identity.

Role-Based Access: Differentiates access levels for users.

Data Seeding: Initializes the database with sample data.

Swagger Integration: Provides an interactive API documentation.

Technologies Used

Backend: ASP.NET Core 8

Database: SQL Server

ORM: Entity Framework Core

Authentication: ASP.NET Core Identity, JWT

API Documentation: Swagger

Mapping: AutoMapper

Prerequisites

.NET 8 SDK

SQL Server

Visual Studio 2022 or later

Setup Instructions

Clone the Repository

git clone https://github.com/nassanii/New-Zealanda-Walks.git
cd New-Zealanda-Walks


Restore Dependencies

dotnet restore


Configure Connection Strings

Update the appsettings.json file with your database connection strings:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=NZWalks;Trusted_Connection=True;",
  "AuthConnection": "Server=localhost;Database=Auth_NZWalks;Trusted_Connection=True;"
}


Apply Migrations

dotnet ef database update


Run the Application

dotnet run  


API Documentation

Once the application is running, navigate to https://localhost:5001/swagger to explore and test the API endpoints interactively.

Authentication

Register: POST /api/auth/register

Login: POST /api/auth/login

Use the obtained JWT token to access protected routes by including it in the Authorization header as a Bearer token.

Seeding Data

Upon application startup, the database is seeded with sample regions and walks data. Ensure that the DbInitializer class is invoked during the application's startup process.

Contributing

Contributions are welcome! Please fork the repository, create a new branch, and submit a pull request with your proposed changes.

License

This project is licensed under the MIT License.
