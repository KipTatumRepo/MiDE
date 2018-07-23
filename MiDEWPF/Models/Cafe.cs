using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiDEWPF.Models
{
    public class Cafe
    {
        private string cafeName;
        private int uid;

        public int Uid
        {
            get
            {
                return uid;
            }

            set
            {
                uid = value;
                RaisePropertyChanged("Uid");
            }
        }

        public string CafeName
        {
            get
            {
                return cafeName;
            }
            set
            {
                cafeName = value;
                RaisePropertyChanged("CafeName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}

