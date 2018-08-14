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
using MiDEWPF.Resources;

namespace MiDEWPF.Pages
{
    /// <summary>
    /// Interaction logic for MiDEResults.xaml
    /// </summary>
    public partial class MiDEResults : Page
    {
        List<string> Values = new List<string>();
        int NewETotal = MiDESelection.AllEValueSum - MiDESelection.EValuesSum;
       

        public MiDEResults()
        {

            InitializeComponent();

            ScenarioNumberDispaly.Text = "For Scenario # " + MiDESelection.ScenarioNumber;
            ScenarioNumberDispaly.FontSize = 30;
            SFactorDisplay.Text = "MiDE Calculated With The Supplied Parameters Of ";
            SFactorDisplay.FontSize = 30;
            SFactorDisplayArray.Text = string.Join(", ", Home.SelectionBox);
            StrategyExclusionArray.Text = string.Join(", ", Home.ExclusionBox);
            MitigationDisplayArray.Text = string.Join(", ", MiDESelection.RemainingMitigationList); 

            SFactorTB.Text = "The S Factor total is " + Home.SValuesSum.ToString();
            SFactorTB.FontSize = 30;

            EValueDispaly.Text = "Which Have An Enhancement Value of " + NewETotal.ToString();
            EValueDispaly.FontSize = 30;

            int SFactorResult = Home.SValuesSum - NewETotal;
            if (SFactorResult < 0)
            {
                SFactorResult = 0;
            }

            ResultsTB.Text = "These Selections Will Lower Your S Factor To " + SFactorResult.ToString();
            ResultsTB.FontSize = 30;

            if (Home.SValuesSum - NewETotal <= 5)
            {
                Image.Text = "Great Job";
                Image.FontSize = 30;
                var uri = new Uri("pack://application:,,,/thumbsUp.jpg", UriKind.Absolute);

                ImageHolder.Source = new BitmapImage(uri);
            }
            else
            {
                Image.Text = "Not Quite The Result We Are Looking For";
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
