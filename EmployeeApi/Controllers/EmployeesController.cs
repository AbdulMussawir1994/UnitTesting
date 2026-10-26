using EmployeeApi.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;
namespace EmployeeApi.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController(IEmployeeService employees) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<Employee>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<Employee>> GetAll() => Ok(employees.GetAll());

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Employee> GetById(int id)
    {
        if (id <= 0) return BadRequest("Employee ID must be positive.");
        var employee = employees.GetById(id);
        return employee is null ? NotFound() : Ok(employee);
    }
}
