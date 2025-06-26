using Models.Dtos;
using Models.Entities;

namespace StokEkstresi.Business.Abstacts
{
    public interface IStokEkstresiService
    {
        Task<List<StokEkstresiDto>?> GetStokEkstresiAsync(DateTime? startDate, DateTime? finishDate, string malKodu);
        Task<List<Stk>?> GetStksAsync();
    }
}
