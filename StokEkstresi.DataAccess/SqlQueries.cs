using Utils.Helpers;

namespace StokEkstresi.DataAccess
{

    public class SqlQueries
    {
        public static string SelectStisWithStartDateAndFinishDate()
        {
            QueryBuilder queryBuilder = new();
            queryBuilder.Clear();

            var query = queryBuilder.Select().Asterisk().From(DatabaseConstants.Database_Test_Sti_Table).Where()
                                    .Column(DatabaseConstants.Col_Sti_Tarih).Between().Append("@startDate").And().Append("@endDate")
                                    .OrderBy(DatabaseConstants.Col_Sti_Tarih).Asc().Build();
            return query;
        }

        public static string SelectStisWithStartDate()
        {
            QueryBuilder queryBuilder = new();
            queryBuilder.Clear();

            var query = queryBuilder.Select().Asterisk().From(DatabaseConstants.Database_Test_Sti_Table).Where()
                                    .Column(DatabaseConstants.Col_Sti_Tarih).GreaterThanOrEqual().Append("@startDate")
                                    .OrderBy(DatabaseConstants.Col_Sti_Tarih).Asc().Build();
            return query;
        }

        public static string SelectStisWithFinishDate()
        {
            QueryBuilder queryBuilder = new();
            queryBuilder.Clear();

            var query = queryBuilder.Select().Asterisk().From(DatabaseConstants.Database_Test_Sti_Table).Where()
                                    .Column(DatabaseConstants.Col_Sti_Tarih).LessThanOrEqual().Append("@endDate")
                                    .OrderBy(DatabaseConstants.Col_Sti_Tarih).Asc().Build();
            return query;
        }
    }
}
