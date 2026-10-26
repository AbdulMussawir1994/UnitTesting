using EmployeeApi.Models;
namespace EmployeeApi.Services;

public class EmployeeService : IEmployeeService
{
    private static readonly IReadOnlyList<Employee> Employees = Array.AsReadOnly(
        new[]
        {
            new Employee(1, "Alice", "Engineering", 75000m),
            new Employee(2, "Bob", "Finance", 65000m),
            new Employee(3, "Carol", "HR", 60000m)
        });
    public IReadOnlyList<Employee> GetAll() => Employees;
    public Employee? GetById(int id) => id <= 0 ? null : Employees.FirstOrDefault(x => x.Id == id);
}
