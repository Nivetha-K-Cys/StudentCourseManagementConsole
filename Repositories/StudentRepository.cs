
using System.Data;
using Microsoft.Data.SqlClient;
using StudentCourseManagementConsole.Helpers;
using StudentCourseManagementConsole.Models;

namespace StudentCourseManagementConsole.Repositories;

public class StudentRepository
{
    public async Task<int> AddAsync(Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        if (string.IsNullOrWhiteSpace(student.Name))
        {
            throw new ArgumentException(
                "Student name is required.", nameof(student));
        }

        if (string.IsNullOrWhiteSpace(student.Email))
        {
            throw new ArgumentException(
                "Student email is required.", nameof(student));
        }

        student.Name = student.Name.Trim();
        student.Email = student.Email.Trim();

        const string sql = @"
            INSERT INTO Students (Name, Email, Age, DepartmentId)
            VALUES (@Name, @Email, @Age, @DepartmentId);

            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        await using var connection = DatabaseHelper.CreateConnection();
        await using var command = new SqlCommand(sql, connection);

        command.Parameters.Add("@Name", SqlDbType.NVarChar, 100)
            .Value = student.Name;

        command.Parameters.Add("@Email", SqlDbType.NVarChar, 255)
            .Value = student.Email;

        command.Parameters.Add("@Age", SqlDbType.Int)
            .Value = student.Age;

        command.Parameters.Add("@DepartmentId", SqlDbType.Int)
            .Value = student.DepartmentId;

        await connection.OpenAsync();

        return Convert.ToInt32(await command.ExecuteScalarAsync());
    }

    public async Task<List<Student>> GetAllAsync()
    {
        const string sql = @"
            SELECT StudentId, Name, Email, Age, DepartmentId
            FROM Students
            ORDER BY Name;";

        var items = new List<Student>();

        await using var connection = DatabaseHelper.CreateConnection();
        await using var command = new SqlCommand(sql, connection);

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            items.Add(new Student
            {
                StudentId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Age = reader.GetInt32(3),
                DepartmentId = reader.GetInt32(4)
            });
        }

        return items;
    }
}
