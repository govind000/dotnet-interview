using TodoApi.Models;

namespace TodoApi.Contracts;

public interface ITodoRepository
{
	Todo Create(Todo todo);
	IEnumerable<Todo> GetAll();
	Todo? GetById(int id);
	bool Update(Todo todo);
	bool Delete(int id);
}
