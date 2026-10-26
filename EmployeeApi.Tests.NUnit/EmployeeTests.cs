using EmployeeApi.Controllers;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
namespace EmployeeApi.Tests.NUnit;

[TestFixture]
public class EmployeeTests
{
    private EmployeeService _service = null!;
    [SetUp] public void Setup() => _service = new EmployeeService();
    [Test] public void GetAll_ReturnsThreeEmployees() => Assert.That(_service.GetAll(), Has.Count.EqualTo(3));
    [TestCase(1, "Alice")]
    [TestCase(2, "Bob")]
    public void GetById_ExistingEmployee_ReturnsName(int id, string expected) =>
        Assert.That(_service.GetById(id)?.Name, Is.EqualTo(expected));
    [Test]
    public void GetById_MissingEmployee_ReturnsNull() =>
        Assert.That(_service.GetById(999), Is.Null);
    [Test]
    public void Controller_GetById_Existing_ReturnsOk() =>
        Assert.That(new EmployeesController(_service).GetById(1).Result, Is.InstanceOf<OkObjectResult>());
    [Test]
    public void Controller_GetById_Invalid_ReturnsBadRequest() =>
        Assert.That(new EmployeesController(_service).GetById(0).Result, Is.InstanceOf<BadRequestObjectResult>());
    [Test]
    public void Controller_GetById_Missing_ReturnsNotFound() =>
        Assert.That(new EmployeesController(_service).GetById(999).Result, Is.InstanceOf<NotFoundResult>());
}
