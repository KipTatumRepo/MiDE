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
            SFactorDisplay.Text = "MiDE Calculated with the supplied parameters of ";
            SFactorDisplayArray.Text = string.Join(", ", Home.SelectionBox);
            SFactorTB.Text = "The S Factor total is " + Home.SValuesSum.ToString();
            MitigationDisplayArray.Text = string.Join(", ", MiDESelection.MitigationSelection);
            EValueDispaly.Text = "And Have a Enhancement Value of " + MiDESelection.EValuesSum.ToString();
            StrategyExclusionArray.Text = string.Join(", ", Home.ExclusionBox);
            ResultsTB.Text = "These selections will lower your S factor to " + (Home.SValuesSum - MiDESelection.EValuesSum).ToString();

            if(Home.SValuesSum - MiDESelection.EValuesSum <= 5)
            {
                var uri = new Uri("pack://application:,,,/thumbsUp.jpg", UriKind.Absolute);
                //Image myImage = new Image();
                //Image newimage = new Image();
                //BitmapImage bi = new BitmapImage();
                //bi.BeginInit();
                //bi.UriSource = new Uri("thumbsUp.JPG, UriKind.Relative");
                //bi.EndInit();
               

                ImageHolder.Source = new BitmapImage(uri);
            }
            else
            {
                SadImage.Text = "Try Harder";
                var uri = new Uri("pack://application:,,,/SadDonkey.png", UriKind.Absolute);
                //Image myImage = new Image();
                //Image newimage = new Image();
                //BitmapImage bi = new BitmapImage();
                //bi.BeginInit();
                //bi.UriSource = new Uri("thumbsUp.JPG, UriKind.Relative");
                //bi.EndInit();

                ImageHolder.Source = new BitmapImage(uri);
            }
        }
       
    }
}
