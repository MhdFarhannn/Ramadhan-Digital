using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class AuthServices
    {
        private readonly Database db;

        public AuthServices(Database database)
        {
            db = database;
        }

        public async Task<bool> Register(User user)
        {
            using var conn = db.Connect();

                string sql = @"
                    INSERT INTO users
                    (id_role, id_kelas, nama, username, password)
                    VALUES
                    (@IdRole, @IdKelas, @Nama, @Username, @Password);
                ";

            var result = await conn.ExecuteAsync(sql, user);

            return result > 0;
        }

        public async Task<bool> IsRegistered()
        {
            using var conn = db.Connect();

            string sql = "SELECT COUNT(*) FROM users";

            int count = await conn.ExecuteScalarAsync<int>(sql);

            return count > 0;
        }

        public async Task<User?> Login(string username)
        {
            using var conn = db.Connect();

            string sql = @"
        SELECT u.*, r.id, r.name
        FROM users u
        LEFT JOIN roles r ON u.id_role = r.id
        WHERE u.username = @username";

            var result = await conn.QueryAsync<User, Role, User>(
                sql,
                (u, r) =>
                {
                    u.Role = r;
                    return u;
                },
                new { username },
                splitOn: "id"
            );

            return result.FirstOrDefault();
        }

    }
}
