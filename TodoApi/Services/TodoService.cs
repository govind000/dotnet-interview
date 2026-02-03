using TodoApi.Contracts;
using TodoApi.Models;
using TodoApi.Models.Requests;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repo;

    public TodoService(ITodoRepository repo)
    {
        _repo = repo;
    }

    public Todo Create(CreateTodoRequest request)
    {
        var todo = new Todo
        {
            Title = request.Title,
            Description = request.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        return _repo.Create(todo);
    }

    public IEnumerable<Todo> GetAll()
        => _repo.GetAll();

    public Todo? GetById(int id)
        => _repo.GetById(id);

    public Todo? Update(int id, UpdateTodoRequest request)
    {
        var existing = _repo.GetById(id);
        if (existing == null) return null;

        existing.Title = request.Title;
        existing.Description = request.Description;
        existing.IsCompleted = request.IsCompleted;

        return _repo.Update(existing) ? existing : null;
    }

    public bool Delete(int id)
        => _repo.Delete(id);
}
