using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiDEWPF.Models
{
    public class Population
    {
        private string popAmount;
        
        public string PopAmount
        {
            get
            {
                return popAmount ;
            }
            set
            {
                popAmount = value;
                RaisePropertyChanged("PopAmount");
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
