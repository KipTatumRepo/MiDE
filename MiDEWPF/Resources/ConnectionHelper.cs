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
            string connectionStr = "Data Source=compasspowerbi;Initial Catalog=MiDE;Persist Security Info=True;User ID=MideApplication;Password=$1nCi7y";
            SqlConnection conn = new SqlConnection(connectionStr);
            return conn;
        }
    }
}
