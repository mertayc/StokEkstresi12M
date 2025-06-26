using Dapper;
using Models.Dtos;
using Models.Entities;
using StokEkstresi.DataAccess.Abstracts;
using StokEkstresi.DataAccess.Concretes.Contexts;
using System.Data;

namespace StokEkstresi.DataAccess.Concretes.Repositories
{
    public class StiRepository : IStiRepository
    {
        private readonly DapperContext _dapperContext;

        public StiRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<IEnumerable<Sti?>> GetAllSTIS()
        {

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QueryAsync<Sti?>("SELECT * FROM Test.dbo.STI;");
            }
        }

        public async Task<List<Sti>?> GetStisByDateRangeAsync(int? startDate, int? finishDate)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var parameters = new { startDate = startDate, endDate = finishDate };

                var result = await connection.QueryAsync<Sti>(SqlQueries.CreateStiQueryWithDate(startDate, finishDate), parameters);

                return result.ToList(); 
            }
        }

        public async Task<List<StokEkstresiDto>?> GetStockReportAsync(int? startDate, int? finishDate, string malKodu)
        {
            using var connection = _dapperContext.CreateConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@StartDate", startDate, DbType.Int32);
            parameters.Add("@FinishDate", finishDate, DbType.Int32);
            parameters.Add("@MalKodu", malKodu, DbType.String);

            var result = await connection.QueryAsync<StokEkstresiDto>(
                "sp_GetStiWithCalculatedStock",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result.ToList();
        }
    }
}
