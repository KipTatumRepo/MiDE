using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MiDEWPF.Models;
using MiDEWPF.Resources;
using System.Data.SqlClient;

namespace MiDEWPF.ViewModel
{
    public class CafeViewModel
    {
        SqlCommand cmd;
        SqlDataReader reader;
        public ObservableCollection<Cafe> Cafes
        {
            get;
            set;
        }

        public void LoadCafes()
        {
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();
            string sqlString = "SELECT CafeName FROM MiDECafes";
            cmd = new SqlCommand(sqlString, conn);
            reader = cmd.ExecuteReader();

            ObservableCollection<string> cafes = new ObservableCollection<string>();
            while (reader.HasRows)
            {
                string value = reader.GetValue(1).ToString();

                cafes.Add(value);
            }

            //Cafes = reader;
            //conn.Close();
            /*ObservableCollection<Cafe> cafes = new ObservableCollection<Cafe>();

            cafes.Add(new Cafe { CafeName = "H" });
            cafes.Add(new Cafe { CafeName = "83" });
            cafes.Add(new Cafe { CafeName = "40/41" });
            cafes.Add(new Cafe { CafeName = "Millenium" });
            cafes.Add(new Cafe { CafeName = "Samm-C" });

            Cafes = cafes;*/
        }
        
    }
}
