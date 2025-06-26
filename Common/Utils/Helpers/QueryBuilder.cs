using System.Text;

namespace Utils.Helpers
{
    public class QueryBuilder
    {
        private readonly StringBuilder stringBuilder = new();

        public int Length => stringBuilder.Length;

      

        public QueryBuilder Asterisk()
        {
            stringBuilder.Append(" * ");

            return this;
        }
        public QueryBuilder And()
        {
            stringBuilder.Append(" AND ");

            return this;
        }
        public QueryBuilder As()
        {
            stringBuilder.Append(" AS ");

            return this;
        }

        public QueryBuilder Asc()
        {
            stringBuilder.Append(" ASC ");

            return this;
        }
        public QueryBuilder Between()
        {
            stringBuilder.Append(" BETWEEN ");

            return this;
        }

        public QueryBuilder Clear()
        {
            stringBuilder.Clear();

            return this;
        }

        public QueryBuilder Column(string column)
        {
            stringBuilder.Append($" {column} ");
            
            return this;
        }

        public QueryBuilder From(string tableName)
        {
            stringBuilder.Append($" FROM {tableName} ");
            return this;
        }

        public QueryBuilder Where(string condition)
        {
            stringBuilder.Append($" WHERE {condition} ");
            return this;
        }
        public QueryBuilder Where()
        {
            stringBuilder.Append($" WHERE ");
            return this;
        }

        public QueryBuilder OrderBy(string columns)
        {
            stringBuilder.Append($" ORDER BY {columns} ");
            return this;
        }

        public string Build()
        {
            var query = stringBuilder.ToString().Trim();

            if (!query.EndsWith(";"))
                query += ";";

            return query;
        }

        public QueryBuilder Select()
        {
            stringBuilder.Append("SELECT ");

            return this;
        }

        public QueryBuilder Append(string value)
        {
            stringBuilder.Append(" " + value + " ");
            return this;
        }

        public QueryBuilder GreaterThanOrEqual()
        {
            stringBuilder.Append(" >= ");
            return this;
        }

        public QueryBuilder LessThanOrEqual()
        {
            stringBuilder.Append(" <= ");
            return this;
        }
    }
}
