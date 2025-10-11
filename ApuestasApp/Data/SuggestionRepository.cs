using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ApuestasApp.Models;

namespace ApuestasApp.Data;

public class SuggestionRepository
{
    private const string ConnectionString =
        "Server=localhost;Database=Apuestas;User Id=sa;Password=triplea;TrustServerCertificate=True;";

    public IList<Suggestion> GetAll()
    {
        var suggestions = new List<Suggestion>();
        using var connection = new SqlConnection(ConnectionString);
        using var command = connection.CreateCommand();
        command.CommandText = @"
            SELECT id, dato
            FROM Sugerencias
            ORDER BY id;";

        connection.Open();
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            suggestions.Add(new Suggestion
            {
                Id = reader.GetInt32(0),
                Dato = reader.IsDBNull(1) ? null : reader.GetString(1)
            });
        }

        return suggestions;
    }

    public int Add(Suggestion suggestion)
    {
        using var connection = new SqlConnection(ConnectionString);
        using var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Sugerencias (dato)
            OUTPUT INSERTED.id
            VALUES (@dato);";
        command.Parameters.Add("@dato", SqlDbType.VarChar, 200).Value = (object?)suggestion.Dato ?? DBNull.Value;

        connection.Open();
        return (int)command.ExecuteScalar();
    }

    public void Update(Suggestion suggestion)
    {
        using var connection = new SqlConnection(ConnectionString);
        using var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Sugerencias
            SET dato = @dato
            WHERE id = @id;";
        command.Parameters.Add("@dato", SqlDbType.VarChar, 200).Value = (object?)suggestion.Dato ?? DBNull.Value;
        command.Parameters.Add("@id", SqlDbType.Int).Value = suggestion.Id;

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var connection = new SqlConnection(ConnectionString);
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Sugerencias WHERE id = @id";
        command.Parameters.Add("@id", SqlDbType.Int).Value = id;

        connection.Open();
        command.ExecuteNonQuery();
    }
}
