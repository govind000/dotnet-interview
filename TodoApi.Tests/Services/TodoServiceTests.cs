using Moq;
using TodoApi.Contracts;
using TodoApi.Models;
using TodoApi.Models.Requests;
using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests.Services;

public class TodoServiceTests
{
    private readonly Mock<ITodoRepository> _repoMock;
    private readonly TodoService _service;

    public TodoServiceTests()
    {
        _repoMock = new Mock<ITodoRepository>();
        _service = new TodoService(_repoMock.Object);
    }

    // ---------- CREATE ----------

    [Fact]
    public void Create_ShouldCreateTodo()
    {
        _repoMock
            .Setup(r => r.Create(It.IsAny<Todo>()))
            .Returns<Todo>(t =>
            {
                t.Id = 1;
                return t;
            });

        var request = new CreateTodoRequest
        {
            Title = "Test",
            Description = "Desc"
        };

        var result = _service.Create(request);

        Assert.Equal(1, result.Id);
        Assert.Equal("Test", result.Title);
        Assert.False(result.IsCompleted);
    }

    // ---------- GET ALL ----------

    [Fact]
    public void GetAll_ShouldReturnTodos()
    {
        _repoMock
            .Setup(r => r.GetAll())
            .Returns(new List<Todo>
            {
                new Todo { Id = 1, Title = "Todo 1" },
                new Todo { Id = 2, Title = "Todo 2" }
            });

        var result = _service.GetAll();

        Assert.Equal(2, result.Count());
    }

    // ---------- GET BY ID ----------

    [Fact]
    public void GetById_ShouldReturnNull_WhenNotFound()
    {
        _repoMock
            .Setup(r => r.GetById(1))
            .Returns((Todo?)null);

        var result = _service.GetById(1);

        Assert.Null(result);
    }

    // ---------- UPDATE ----------

    [Fact]
    public void Update_ShouldReturnNull_WhenTodoDoesNotExist()
    {
        _repoMock
            .Setup(r => r.GetById(1))
            .Returns((Todo?)null);

        var result = _service.Update(1, new UpdateTodoRequest
        {
            Title = "Updated",
            IsCompleted = true
        });

        Assert.Null(result);
    }

    [Fact]
    public void Update_ShouldUpdateTodo_WhenExists()
    {
        var existing = new Todo
        {
            Id = 1,
            Title = "Old",
            IsCompleted = false
        };

        _repoMock
            .Setup(r => r.GetById(1))
            .Returns(existing);

        _repoMock
            .Setup(r => r.Update(It.IsAny<Todo>()))
            .Returns(true);

        var result = _service.Update(1, new UpdateTodoRequest
        {
            Title = "Updated",
            IsCompleted = true
        });

        Assert.NotNull(result);
        Assert.Equal("Updated", result!.Title);
        Assert.True(result.IsCompleted);
    }

    // ---------- DELETE ----------

    [Fact]
    public void Delete_ShouldReturnTrue_WhenDeleted()
    {
        _repoMock
            .Setup(r => r.Delete(1))
            .Returns(true);

        var result = _service.Delete(1);

        Assert.True(result);
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenNotDeleted()
    {
        _repoMock
            .Setup(r => r.Delete(1))
            .Returns(false);

        var result = _service.Delete(1);

        Assert.False(result);
    }
}
