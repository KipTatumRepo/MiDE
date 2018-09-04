using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiDEWPF.Resources
{
    public class ConnectionHelper
    {
        public static SqlConnection GetConn()
        {
            string connectionStr = "Data Source=compasspowerbi;Initial Catalog=MIDE;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionStr);
            return conn;
        }
    }
}
