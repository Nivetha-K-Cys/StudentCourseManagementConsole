using StudentCourseManagementConsole.Helpers;
using StudentCourseManagementConsole.Repositories;
using StudentCourseManagementConsole.Services;


var departmentService = new DepartmentService(new DepartmentRepository());
var studentService = new StudentService(new StudentRepository());
var courseService = new CourseService(new CourseRepository());

await DatabaseHelper.TestConnectionAsync();

while (true)
{
    Console.WriteLine("\n========================================");
    Console.WriteLine(" STUDENT COURSE MANAGEMENT SYSTEM");
    Console.WriteLine("========================================");
    Console.WriteLine("1. Add Department");
    Console.WriteLine("2. List Departments");
    Console.WriteLine("3. Add Student");
    Console.WriteLine("4. List Students");
    Console.WriteLine("5. Add Course");
    Console.WriteLine("6. List Courses");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");
    var choice = Console.ReadLine()?.Trim();

    try
    {
        switch (choice)
        {
            case "1":
                Console.Write("Department name: "); var departmentName = Console.ReadLine() ?? "";
                Console.Write("Location (optional): "); var location = Console.ReadLine();
                Console.WriteLine($"Department created. ID: {await departmentService.AddAsync(departmentName, location)}");
                break;
            case "2":
                var departments = await departmentService.GetAllAsync();
                if (departments.Count == 0) Console.WriteLine("No departments found.");
                foreach (var d in departments) Console.WriteLine($"{d.DepartmentId} | {d.DepartmentName} | {d.Location ?? "-"}");
                break;
            case "3":
                Console.Write("Student name: "); var studentName = Console.ReadLine() ?? "";
                Console.Write("Email: "); var email = Console.ReadLine() ?? "";
                Console.Write("Age: "); var age = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Department ID (create department first): "); var studentDepartmentId = int.Parse(Console.ReadLine() ?? "0");
                Console.WriteLine($"Student registered. ID: {await studentService.AddAsync(studentName, email, age, studentDepartmentId)}");
                break;
            case "4":
                var students = await studentService.GetAllAsync();
                if (students.Count == 0) Console.WriteLine("No students found.");
                foreach (var s in students) Console.WriteLine($"{s.StudentId} | {s.Name} | {s.Email} | Age: {s.Age} | Department ID: {s.DepartmentId}");
                break;
            case "5":
                Console.Write("Course name: "); var courseName = Console.ReadLine() ?? "";
                Console.Write("Duration in months: "); var duration = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Fee: "); var fee = decimal.Parse(Console.ReadLine() ?? "0");
                Console.Write("Department ID (create department first): "); var courseDepartmentId = int.Parse(Console.ReadLine() ?? "0");
                Console.WriteLine($"Course created. ID: {await courseService.AddAsync(courseName, duration, fee, courseDepartmentId)}");
                break;
            case "6":
                var courses = await courseService.GetAllAsync();
                if (courses.Count == 0) Console.WriteLine("No courses found.");
                foreach (var c in courses) Console.WriteLine($"{c.CourseId} | {c.CourseName} | {c.DurationMonths} months | Fee: {c.Fee:C} | Department ID: {c.DepartmentId}");
                break;
            case "0": return;
            default: Console.WriteLine("Invalid option. Please try again."); break;
        }
    }
    catch (FormatException)
    {
        Console.WriteLine("Invalid number format. Please enter a valid number.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        Console.WriteLine("Check that SQL Server is running and the database script has been executed.");
    }
}
