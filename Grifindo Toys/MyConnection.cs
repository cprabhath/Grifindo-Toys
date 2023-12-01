using System.Configuration;
using System.Data.SqlClient;

namespace Grifindo_Toys
{
    class MyConnection
    {
        public SqlConnection conn;
        public MyConnection()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Grifindo"].ConnectionString);
        }

        public static string type;
    }
}
