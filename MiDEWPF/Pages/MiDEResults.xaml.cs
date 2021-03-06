﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

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

            //Here we are formatting the text in the exclusions list, if a mitigation is selected for removal then  . . .
            if (MiDESelection.MitigationSelection.Count >= 1 && Home.ExclusionBox.Count >= 1)
            {
                StrategyExclusionArray.Text = string.Join(", ", Home.ExclusionBox) + ", " + string.Join(", ", MiDESelection.MitigationSelection);
            }
            else if (MiDESelection.MitigationSelection.Count >= 1 && Home.ExclusionBox.Count <= 0)
            {
                StrategyExclusionArray.Text = string.Join(", ", MiDESelection.MitigationSelection);
            }
            else
            {
                StrategyExclusionArray.Text = string.Join(", ", Home.ExclusionBox);
            }

            MitigationDisplayArray.Text = string.Join(", ", MiDESelection.RemainingMitigationList); 

            SFactorTB.Text = "The S Factor Total Is " + Home.SValuesSum.ToString();
            SFactorTB.FontSize = 30;

            EValueDispaly.Text = "Your Selected Enhancements Have A Value Of  " + NewETotal.ToString();
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
                
                var uri = new Uri("pack://application:,,,/Resources/Demoji-thumbsup.png", UriKind.Absolute);

                ImageHolder.Source = new BitmapImage(uri);
            }
            else
            {
                Image.Text = "Not Quite The Result We Are Looking For";
                Image.FontSize = 32;
                
                var uri = new Uri("pack://application:,,,/Resources/Demoji-thumbsdown.png", UriKind.Absolute);

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
