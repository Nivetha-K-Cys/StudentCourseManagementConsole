using StudentCourseManagementConsole.Models;
using StudentCourseManagementConsole.Repositories;

namespace StudentCourseManagementConsole.Services;

public class DepartmentService(DepartmentRepository repository)
{
    public Task<List<Department>> GetAllAsync() => repository.GetAllAsync();

    public Task<int> AddAsync(string name, string? location)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Department name is required.");
        return repository.AddAsync(new Department { DepartmentName = name.Trim(), Location = string.IsNullOrWhiteSpace(location) ? null : location.Trim() });
    }
}
