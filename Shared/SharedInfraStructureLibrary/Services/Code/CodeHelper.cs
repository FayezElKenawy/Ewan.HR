using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SharedCoreLibrary.Application.Services;

namespace SharedInfraStructureLibrary.Services
{
    public class CodeHelper : ICodeHelper
    {
        private readonly IConfiguration _config;
        public CodeHelper(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string NewDN(string table_name, int totalNoDigits = 3, string prefix = "", string keyName = "Id")
        {
            const int first = 1;
            string Id = "";
            string sqlQuery = $@"
                         Select SUBSTRING(CAST({keyName} AS NVARCHAR), {(prefix.Length > 0 ? prefix.Length + 1 : 0)},{totalNoDigits - prefix.Length})
                          From   {table_name}
                         Where LEN({keyName})={totalNoDigits}";

            Id = ExcureQuery(sqlQuery);
            Id = prefix + (!string.IsNullOrEmpty(Id) ? (int.Parse(Id) + 1).ToString($@"D{totalNoDigits - prefix.Length}") : first.ToString($@"D{(totalNoDigits - prefix.Length).ToString()}"));

            return Id;
        }
        public string NewYYMMDN(string table_name, DateTime date, int noDigits = 4, string keyName = "Id")
        {
            const int first = 1;
            string Id = "";
            string sqlQuery = "";

            string yearMonth = date.Year.ToString().Substring(2) + date.Month.ToString("D2");
            sqlQuery = $@"
                            Select CAST(MAX({keyName}) AS nvarchar)
                            From   {table_name}  with (TABLOCK,XLOCK)
                            Where 
                            {keyName} Like '{yearMonth}%' And LEN({keyName})={noDigits} +LEN({yearMonth}) ";

            Id = ExcureQuery(sqlQuery);
            Id = !string.IsNullOrEmpty(Id) ? (long.Parse(Id) + 1).ToString($@"D{noDigits}") : yearMonth + first.ToString($@"D{(noDigits).ToString()}");
            return Id;
        }
        private string ExcureQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(_config.GetSection("ConnectionStrings:FinanceConnection").Value))
            {
                string result = "";
                SqlCommand command = new(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        result = reader[0].ToString();
                    }
                }
                finally
                {
                    reader.Close();
                }

                return result;
            }
        }

    }
}
