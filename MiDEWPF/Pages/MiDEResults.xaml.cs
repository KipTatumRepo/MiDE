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
        List<string> Values = new List<string>();
        public MiDEResults()
        {
            

            InitializeComponent();

            ScenarioNumberDispaly.Text = "For Scenario # " + MiDESelection.ScenarioNumber;
            ScenarioNumberDispaly.FontSize = 32;
            SFactorDisplay.Text = "MiDE Calculated With The Supplied Parameters Of ";
            SFactorDisplay.FontSize = 32;
            SFactorDisplayArray.Text = string.Join(", ", Home.SelectionBox);
            SFactorTB.Text = "The S Factor total is " + Home.SValuesSum.ToString();
            SFactorTB.FontSize = 32;
            MitigationDisplayArray.Text = string.Join(", ", MiDESelection.MitigationSelection);
            EValueDispaly.Text = "Which Have An Enhancement Value of " + MiDESelection.EValuesSum.ToString();
            EValueDispaly.FontSize = 32;
            StrategyExclusionArray.Text = string.Join(", ", Home.ExclusionBox);
            ResultsTB.Text = "These Selections Will Lower Your S Factor To " + (Home.SValuesSum - MiDESelection.EValuesSum).ToString();
            ResultsTB.FontSize = 32;

            if (Home.SValuesSum - MiDESelection.EValuesSum <= 5)
            {
                Image.Text = "Great Job";
                Image.FontSize = 32;
                var uri = new Uri("pack://application:,,,/thumbsUp.jpg", UriKind.Absolute);
                
                ImageHolder.Source = new BitmapImage(uri);
            }
            else
            {
                Image.Text = "Try Harder";
                Image.FontSize = 32;
                var uri = new Uri("pack://application:,,,/SadDonkey.png", UriKind.Absolute);
                
                ImageHolder.Source = new BitmapImage(uri);
                
            }
        }

        private void NewScenario_Click(object sender, RoutedEventArgs e)
        {
            Home.SelectionBox.Clear();
            Home.SValuesSum = 0;
            MiDESelection.MitigationSelection.Clear();
            MiDESelection.EValuesSum = 0;
            Home.ExclusionBox.Clear();

            NavigationService.Navigate(
                new Uri("Pages/Home.xaml", UriKind.Relative));
        }
    }
}
