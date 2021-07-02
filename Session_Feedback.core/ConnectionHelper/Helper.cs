using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Session_Feedback.core.ConnectionHelper
{
    public class Helper
    {
        
        public static IDbConnection OpenSession(string connectionString)
        {
            IDbConnection session = new SqlConnection(connectionString);
            session.Open();
            return session;
        }
    }
}
