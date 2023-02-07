# Auth Web App
This project is built in VS 2022, using .NET Core 6.0
## Main Features
- Login
- Register
- Logout
- Regular page
- Page for Logged in user
## How to build
- Open the project in VS 2022
- Add the following dependencies using Nuget Manager:
  - Microsoft.EntityFrameworkCore
  - Microsoft.EntityFrameworkCore.Design
  - Microsoft.EntityFrameworkCore.SqlServer
  - Microsoft.EntityFrameworkCore.Tools
  - Microsoft.VisualStudio.Web.CodeGeneration.Design
- Create App_Data folder under the main project
- Open Package Console Manager nad type the folloing:
  - Add-Migration init
  - Update-Database
