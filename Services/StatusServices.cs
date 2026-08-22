using Dapper;
using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public class StatusServices{
        private readonly Database db;

        public StatusServices(Database database)
        {
            db = database;
        }

        //GET ALL STATUS ABSENSI
        public async Task<IEnumerable<StatusAbsensi>> GetAllStatusAbsensi()
        {
            using var conn = db.Connect();
            string sql = @"SELECT * FROM status_absensi";
            return await conn.QueryAsync<StatusAbsensi>(sql);
        }

        //GET ALL STATUS SETORAN HAFALAN
        public async Task<IEnumerable<StatusSetoranHafalan>> GetAllStatusSetoranHafalan()
        {
            using var conn = db.Connect();
            string sql = @"SELECT * FROM status_setoran_hafalan";
            return await conn.QueryAsync<StatusSetoranHafalan>(sql);
        }

        //GET ALL STATUS SHOLAT WAJIB
        public async Task<IEnumerable<StatusSholatWajib>> GetAllStatusSholatWajib()
        {
            using var conn = db.Connect();
            string sql = @"SELECT * FROM status_sholat_wajib";
            return await conn.QueryAsync<StatusSholatWajib>(sql);
        }
    }
}