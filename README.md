# ScheduleCreator
Web app for creating school schedules based on .NET Core MVC and SQL Server.
# Setup
## Requirements
* Visual Studio 2022 (with MVC development pack)
* SQL server

## Instalation
1. Clone repository to your PC. Git Bash command: 
```
git clone git@github.com:khhhhh/ScheduleCreator.git
```
5. Open [appsettings.json](ScheduleCreator/appsettings.json) with text editor and change value of key `DefaultConnection` to your connection string.
> :warning: **SQL server support only**
6. Open [ScheduleCreator.sln](/ScheduleCreator.sln) with Visual Studio.
7. Build project.
8. Make migrations and create database.
  - .NET CLI
  ```
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ````
  - Visual Studio (Power Shell)
  ```
  Add-Migration InitialCreate
  Update-Database
  ```

9. Build and run project.
