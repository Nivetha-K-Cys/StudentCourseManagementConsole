using Microsoft.Data.SqlClient;

namespace StudentCourseManagementConsole.Helpers;

public static class DatabaseHelper
{
    // Change Server if your SQL Server instance uses a different name.
    private const string ConnectionString =
        @"Server=.\SQLEXPRESS;Database=StudentCourseDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public static SqlConnection CreateConnection() => new(ConnectionString);
}
