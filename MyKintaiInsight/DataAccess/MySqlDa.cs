using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace MyKintaiInsight.DataAccess
{
    public class SqlDa
    {
        private static string connectionString;
        private static SqlDa _instance = null;
        private SqlConnection conn;
        private SqlDa()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["DbConnection"].ToString();
            conn = new SqlConnection(connectionString);
        }

        public static SqlDa GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SqlDa();
            }
            return _instance;
        }
        public int ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, CommandType.Text, null);
        }
        public int ExecuteNonQuery(string query, CommandType cmdType, List<SqlParameter> paras)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = cmdType;
                foreach(SqlParameter para in paras)
                {
                    cmd.Parameters.Add(para);
                }
                var rslt = cmd.ExecuteNonQuery();
                return rslt;
            }
            finally 
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}
