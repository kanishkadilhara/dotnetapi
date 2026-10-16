# EmployeeAdmin API

EmployeeAdmin is a learning project for understanding how to build a REST API with
ASP.NET Core, Entity Framework Core, and SQL Server.

The project currently provides employee endpoints and a sample weather endpoint.
It is intentionally simple and is being developed as part of my journey to learn
the fundamentals of .NET Web API development.

## What This Project Demonstrates

- Creating an ASP.NET Core Web API
- Organizing API endpoints with controllers
- Routing and HTTP methods
- Model binding JSON request bodies
- Returning `200 OK` and `404 Not Found` responses
- Registering services with dependency injection
- Creating an Entity Framework Core `DbContext`
- Reading and writing employee data with EF Core
- Connecting an API to SQL Server
- Creating and applying EF Core migrations
- Generating an OpenAPI document
- Testing requests with a `.http` file

## Technologies

- C#
- ASP.NET Core Web API
- .NET 11 preview
- Entity Framework Core
- SQL Server
- Microsoft SQL Server EF Core provider
- Built-in ASP.NET Core OpenAPI support

## Project Structure

```text
dotnetapi/
├── EmployeeAdmin/
│   ├── Controllers/
│   │   ├── EmployeesController.cs
│   │   └── WeatherForecastController.cs
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   ├── Migrations/
│   │   ├── 20261015164238_InitialCreate.cs
│   │   ├── 20261015164238_InitialCreate.Designer.cs
│   │   └── ApplicationDbContextModelSnapshot.cs
│   ├── Models/
│   │   ├── AddEmployeeDto.cs
│   │   └── Employee.cs
│   ├── appsettings.json
│   ├── EmployeeAdmin.csproj
│   ├── EmployeeAdmin.http
│   └── Program.cs
└── README.md
```

## Prerequisites

Install the following before running the project:

- .NET SDK compatible with the project target framework
- SQL Server running locally or on another accessible server
- Visual Studio, Visual Studio Code, or another C# editor
- Optional: the REST Client extension for Visual Studio Code
- Optional: `dotnet-ef` for Entity Framework Core CLI commands

Because this project currently targets a .NET preview SDK, install the preview
SDK specified by the project or change the target framework and package versions
to a supported .NET release.

## Clone the Repository

```bash
git clone https://github.com/<your-username>/<your-repository>.git
cd <your-repository>
```

## Configure SQL Server

Open `EmployeeAdmin/appsettings.json` and configure the connection string.
The key must be named `DefaultConnection`, because that is the key read by
`Program.cs`.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=EmployeeApi;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

For a local SQL Server installation, the server portion may instead look like:

```text
Server=localhost;Database=EmployeeApi;Trusted_Connection=True;TrustServerCertificate=True;
```

Do not commit real passwords, access tokens, or other secrets to GitHub. Use
user secrets or environment variables for credentials in real applications.

## Restore and Build

Run these commands from the repository root:

```bash
dotnet restore EmployeeAdmin/EmployeeAdmin.csproj
dotnet build EmployeeAdmin/EmployeeAdmin.csproj
```

## Entity Framework Core Migrations

Install the EF Core CLI tool if it is not already installed:

```bash
dotnet tool install --global dotnet-ef
```

Apply the existing migration and create the database:

```bash
dotnet ef database update \
  --project EmployeeAdmin/EmployeeAdmin.csproj \
  --startup-project EmployeeAdmin/EmployeeAdmin.csproj
```

When the model changes, create a new migration:

```bash
dotnet ef migrations add DescribeYourChange \
  --project EmployeeAdmin/EmployeeAdmin.csproj \
  --startup-project EmployeeAdmin/EmployeeAdmin.csproj
```

Then apply it:

```bash
dotnet ef database update \
  --project EmployeeAdmin/EmployeeAdmin.csproj \
  --startup-project EmployeeAdmin/EmployeeAdmin.csproj
```

## Run the API

```bash
dotnet run --project EmployeeAdmin/EmployeeAdmin.csproj
```

In Development, the HTTP profile uses:

```text
http://localhost:5296
```

The HTTPS profile also uses:

```text
https://localhost:7124
```

If port `5296` is already in use, stop the existing application process or run
the API on another port:

```bash
dotnet run \
  --project EmployeeAdmin/EmployeeAdmin.csproj \
  --urls http://localhost:5301
```

## OpenAPI Documentation

When the application is running in the Development environment, the OpenAPI
document is available at:

```text
http://localhost:5296/openapi/v1.json
```

This endpoint returns the API definition as JSON. It is not a Swagger UI page.

## API Endpoints

### List all employees

```http
GET /api/employees
```

Example:

```bash
curl http://localhost:5296/api/employees
```

### Get one employee

```http
GET /api/employees/{id}
```

The `id` must be a valid employee GUID.

Example:

```bash
curl http://localhost:5296/api/employees/3F2504E0-4F89-11D3-9A0C-0305E82C3301
```

The endpoint returns `404 Not Found` when the employee does not exist.

### Add an employee

```http
POST /api/employees
Content-Type: application/json
```

Example request:

```json
{
  "names": "Dilhara",
  "email": "dilhara@gmail.com",
  "phone": "0773434567",
  "salary": 35000
}
```

Example using `curl`:

```bash
curl -X POST http://localhost:5296/api/employees \
  -H "Content-Type: application/json" \
  -d '{
    "names": "Dilhara",
    "email": "dilhara@gmail.com",
    "phone": "0773434567",
    "salary": 35000
  }'
```

### Weather sample

```http
GET /WeatherForecast
```

This is the sample endpoint generated by the ASP.NET Core Web API template.

## Testing with `EmployeeAdmin.http`

The project includes
[`EmployeeAdmin/EmployeeAdmin.http`](./EmployeeAdmin/EmployeeAdmin.http), which
contains requests for:

- The OpenAPI document
- The weather sample
- Listing employees
- Creating an employee
- Getting an employee by ID

In Visual Studio Code, install a REST client extension to run these requests
directly from the file.

## Database Model

The `Employee` entity currently contains:

| Property | Type | Description |
|---|---|---|
| `Id` | `Guid` | Unique employee identifier |
| `Names` | `string` | Employee name |
| `Email` | `string` | Employee email address |
| `Phone` | `string?` | Optional phone number |
| `Salary` | `decimal` | Employee salary |

## Current Limitations

This is a learning project and is not yet production-ready. Current limitations
include:

- The employee endpoints use synchronous database calls.
- The API currently returns the EF Core entity directly.
- Create requests do not yet have comprehensive validation.
- There are no authentication or authorization rules.
- There are no automated unit or integration tests.
- There is no centralized API exception handling.
- Update and delete employee endpoints have not been added.

## Planned Improvements

- Add asynchronous controller and EF Core operations
- Add create, update, and delete status-code handling
- Add request and response DTOs
- Add validation for employee data
- Add a service layer
- Add global exception handling and Problem Details
- Add authentication and authorization
- Add pagination, filtering, and searching
- Add unit and integration tests
- Add structured logging
- Deploy the API to a cloud platform

## Learning Goals

The main goal of this repository is to learn how the pieces of a .NET API fit
together:

1. Receive an HTTP request.
2. Route it to a controller action.
3. Bind request data to a C# model.
4. Use Entity Framework Core to access SQL Server.
5. Return an HTTP response to the client.
6. Document and test the API.

## License

This project is created for learning and educational purposes.
