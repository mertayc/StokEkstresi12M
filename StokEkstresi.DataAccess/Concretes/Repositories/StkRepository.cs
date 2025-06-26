using Dapper;
using Models.Entities;
using StokEkstresi.DataAccess.Abstracts;
using StokEkstresi.DataAccess.Concretes.Contexts;

namespace StokEkstresi.DataAccess.Concretes.Repositories
{
    public class StkRepository : IStkRepository
    {

        private readonly DapperContext _dapperContext;

        public StkRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<List<Stk>?> GetAllStks()
        {

            using (var connection = _dapperContext.CreateConnection())
            {

                var result = await connection.QueryAsync<Stk>(SqlQueries.GetAllStkQuery());

                return result.ToList();
            }
        }
    }
}
