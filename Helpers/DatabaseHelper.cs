
using Microsoft.Data.SqlClient;

namespace StudentCourseManagementConsole.Helpers;

public static class DatabaseHelper
{
    private const string ConnectionString =
        @"Server=.\SQLEXPRESS;Database=StudentCourseDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public static SqlConnection CreateConnection()
    {
        return new SqlConnection(ConnectionString);
    }

    public static async Task<bool> TestConnectionAsync()
    {
        try
        {
            await using var connection = CreateConnection();
            await connection.OpenAsync();

            await using var command = new SqlCommand(
                "SELECT DB_NAME();", connection);

            var databaseName = await command.ExecuteScalarAsync();

            Console.WriteLine(
                $"Database connected successfully: {databaseName}");

            return true;
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Database connection failed: {ex.Message}");
            return false;
        }
    }
}
