using System.Web.Configuration;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MyKintaiInsight.DataAccess
{
    public class MySqlDa
    {
        private static string connectionString;
        private static MySqlDa _instance = null;
        private MySqlConnection conn;
        private MySqlDa()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["DbConnection"].ToString();
            conn = new MySqlConnection(connectionString);
        }

        public static MySqlDa GetInstance()
        {
            if (_instance == null)
            {
                _instance = new MySqlDa();
            }
            return _instance;
        }
        public int ExecuteNonQuery(string query)
        {
            return ExecuteNonQuery(query, CommandType.Text, null);
        }
        public int ExecuteNonQuery(string query, CommandType cmdType, List<MySqlParameter> paras)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = cmdType;
                foreach(MySqlParameter para in paras)
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
