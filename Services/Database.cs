using Npgsql;

namespace Ramadhan_Digital.Services
{
    public class Database
    {
        private readonly string _connectionString = Env.Value["Database:connection"]!;

        public NpgsqlConnection Connect()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
