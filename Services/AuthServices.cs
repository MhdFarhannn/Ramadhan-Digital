using Dapper;
using Ramadhan_Digital.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ramadhan_Digital.Services
{
    public class AuthServices
    {
        private readonly Database db;

        public AuthServices(Database database)
        {
            db = database;
        }
        // ===============
        // REGISTER ADMIN
        // ===============
        public async Task<bool> RegisterAdmin(User user)
        {
            using var conn = db.Connect();

                string sql = @"
                    INSERT INTO users
                    (id_role, id_kelas, nama, username, password)
                    VALUES
                    (@IdRole, @IdKelas, @Nama, @Username, @Password);
                ";

            var result = await conn.ExecuteAsync(sql, new
            {
                IdRole = 1,
                IdKelas =1,
                Nama = user.Nama,
                Username = user.Username,
                Password = user.Password
            });

            return result > 0;
        }

        public async Task<bool> IsRegistered()
        {
            using var conn = db.Connect();

            string sql = "SELECT COUNT(*) FROM users";

            int count = await conn.ExecuteScalarAsync<int>(sql);

            return count > 0;
        }

        // ===============
        // LOGIN
        // ===============

        public async Task<User?> Login(string username)
        {
            using var conn = db.Connect();

            string sql = @"
         SELECT u.id, u.nama AS Nama, u.username AS Username, u.password AS Password,
        r.Name as Role,k.Nama as Kelas, k.id as idKelas
 FROM users u
 JOIN role r ON u.id_role = r.id
 JOIN kelas k ON u.id_kelas = k.id
 WHERE u.username = @username
";

            var ReturnUser = await conn.QueryFirstOrDefaultAsync<User>(sql, new { username = username });
            return ReturnUser;
        }

        public async Task<bool> RegisterUser(User user)
        {
            using var conn = db.Connect();
            string sql = @"
                INSERT INTO users
                (id_role, id_kelas, nama, username, password)
                VALUES
                (@IdRole, @IdKelas, @Nama, @Username, @Password);
            ";
            var result = await conn.ExecuteAsync(sql, new
            {
                IdRole = 3,
                IdKelas = user.IdKelas,
                Nama = user.Nama,
                Username = user.Username,
                Password = user.Password
            });
            return result > 0;
        }
        
        public async Task<bool> registerGuru(User user)
        {
            using var conn = db.Connect();
            string sql = @"
                INSERT INTO users
                (id_role, nama, username, password)
                VALUES
                (@IdRole, @Nama, @Username, @Password);
            ";
            var result = await conn.ExecuteAsync(sql, new
            {
                IdRole = 2,
                Kelas = 10,
                Nama = user.Nama,
                Username = user.Username,
                Password = user.Password
            });
            return result > 0;
        }

        // ===============
        // REGISTER SISWA
        // ===============
        public async Task<bool> RegisterSiswa(User user)
        {
            using var conn = db.Connect();
            string sql = @"
        INSERT INTO users
        (id_role, id_kelas, nama, username, password)
        VALUES
        (@IdRole, @IdKelas, @Nama, @Username, @Password);
    ";

            var result = await conn.ExecuteAsync(sql, new
            {
                IdRole = 3, // Role ID untuk Siswa
                IdKelas = user.IdKelas,
                Nama = user.Nama,
                Username = user.Username,
                Password = user.Password
            });

            return result > 0;
        }

        // ===============
        // REGISTER GURU
        // ===============
        
        public async Task<bool> RegisterGuru(User user)
        {
            using var conn = db.Connect();
            string sql = @"
        INSERT INTO users
        (id_role, id_kelas, nama, username, password)
        VALUES
        (@IdRole, @IdKelas, @Nama, @Username, @Password);
    ";

            var result = await conn.ExecuteAsync(sql, new
            {
                IdRole = 2, // Role ID untuk Guru
                IdKelas = 10, // ID Kelas default
                Nama = user.Nama,
                Username = user.Username,
                Password = user.Password
            });

            return result > 0;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            using var conn = db.Connect();
            string sql = @"
        SELECT u.id, u.nama, u.username, r.Name AS Role, k.Nama AS Kelas
        FROM users u
        LEFT JOIN role r ON u.id_role = r.id
        LEFT JOIN kelas k ON u.id_kelas = k.id;
    ";
            return await conn.QueryAsync<UserDTO>(sql);
        }

        public async Task<bool> DeleteUser(int id)
        {
            using var conn = db.Connect();
            string sql = "DELETE FROM users WHERE id = @id";
            int result = await conn.ExecuteAsync(sql, new { id });
            return result > 0;
        }
        
        //PUT KELAS GURU
        public async Task<bool> UpdateKelasGuru(int id, int idKelas)
        {
            using var conn = db.Connect();
            string sql = "UPDATE users SET id_kelas = @idKelas WHERE id = @id";
            int result = await conn.ExecuteAsync(sql, new { idKelas, id });
            return result > 0;
        }

        // GET ALL SISWA
        public async Task<IEnumerable<UserDTO>> GetAllSiswa()
        {
            using var conn = db.Connect();
            string sql = @"
        SELECT u.id, u.nama, u.username, r.Name AS Role, k.Nama AS Kelas
        FROM users u
        LEFT JOIN role r ON u.id_role = r.id
        LEFT JOIN kelas k ON u.id_kelas = k.id
        WHERE u.id_role = 3;
    ";
            return await conn.QueryAsync<UserDTO>(sql);
        }

        //GET ALL GURU
        public async Task<IEnumerable<UserDTO>> GetAllGuru()
        {
            using var conn = db.Connect();
            string sql = @"
        SELECT u.id, u.nama, u.username, r.Name AS Role, k.Nama AS Kelas
        FROM users u
        LEFT JOIN role r ON u.id_role = r.id
        LEFT JOIN kelas k ON u.id_kelas = k.id
        WHERE u.id_role = 2;
    ";
            return await conn.QueryAsync<UserDTO>(sql);
        }

        public async Task<bool> UpdateUser(
            int id,
            string? nama,
            string? username,
            int? idKelas,
            string? password,
            IPasswordService passwordService)
        {
            using var conn = db.Connect();
        
            var updates = new List<string>();
            var parameters = new DynamicParameters();
        
            parameters.Add("id", id);
        
            // Update nama jika dikirim
            if (!string.IsNullOrWhiteSpace(nama))
            {
                updates.Add("nama = @nama");
                parameters.Add("nama", nama);
            }
        
            // Update username jika dikirim
            if (!string.IsNullOrWhiteSpace(username))
            {
                updates.Add("username = @username");
                parameters.Add("username", username);
            }
        
            // Update kelas jika dikirim
            if (idKelas.HasValue)
            {
                updates.Add("id_kelas = @idKelas");
                parameters.Add("idKelas", idKelas.Value);
            }
        
            // Update password jika dikirim
            if (!string.IsNullOrWhiteSpace(password))
            {
                var hashedPassword = passwordService.HashPassword(password);
        
                updates.Add("password = @password");
                parameters.Add("password", hashedPassword);
            }
        
            // Tidak ada id_role di sini.
            // Jadi id_role tidak dapat diubah melalui PATCH.
        
            if (updates.Count == 0)
            {
                return false;
            }
        
            string sql = $"""
                UPDATE users
                SET {string.Join(", ", updates)}
                WHERE id = @id
                """;
        
            int result = await conn.ExecuteAsync(sql, parameters);
        
            return result > 0;
        }



    }
}
