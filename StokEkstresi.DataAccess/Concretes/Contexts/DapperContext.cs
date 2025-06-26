using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StokEkstresi.DataAccess.Concretes.Contexts
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MSSQL");

            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new InvalidOperationException("Connection string 'MSSQL' bulunamadı veya boş. Lütfen appsettings.json dosyasını kontrol edin.");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    }
}
