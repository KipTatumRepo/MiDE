using MiDEWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static MiDEWPF.Pages.Home;

namespace MiDEWPF.ViewModel
{
    public class MenuItemViewModel
    {
        private readonly ICommand _command;
        string Value;
        
        public ObservableCollection<Cafe> Cafes
        {
            get;
            set;
        }

        public MenuItemViewModel()
        {
            _command = new CommandViewModel(Execute);
        }

        public string Header { get; set; }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

        public ICommand Command
        {
            get
            {
                return _command;
            }
        }

        public void Execute()
        {
            // (NOTE: In a view model, you normally should not use MessageBox.Show()).
            //MessageBox.Show("Clicked at " + Header);
            Value = Header;
           
        }

    }
}
