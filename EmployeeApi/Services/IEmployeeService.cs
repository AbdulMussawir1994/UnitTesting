using EmployeeApi.Models;
namespace EmployeeApi.Services;
public interface IEmployeeService
{
    IReadOnlyList<Employee> GetAll();
    Employee? GetById(int id);
}
