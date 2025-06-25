# TodoAppSolution

## Overview

`TodoAppSolution` consists of:
1. **TodoApp**: A Blazor WebAssembly project for managing tasks.
2. **TodosApi**: An ASP.NET Core Web API project providing backend services for TodoApp.

---
#### Link: https://staging.d2lnm6vmiw33x9.amplifyapp.com/

#### Log in credentials: 

username: admin

password: password

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later installed.
- A compatible code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/).

---

## Running the Applications

### Frontend Running TodoApp

Frontend is runnig on port 5000 [http://localhost:5000](http://localhost:5000/)

Use this login data  for testing:

username: `admin` 

password: `password`

#### Frontend:From Parent Directory TodoAppSolution

dotnet run --project TodoApp

#### From TodoApp directory

dotnet run 

### Backend Running TodosApi

Backend is runnig on [http://localhost:5075](http://localhost:5075)

See Swagger API documentation [http://localhost:5075/swagger/index.html](http://localhost:5075/swagger/index.html)

#### From Parent Directory TodoAppSolution

dotnet run --project TodosApi

#### From TodosApi directory

dotnet run 


## Building the Solution

dotnet build



