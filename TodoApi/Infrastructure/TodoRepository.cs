using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using TodoApi.Contracts;
using TodoApi.Models;

namespace TodoApi.Infrastructure;

public class TodoRepository : ITodoRepository
{
    private readonly string _connectionString;

    public TodoRepository(IOptions<DatabaseOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public Todo Create(Todo todo)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var cmd = connection.CreateCommand();
        cmd.CommandText = """
            INSERT INTO Todos (Title, Description, IsCompleted, CreatedAt)
            VALUES (@title, @desc, @done, @created);
            SELECT last_insert_rowid();
        """;

        cmd.Parameters.AddWithValue("@title", todo.Title);
        cmd.Parameters.AddWithValue("@desc", todo.Description ?? "");
        cmd.Parameters.AddWithValue("@done", todo.IsCompleted ? 1 : 0);
        cmd.Parameters.AddWithValue("@created", todo.CreatedAt.ToString("o"));

        todo.Id = Convert.ToInt32(cmd.ExecuteScalar());
        return todo;
    }

    public IEnumerable<Todo> GetAll()
    {
        var list = new List<Todo>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM Todos";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new Todo
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Description = reader.GetString(2),
                IsCompleted = reader.GetInt32(3) == 1,
                CreatedAt = DateTime.Parse(reader.GetString(4))
            });
        }

        return list;
    }

    public Todo? GetById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM Todos WHERE Id = @id";
        cmd.Parameters.AddWithValue("@id", id);

        using var reader = cmd.ExecuteReader();
        if (!reader.Read()) return null;

        return new Todo
        {
            Id = reader.GetInt32(0),
            Title = reader.GetString(1),
            Description = reader.GetString(2),
            IsCompleted = reader.GetInt32(3) == 1,
            CreatedAt = DateTime.Parse(reader.GetString(4))
        };
    }

    public bool Update(Todo todo)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var cmd = connection.CreateCommand();
        cmd.CommandText = """
            UPDATE Todos
            SET Title=@title, Description=@desc, IsCompleted=@done
            WHERE Id=@id
        """;

        cmd.Parameters.AddWithValue("@id", todo.Id);
        cmd.Parameters.AddWithValue("@title", todo.Title);
        cmd.Parameters.AddWithValue("@desc", todo.Description ?? "");
        cmd.Parameters.AddWithValue("@done", todo.IsCompleted ? 1 : 0);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Delete(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "DELETE FROM Todos WHERE Id=@id";
        cmd.Parameters.AddWithValue("@id", id);

        return cmd.ExecuteNonQuery() > 0;
    }
}
