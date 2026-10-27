using StudentCourseManagementConsole.Models;
using StudentCourseManagementConsole.Repositories;

namespace StudentCourseManagementConsole.Services;

public class StudentService(StudentRepository repository)
{
    public Task<List<Student>> GetAllAsync() => repository.GetAllAsync();

    public Task<int> AddAsync(string name, string email, int age, int departmentId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Student name is required.");
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@')) throw new ArgumentException("Enter a valid email address.");
        if (age is < 17 or > 60) throw new ArgumentException("Age must be between 17 and 60.");
        if (departmentId <= 0) throw new ArgumentException("Department ID must be positive.");
        return repository.AddAsync(new Student { Name = name.Trim(), Email = email.Trim(), Age = age, DepartmentId = departmentId });
    }
}
