using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Session_Feedback.core.ConnectionHelper
{
    public class Helper
    {
        private static string _connectionString;

        public Helper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("connection_string");
        }

        public static IDbConnection OpenSession()
        {
            IDbConnection session = new SqlConnection(_connectionString);

            return session;
        }
    }
}
