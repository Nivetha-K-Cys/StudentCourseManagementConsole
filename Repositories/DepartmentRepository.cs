using System.Data;
using Microsoft.Data.SqlClient;
using StudentCourseManagementConsole.Helpers;
using StudentCourseManagementConsole.Models;

namespace StudentCourseManagementConsole.Repositories;

public class DepartmentRepository
{
    public async Task<int> AddAsync(Department department)
    {
        const string sql = "INSERT INTO Departments (DepartmentName, Location) VALUES (@Name, @Location); SELECT CAST(SCOPE_IDENTITY() AS INT);";
        await using var connection = DatabaseHelper.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = department.DepartmentName;
        command.Parameters.Add("@Location", SqlDbType.NVarChar, 100).Value = (object?)department.Location ?? DBNull.Value;
        await connection.OpenAsync();
        return Convert.ToInt32(await command.ExecuteScalarAsync());
    }

    public async Task<List<Department>> GetAllAsync()
    {
        const string sql = "SELECT DepartmentId, DepartmentName, Location FROM Departments ORDER BY DepartmentName;";
        var items = new List<Department>();
        await using var connection = DatabaseHelper.CreateConnection();
        await using var command = new SqlCommand(sql, connection);
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            items.Add(new Department { DepartmentId = reader.GetInt32(0), DepartmentName = reader.GetString(1), Location = reader.IsDBNull(2) ? null : reader.GetString(2) });
        return items;
    }
}
