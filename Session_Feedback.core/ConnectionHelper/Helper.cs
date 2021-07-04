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
            if(session.State == ConnectionState.Closed)
            {
                session.Open();
            }
            return session;
        }
    }
}
