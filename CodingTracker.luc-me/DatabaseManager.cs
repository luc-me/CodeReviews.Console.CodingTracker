using System.Linq.Expressions;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;


namespace CodingTracker.luc_me;

public class DatabaseManager
{

    static string? connectionString="";
    public DatabaseManager()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        connectionString= config.GetConnectionString("SQLite");

    }
    internal void CreateTable()
    {
            using SqliteConnection connection = new(connectionString);
            connection.Open();
            string sql = @"CREATE TABLE IF NOT EXISTS Coding (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            StartTime TEXT,
            EndTime TEXT,
            Duration TEXT
            );";

            connection.Execute(sql);
            connection.Close();
    }
    internal void Insert(Coding session)
    {
        using SqliteConnection connection = new(connectionString);
        connection.Open();
        string sql = "INSERT INTO Coding (StartTime,EndTime,Duration) VALUES (@startTime,@endTime,@duration)";
        connection.Execute(sql,new {
            startTime = session.StartTime, 
            endTime = session.EndTime, 
            duration = session.Duration
            });

        connection.Close();
    }
    internal List<Coding> Get()
    {
        using SqliteConnection connection = new(connectionString);
        connection.Open();

        string sql = "SELECT * FROM Coding";
        var sessions = connection.Query<Coding>(sql).ToList();

        return sessions;
    }
    internal void Delete(List<Coding> selectedSessions)
    {
        using SqliteConnection connection = new(connectionString);
        connection.Open();
        var ids = selectedSessions.Select(session=>session.Id);
        
        string sql = "DELETE FROM Coding WHERE Id IN @ids";
        connection.Execute(sql,new {ids});
    }
    internal void Update(List<Coding> selectedSessions)
    {
        using SqliteConnection connection = new(connectionString);
        connection.Open();

        string sql = @"UPDATE Coding SET 
        StartTime = @startTime,
        EndTime = @endTime,
        Duration = @duration
        WHERE Id = @id
        ";
        foreach (Coding s in selectedSessions)
            connection.Execute(sql,new {
                startTime = s.StartTime, 
                endTime = s.EndTime,
                duration = s.Duration,
                id = s.Id
                });
    }
}
