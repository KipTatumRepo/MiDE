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

namespace MiDEWPF.Pages
{
    /// <summary>
    /// Interaction logic for MiDEResults.xaml
    /// </summary>
    public partial class MiDEResults : Page
    {
        public MiDEResults()
        {
            InitializeComponent();

            SFactorTB.Text = Home.SValuesSum.ToString();
            EValueTB.Text = MiDESelection.EValuesSum.ToString();
            ResultsTB.Text = (Home.SValuesSum - MiDESelection.EValuesSum).ToString();

        }
    }
}
