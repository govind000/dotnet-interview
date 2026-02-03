using TodoApi.Models;
using TodoApi.Models.Requests;

namespace TodoApi.Contracts;

public interface ITodoService
{
    Todo Create(CreateTodoRequest request);
    IEnumerable<Todo> GetAll();
    Todo? GetById(int id);
    Todo? Update(int id, UpdateTodoRequest request);
    bool Delete(int id);
}
