using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDataPLC
{
    public class DBConnect
    {
        //public static string connection_string = "Data Source=DESKTOP-NP52VJJ\\SQLEXPRESS;Initial Catalog=DataQCCheck;Integrated Security=True";


        public static string connection_string = "Data Source=DESKTOP-CEVMTEM\\SQLEXPRESS;Initial Catalog=DataQCCheck;Integrated Security=True";

        public static DataTable StoreFillDS(string query_object, CommandType type, params object[] obj)
        {
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query_object, conn);
                cmd.CommandType = type;
                SqlCommandBuilder.DeriveParameters(cmd);
                for (int i = 1; i <= obj.Length; i++)
                {
                    cmd.Parameters[i].Value = obj[i - 1];
                }
                SqlDataAdapter dap = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                dap.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }
        public static object getscalra(string query_object, CommandType type, params object[] obj)
        {
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                Object data;
                conn.Open();
                SqlCommand cmd = new SqlCommand(query_object, conn);
                cmd.CommandType = type;
                SqlCommandBuilder.DeriveParameters(cmd);
                for (int i = 1; i <= obj.Length; i++)
                {
                    cmd.Parameters[i].Value = obj[i - 1];
                }
                data = cmd.ExecuteScalar();
                conn.Close();
                return data;
            }
        }
        public static object excutenonquery(string query_object, CommandType type, params object[] obj)
        {
            using (SqlConnection conn = new SqlConnection(connection_string))
            {
                Object data;
                conn.Open();
                SqlCommand cmd = new SqlCommand(query_object, conn);
                cmd.CommandType = type;
                SqlCommandBuilder.DeriveParameters(cmd);
                for (int i = 1; i <= obj.Length; i++)
                {
                    cmd.Parameters[i].Value = obj[i - 1];
                }
                data = cmd.ExecuteNonQuery();
                conn.Close();
                return data;
            }
        }
    }
}
