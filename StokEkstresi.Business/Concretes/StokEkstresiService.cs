using Models.Dtos;
using Models.Entities;
using StokEkstresi.Business.Abstacts;
using StokEkstresi.DataAccess.Abstracts;
using Utils.Helpers.Conversion;

namespace StokEkstresi.Business.Concretes
{
    public class StokEkstresiService : IStokEkstresiService
    {
        private readonly IStiRepository _stiRepository;
        private readonly IStkRepository _stkRepository;

        public StokEkstresiService(IStiRepository sTIRepository, IStkRepository stkRepository)
        {
            _stiRepository = sTIRepository;
            _stkRepository = stkRepository;
        }

        public async Task<List<StokEkstresiDto>?> GetStokEkstresiAsync(DateTime? startDate, DateTime? finishDate, string malKodu)
        {
            int? startDateInt = null;
            int? finishDateInt = null;

            if (startDate.HasValue)
                startDateInt = DateTimeConversionHelper.ConvertToIntFromDateTime(startDate.Value);

            if (finishDate.HasValue)
                finishDateInt = DateTimeConversionHelper.ConvertToIntFromDateTime(finishDate.Value);

            List<StokEkstresiDto>? stokEkstresiDtos = await _stiRepository.GetStockReportAsync(startDateInt, finishDateInt, malKodu);

            return stokEkstresiDtos;
        }

        public async Task<List<Stk>?> GetStksAsync()
        {
            return await _stkRepository.GetAllStks();
        }

    }
}
