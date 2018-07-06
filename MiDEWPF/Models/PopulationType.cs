using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiDEWPF.Models
{
    public class PopulationType
    {
        private string populationType;

        public string PopType
        {
            get
            {
                return populationType;
            }
            set
            {
                populationType = value;
                RaisePropertyChanged("PopType");
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
