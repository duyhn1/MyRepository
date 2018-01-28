using System.Web.Configuration;
using System.Data.SqlClient;
namespace MyKintaiInsight.DataAccess
{
    public class MySqlDa
    {
        private static string connectionString;
        private static MySqlDa Instance = null;
        protected SqlConnection conn = new SqlConnection();
        private MySqlDa()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["DbConnection"].ToString();

        }
    }
}
