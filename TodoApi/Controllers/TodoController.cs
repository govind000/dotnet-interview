using Microsoft.AspNetCore.Mvc;
using TodoApi.Contracts;
using TodoApi.Models;
using TodoApi.Models.Requests;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/todos")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTodoRequest request)
    {
        var todo = _todoService.Create(request);
        return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_todoService.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var todo = _todoService.GetById(id);
        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] UpdateTodoRequest request)
    {
        var updated = _todoService.Update(id, request);
        if (updated == null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var deleted = _todoService.Delete(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
