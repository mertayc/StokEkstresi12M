using StokEkstresi.Business.Abstacts;
using StokEkstresi.DataAccess.Abstracts;

namespace StokEkstresi.Business.Concretes
{
    public class StokEkstresiService : IStokEkstresiService
    {
        private readonly ISTIRepository _sTIRepository;

        public StokEkstresiService(ISTIRepository sTIRepository)
        {
            _sTIRepository = sTIRepository;
        }


        public async Task xx()
        {
          var result = await _sTIRepository.GetAllSTIS();
        }
    }
}
