using Utils.Helpers;

namespace StokEkstresi.DataAccess
{

    public class SqlQueries
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static string CreateStiQueryWithDate(int? startDate, int? endDate)
        {
            var queryBuilder = InitializeStiSelectQuery();

            if (startDate.HasValue || endDate.HasValue)
                queryBuilder.Where();

            if (startDate.HasValue && endDate.HasValue)
                queryBuilder.Column(DatabaseConstants.Col_Sti_Tarih).Between().Append("@startDate").And().Append("@endDate");

            else if (startDate.HasValue)
                queryBuilder.Column(DatabaseConstants.Col_Sti_Tarih).GreaterThanOrEqual().Append("@startDate");

            else if (endDate.HasValue)
                queryBuilder.Column(DatabaseConstants.Col_Sti_Tarih).LessThanOrEqual().Append("@endDate");

            return queryBuilder.OrderBy(DatabaseConstants.Col_Sti_Tarih).Asc().Build();
        }

        private static QueryBuilder InitializeStiSelectQuery()
        {
            return new QueryBuilder().Select().Asterisk().From(DatabaseConstants.Database_Test_Sti_Table);
        }

        public static string GetAllStkQuery()
        {
            return new QueryBuilder().Select().Asterisk().From(DatabaseConstants.Database_Test_Stk_Table).Build();
        }



    }
}
