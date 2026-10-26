using EmployeeApi.Controllers;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace EmployeeApi.Tests.XUnit;

public class EmployeeTests
{
    private readonly EmployeeService _service = new();
    [Fact]
    public void GetAll_ReturnsThreeEmployees() => Assert.Equal(3, _service.GetAll().Count);
    [Theory]
    [InlineData(1, "Alice")]
    [InlineData(2, "Bob")]
    public void GetById_ExistingEmployee_ReturnsName(int id, string expected) =>
        Assert.Equal(expected, _service.GetById(id)?.Name);
    [Fact]
    public void GetById_MissingEmployee_ReturnsNull() => Assert.Null(_service.GetById(999));
    [Fact]
    public void Controller_GetById_Existing_ReturnsOk()
    {
        var result = new EmployeesController(_service).GetById(1);
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, ok.StatusCode);
    }
    [Fact]
    public void Controller_GetById_Invalid_ReturnsBadRequest() =>
        Assert.IsType<BadRequestObjectResult>(new EmployeesController(_service).GetById(0).Result);
    [Fact]
    public void Controller_GetById_Missing_ReturnsNotFound() =>
        Assert.IsType<NotFoundResult>(new EmployeesController(_service).GetById(999).Result);

    [Fact]
    public void GetById_WhenEmployeeDoesNotExist_ReturnsNull()
    {
        // Arrange
        const int id = 5;

        // Act
        var result = _service.GetById(id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetById_WhenEmployeeExists_ReturnsEmployee()
    {
        // Arrange
        const int id = 1;

        // Act
        var result = _service.GetById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal("Alice", result.Name);
    }

    [Theory]
    [InlineData(1, "Alice")]
    [InlineData(2, "Bob")]
    [InlineData(3, "Carol")]
    public void GetById_WhenEmployeeExists_ReturnsCorrectEmployee(int id, string expectedName)
    {
        // Act
        var result = _service.GetById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(expectedName, result.Name);
    }
}
