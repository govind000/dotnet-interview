using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

public class SqliteDatabaseInitializer : IDatabaseInitializer
{
    private readonly string _connectionString;

    public SqliteDatabaseInitializer(IOptions<DatabaseOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = """
            CREATE TABLE IF NOT EXISTS Todos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                Description TEXT,
                IsCompleted INTEGER NOT NULL DEFAULT 0,
                CreatedAt TEXT NOT NULL
            );
        """;

        command.ExecuteNonQuery();
    }
}