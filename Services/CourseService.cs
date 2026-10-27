using StudentCourseManagementConsole.Models;
using StudentCourseManagementConsole.Repositories;

namespace StudentCourseManagementConsole.Services;

public class CourseService(CourseRepository repository)
{
    public Task<List<Course>> GetAllAsync() => repository.GetAllAsync();

    public Task<int> AddAsync(string name, int durationMonths, decimal fee, int departmentId)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Course name is required.");
        if (durationMonths <= 0) throw new ArgumentException("Duration must be greater than zero.");
        if (fee < 0) throw new ArgumentException("Fee cannot be negative.");
        if (departmentId <= 0) throw new ArgumentException("Department ID must be positive.");
        return repository.AddAsync(new Course { CourseName = name.Trim(), DurationMonths = durationMonths, Fee = fee, DepartmentId = departmentId });
    }
}
