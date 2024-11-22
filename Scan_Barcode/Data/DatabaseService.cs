using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Scan_Barcode.Data
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MOPDatabase");
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Metodo di esempio per eseguire una query SELECT
        public List<Dictionary<string, object>> ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            var result = new List<Dictionary<string, object>>();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    // Aggiungi parametri alla query, se presenti
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }
                            result.Add(row);
                        }
                    }
                }
            }
            return result;
        }
        

        // Metodo di esempio per eseguire una query INSERT, UPDATE, DELETE
        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }
        public async Task<object> ExecuteScalarAsync(string query, Dictionary<string, object> parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

                await connection.OpenAsync();
                return await command.ExecuteScalarAsync();
            }
        }
        public async Task<int> ExecuteNonQueryAsync(string query, Dictionary<string, object> parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }
        public async Task<List<T>> QueryAsync<T>(string query, Dictionary<string, object> parameters = null)
        {
            var result = new List<T>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                // Aggiungi parametri alla query, se presenti
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    // Ottieni le proprietà della classe T
                    var properties = typeof(T).GetProperties();

                    while (await reader.ReadAsync())
                    {
                        // Crea un'istanza di T per ogni riga
                        var item = Activator.CreateInstance<T>();

                        foreach (var property in properties)
                        {
                            // Cerca se la proprietà corrisponde a una colonna del reader
                            if (reader.HasColumn(property.Name))
                            {
                                var value = reader[property.Name];
                                if (value != DBNull.Value)
                                {
                                    property.SetValue(item, value);
                                }
                            }
                        }

                        result.Add(item);
                    }
                }
            }

            return result;
        }

        

        
    }
}

public static class SqlDataReaderExtensions
{
    public static bool HasColumn(this SqlDataReader reader, string columnName)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}
