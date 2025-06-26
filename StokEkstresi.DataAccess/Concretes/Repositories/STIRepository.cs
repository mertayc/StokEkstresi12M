using Dapper;
using Models.Entities;
using StokEkstresi.DataAccess.Abstracts;
using StokEkstresi.DataAccess.Concretes.Contexts;

namespace StokEkstresi.DataAccess.Concretes.Repositories
{
    public class STIRepository : ISTIRepository
    {
        private readonly DapperContext _dapperContext;

        public STIRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<STI?>> GetAllSTIS()
        {

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<STI?>("SELECT * FROM Test.dbo.STI;");
            }
        }

        public async Task<List<STI?>> GetStiWith()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var parameters = new { startDate = 0, endDate = 42450 };

                var result = await connection.QueryAsync<STI>(SqlQueries.SelectStisWithStartDateAndFinishDate(), parameters);

               
            }
        }
    }
}
