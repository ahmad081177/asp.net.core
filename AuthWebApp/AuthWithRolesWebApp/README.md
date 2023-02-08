# Auth Web App
This project is built in VS 2022, using .NET Core 6.0
## Main Features
- Login
- Register
- Logout
- Pages: 
  - anonymous (Not logged in user), /Example/AnyUser
  - Logged in user page, /Example/LoggedInUser
  - Admin page, /Example/AdminUser
- Roles: user, teacher and admin
- Roles and Session as Filters:
  - Logged in Filter: 
  ```
  [SessionFilter]
  public IActionResult Dashboard()
  {
	  return View();
  }
  ```
  - Role Filter:
  ```
  [UserRoleFilter(AppRoleConstants.ADMIN_ROLE_NAME)]
  //[UserRoleFilter("admin")]
  public IActionResult AdminUser()
  {
      return View();
  }
  ```
## How to build
- Open the project in VS 2022
- Add the following dependencies using Nuget Manager:
  - Microsoft.EntityFrameworkCore
  - Microsoft.EntityFrameworkCore.Design
  - Microsoft.EntityFrameworkCore.SqlServer
  - Microsoft.EntityFrameworkCore.Tools
  - Microsoft.VisualStudio.Web.CodeGeneration.Design
- Create App_Data folder under the main project
- Open Package Console Manager nad type the following:
  - Add-Migration init
  - Update-Database
