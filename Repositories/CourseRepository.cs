using System.Data;
using Microsoft.Data.SqlClient;
using StudentCourseManagementConsole.Helpers;
using StudentCourseManagementConsole.Models;

namespace StudentCourseManagementConsole.Repositories;

public class CourseRepository
{
    public async Task<int> AddAsync(Course course)
    {
        const string sql = "INSERT INTO Courses (CourseName, DurationMonths, Fee, DepartmentId) VALUES (@Name, @Duration, @Fee, @DepartmentId); SELECT CAST(SCOPE_IDENTITY() AS INT);";
        await using var connection = DatabaseHelper.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = course.CourseName;
        command.Parameters.Add("@Duration", SqlDbType.Int).Value = course.DurationMonths;
        command.Parameters.Add("@Fee", SqlDbType.Decimal).Value = course.Fee;
        command.Parameters["@Fee"].Precision = 10;
        command.Parameters["@Fee"].Scale = 2;
        command.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = course.DepartmentId;
        await connection.OpenAsync();
        return Convert.ToInt32(await command.ExecuteScalarAsync());
    }

    public async Task<List<Course>> GetAllAsync()
    {
        const string sql = "SELECT CourseId, CourseName, DurationMonths, Fee, DepartmentId FROM Courses ORDER BY CourseName;";
        var items = new List<Course>();
        await using var connection = DatabaseHelper.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            items.Add(new Course { CourseId = reader.GetInt32(0), CourseName = reader.GetString(1), DurationMonths = reader.GetInt32(2), Fee = reader.GetDecimal(3), DepartmentId = reader.GetInt32(4) });
        return items;
    }
}
