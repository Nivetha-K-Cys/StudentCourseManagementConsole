STUDENT COURSE MANAGEMENT SYSTEM - CONSOLE APP
Target: .NET 10
Database: SQL Server Express
Data access: ADO.NET with Microsoft.Data.SqlClient

PROJECT FOLDERS
Models/       Department.cs, Student.cs, Course.cs
Repositories/ DepartmentRepository.cs, StudentRepository.cs, CourseRepository.cs
Services/     DepartmentService.cs, StudentService.cs, CourseService.cs
Helpers/      DatabaseHelper.cs
Database/     StudentCourseDB.sql
Program.cs    Console menu

SETUP IN VISUAL STUDIO
1. Extract this ZIP file.
2. Open StudentCourseManagementConsole.csproj in Visual Studio 2022 (with .NET 10 SDK installed).
3. Open Database/StudentCourseDB.sql in SQL Server Management Studio and execute it.
4. If your SQL Server instance is not .\SQLEXPRESS, edit Helpers/DatabaseHelper.cs and change the Server= value.
5. Visual Studio should restore Microsoft.Data.SqlClient from NuGet. If needed, right-click the project > Manage NuGet Packages > Browse > Microsoft.Data.SqlClient > Install.
6. Build > Build Solution, then press Ctrl+F5.
7. Create a department first. Use its ID when adding students and courses.

DEFAULT CONNECTION STRING
Server=.\SQLEXPRESS;Database=StudentCourseDB;Trusted_Connection=True;TrustServerCertificate=True;

This learning project includes Add/List operations for Departments, Students, and Courses.
