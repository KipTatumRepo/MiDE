using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MiDEWPF.Models
{
    public class ItemMenu : MenuItem
    {
        public ObservableCollection<MenuItem> SubItems { get; set; }
        public new string Header { get; set; }

        public void ItemsMenu()
        {
            SubItems = new ObservableCollection<MenuItem>();
        }
        
    }
}
