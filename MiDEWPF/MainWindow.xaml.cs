using MiDEWPF.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiDEWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DateTime now = DateTime.Now;
        private string userId;
        private DateTime sessionStart;
        private DateTime sessionEnd;

        public MainWindow()
        {
            InitializeComponent();
            userId = Environment.UserName;
            sessionStart = DateTime.Now;
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MiDEDataSetTableAdapters.ApplicationSessionsTableAdapter infoAdapter = new MiDEDataSetTableAdapters.ApplicationSessionsTableAdapter();
            infoAdapter.Insert(Properties.Settings.Default.ApplicationName, userId, sessionStart, null);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sessionEnd = DateTime.Now;
            MiDEDataSetTableAdapters.ApplicationSessionsTableAdapter infoAdapter = new MiDEDataSetTableAdapters.ApplicationSessionsTableAdapter();
            infoAdapter.UpdateQuery(sessionEnd, sessionStart, sessionStart);
            
            BiMessageBox.Show("Close MiDE?");
        }

        
    }
}
