using EmployeeApi.Controllers;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace EmployeeApi.Tests.MSTest;


[TestClass]
public class EmployeeTests
{
    private readonly EmployeeService _service = new();
    [TestMethod] public void GetAll_ReturnsThreeEmployees() => Assert.AreEqual(3, _service.GetAll().Count);
    [DataTestMethod]
    [DataRow(1, "Alice")]
    [DataRow(2, "Bob")]
    public void GetById_ExistingEmployee_ReturnsName(int id, string expected) =>
        Assert.AreEqual(expected, _service.GetById(id)?.Name);
    [TestMethod]
    public void GetById_MissingEmployee_ReturnsNull() =>
        Assert.IsNull(_service.GetById(999));
    [TestMethod]
    public void Controller_GetById_Existing_ReturnsOk() =>
        Assert.IsInstanceOfType<OkObjectResult>(new EmployeesController(_service).GetById(1).Result);
    [TestMethod]
    public void Controller_GetById_Invalid_ReturnsBadRequest() =>
        Assert.IsInstanceOfType<BadRequestObjectResult>(new EmployeesController(_service).GetById(0).Result);
    [TestMethod]
    public void Controller_GetById_Missing_ReturnsNotFound() =>
        Assert.IsInstanceOfType<NotFoundResult>(new EmployeesController(_service).GetById(999).Result);
}
