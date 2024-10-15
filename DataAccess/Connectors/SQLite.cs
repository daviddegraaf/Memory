using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace DataAccess.Connectors
{
    public class SQLite : IDisposable
    {
        private readonly SqliteConnection _connection;

        public SQLite(string databaseFile)
        {
            Batteries.Init();

            string connectionString = $"Data Source={databaseFile};";
            _connection = new SqliteConnection(connectionString);
            _connection.Open();
        }

        public int ExecuteNonQuery(string query, Dictionary<string, object>? parameters = null)
        {
            if (_connection == null) throw new NullReferenceException("No sqlite connection defined.");
            if (_connection.State != System.Data.ConnectionState.Open) _connection.Open();

            using var command = new SqliteCommand(query, _connection);
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
            }
            return command.ExecuteNonQuery();
        }

        public List<Dictionary<string, object>> ExecuteQuery(string query, Dictionary<string, object>? parameters = null)
        {
            if (_connection == null) throw new NullReferenceException("No sqlite connection defined.");
            if (_connection.State != System.Data.ConnectionState.Open) _connection.Open();

            var results = new List<Dictionary<string, object>>();

            using (var command = new SqliteCommand(query, _connection))
            {
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                 using SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[reader.GetName(i)] = reader.GetValue(i);
                    }
                    results.Add(row);
                }
            }

            return results;
        }

        public void Dispose()
        {
            _connection?.Close();
        }
    }
}
