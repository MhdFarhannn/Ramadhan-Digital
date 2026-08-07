using Ramadhan_Digital.Models;

namespace Ramadhan_Digital.Services
{
    public interface ISetoranHafalanService
    {
        Task<IEnumerable<SetoranHafalan>> GetAllAsync();
        Task<SetoranHafalan?> GetByIdAsync(int id);
        Task<IEnumerable<SetoranHafalan>> GetByUserIdAsync(int userId);
        Task<SetoranHafalan> CreateAsync(SetoranHafalan setoran);
        Task<bool> UpdateAsync(int id, SetoranHafalan setoran);
        Task<bool> DeleteAsync(int id);
    }
}