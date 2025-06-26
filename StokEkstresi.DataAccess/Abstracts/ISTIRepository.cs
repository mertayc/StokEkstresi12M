using Models.Dtos;
using Models.Entities;

namespace StokEkstresi.DataAccess.Abstracts
{
    public interface IStiRepository
    {
        Task<IEnumerable<Sti?>> GetAllSTIS();
        Task<List<Sti>?> GetStisByDateRangeAsync(int? startDate, int? finishDate);
        Task<List<StokEkstresiDto>?> GetStockReportAsync(int? startDate, int? finishDate, string malKodu);
    }
}
