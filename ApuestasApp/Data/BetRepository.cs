using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ApuestasApp.Models;

namespace ApuestasApp.Data
{
    public class BetRepository
    {
        private const string ConnectionString =
            "Server=localhost;Database=Apuestas;User Id=sa;Password=triplea;TrustServerCertificate=True;";

        public IList<Bet> GetBetsByDateRange(DateTime from, DateTime to)
        {
            var bets = new List<Bet>();
            using var connection = new SqlConnection(ConnectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT id, fecha, liga, partido, importe, ganancia, tipo, resultado, cuota, nota, antesDurante, tipster, imagen
                FROM bitacora
                WHERE fecha >= @from AND fecha < @to
                ORDER BY fecha DESC, id DESC;";
            command.Parameters.Add("@from", SqlDbType.DateTime).Value = from.Date;
            command.Parameters.Add("@to", SqlDbType.DateTime).Value = to.Date.AddDays(1);

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                bets.Add(new Bet
                {
                    Id = reader.GetInt32(0),
                    Fecha = reader.GetDateTime(1),
                    Liga = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Partido = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Importe = reader.IsDBNull(4) ? null : reader.GetDecimal(4),
                    Ganancia = reader.IsDBNull(5) ? null : reader.GetDecimal(5),
                    Tipo = reader.IsDBNull(6) ? null : reader.GetString(6),
                    Resultado = reader.IsDBNull(7) ? null : reader.GetString(7),
                    Cuota = reader.IsDBNull(8) ? null : reader.GetDecimal(8),
                    Nota = reader.IsDBNull(9) ? null : reader.GetString(9),
                    AntesDurante = reader.IsDBNull(10) ? null : reader.GetString(10),
                    Tipster = reader.IsDBNull(11) ? null : reader.GetString(11),
                    Imagen = reader.IsDBNull(12) ? null : (byte[])reader[12]
                });
            }

            return bets;
        }

        public IList<string?> GetDistinctResults()
        {
            var results = new List<string?>();
            using var connection = new SqlConnection(ConnectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT resultado FROM bitacora ORDER BY resultado;";

            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                results.Add(reader.IsDBNull(0) ? null : reader.GetString(0));
            }

            return results;
        }

        public int Add(Bet bet)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO bitacora (fecha, liga, partido, importe, ganancia, tipo, resultado, cuota, nota, antesDurante, tipster, imagen)
                OUTPUT INSERTED.id
                VALUES (@fecha, @liga, @partido, @importe, @ganancia, @tipo, @resultado, @cuota, @nota, @antesDurante, @tipster, @imagen);";
            AddParameters(command, bet);

            connection.Open();
            return (int)command.ExecuteScalar();
        }

        public void Update(Bet bet)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE bitacora
                SET fecha = @fecha,
                    liga = @liga,
                    partido = @partido,
                    importe = @importe,
                    ganancia = @ganancia,
                    tipo = @tipo,
                    resultado = @resultado,
                    cuota = @cuota,
                    nota = @nota,
                    antesDurante = @antesDurante,
                    tipster = @tipster,
                    imagen = @imagen
                WHERE id = @id;";
            AddParameters(command, bet);
            command.Parameters.Add("@id", SqlDbType.Int).Value = bet.Id;

            connection.Open();
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM bitacora WHERE id = @id";
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

            connection.Open();
            command.ExecuteNonQuery();
        }

        private static void AddParameters(SqlCommand command, Bet bet)
        {
            command.Parameters.Add("@fecha", SqlDbType.DateTime).Value = bet.Fecha;
            command.Parameters.Add("@liga", SqlDbType.VarChar, 100).Value = (object?)bet.Liga ?? DBNull.Value;
            command.Parameters.Add("@partido", SqlDbType.VarChar, 100).Value = (object?)bet.Partido ?? DBNull.Value;
            command.Parameters.Add("@importe", SqlDbType.Decimal).Value = bet.Importe.HasValue ? bet.Importe.Value : DBNull.Value;
            command.Parameters.Add("@ganancia", SqlDbType.Decimal).Value = bet.Ganancia.HasValue ? bet.Ganancia.Value : DBNull.Value;
            command.Parameters.Add("@tipo", SqlDbType.VarChar, 1000).Value = (object?)bet.Tipo ?? DBNull.Value;
            command.Parameters.Add("@resultado", SqlDbType.VarChar, 2).Value = (object?)bet.Resultado ?? DBNull.Value;
            command.Parameters.Add("@cuota", SqlDbType.Decimal).Value = bet.Cuota.HasValue ? bet.Cuota.Value : DBNull.Value;
            command.Parameters.Add("@nota", SqlDbType.VarChar, 1000).Value = (object?)bet.Nota ?? DBNull.Value;
            command.Parameters.Add("@antesDurante", SqlDbType.VarChar, 1).Value = (object?)bet.AntesDurante ?? DBNull.Value;
            command.Parameters.Add("@tipster", SqlDbType.VarChar, 50).Value = (object?)bet.Tipster ?? DBNull.Value;
            var imageParameter = command.Parameters.Add("@imagen", SqlDbType.VarBinary, -1);
            imageParameter.Value = (object?)bet.Imagen ?? DBNull.Value;

            foreach (SqlParameter parameter in command.Parameters)
            {
                if (parameter.SqlDbType == SqlDbType.Decimal)
                {
                    parameter.Precision = 18;
                    parameter.Scale = 2;
                }
            }
        }
    }
}
