using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi.Contracts;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Models.Requests;
using Xunit;

namespace TodoApi.Tests.Controllers;

public class TodoControllerTests
{
    private readonly Mock<ITodoService> _serviceMock;
    private readonly TodoController _controller;

    public TodoControllerTests()
    {
        _serviceMock = new Mock<ITodoService>();
        _controller = new TodoController(_serviceMock.Object);
    }

    // ---------- GET BY ID ----------

    [Fact]
    public void GetById_ShouldReturnNotFound_WhenMissing()
    {
        _serviceMock
            .Setup(s => s.GetById(1))
            .Returns((Todo?)null);

        var result = _controller.GetById(1);

        Assert.IsType<NotFoundResult>(result);
    }

    // ---------- GET ALL ------
}