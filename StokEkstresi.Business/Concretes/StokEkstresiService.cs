using StokEkstresi.Business.Abstacts;
using StokEkstresi.DataAccess.Abstracts;
using Utils.Helpers.Conversion;

namespace StokEkstresi.Business.Concretes
{
    public class StokEkstresiService : IStokEkstresiService
    {
        private readonly ISTIRepository _sTIRepository;

        public StokEkstresiService(ISTIRepository sTIRepository)
        {
            _sTIRepository = sTIRepository;
        }

        public async Task GetStiWithDate(DateTime? startDate, DateTime? finishDate)
        {
            int startDateInt = 0;
            int finishDateInt = 0;

            if (startDate.HasValue)
                startDateInt = DateTimeConversionHelper.ConvertToIntFromDateTime(startDate.Value);

            if (finishDate.HasValue)
                finishDateInt = DateTimeConversionHelper.ConvertToIntFromDateTime(finishDate.Value);

          


        }
        public async Task xx()
        {
            var result = await _sTIRepository.GetAllSTIS();
        }
    }
}
