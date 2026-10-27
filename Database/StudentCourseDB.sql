-- Run this script once in SQL Server Management Studio (SSMS).
IF DB_ID(N'StudentCourseDB') IS NULL
    CREATE DATABASE StudentCourseDB;
GO
USE StudentCourseDB;
GO

IF OBJECT_ID(N'dbo.Departments', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Departments
    (
        DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
        DepartmentName NVARCHAR(100) NOT NULL UNIQUE,
        Location NVARCHAR(100) NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Students', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students
    (
        StudentId INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Email NVARCHAR(255) NOT NULL UNIQUE,
        Age INT NOT NULL CHECK (Age BETWEEN 17 AND 60),
        DepartmentId INT NOT NULL,
        CONSTRAINT FK_Students_Departments FOREIGN KEY (DepartmentId)
            REFERENCES dbo.Departments(DepartmentId)
    );
END;
GO

IF OBJECT_ID(N'dbo.Courses', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Courses
    (
        CourseId INT IDENTITY(1,1) PRIMARY KEY,
        CourseName NVARCHAR(100) NOT NULL UNIQUE,
        DurationMonths INT NOT NULL CHECK (DurationMonths > 0),
        Fee DECIMAL(10,2) NOT NULL CHECK (Fee >= 0),
        DepartmentId INT NOT NULL,
        CONSTRAINT FK_Courses_Departments FOREIGN KEY (DepartmentId)
            REFERENCES dbo.Departments(DepartmentId)
    );
END;
GO

-- Optional sample department. Run only if you want a sample row.
-- INSERT INTO Departments (DepartmentName, Location) VALUES (N'Computer Science', N'Block A');
