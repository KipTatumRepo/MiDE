using MiDEWPF.Models;
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
    /// Interaction logic for MiDESelection.xaml
    /// </summary>
    public partial class MiDESelection : Page
    {
        int j = 0;
        int i = 0;
        int k = 0;
        List<String> content = new List<string>();
        MiDEDataSet ds = new MiDEDataSet();
        List<int> Buttons = new List<int>();

        public MiDESelection()
        {
            Style style = FindResource("mButton") as Style;
            
            InitializeComponent();

            
            MiDEDataSetTableAdapters.MiDEEValuesTableAdapter eadapter = new MiDEDataSetTableAdapters.MiDEEValuesTableAdapter();
            eadapter.Fill(ds.MiDEEValues);

            foreach(var item in ds.MiDEEValues)
            {
                NewButton button = new NewButton();
                button.Content = ds.MiDEEValues.Rows[j][1].ToString();
                button.Bid = i;
                button.Style = style;
                mitigationDisplay.Children.Add(button); //new NewButton
                /*{
                    Content = ds.MiDEEValues.Rows[j][1].ToString(),
                    Style = style,
                    Margin = new Thickness(5),
                    Bid = i
                });*/
                j++;
                i++;
                content.Add(button.Content.ToString());
                Buttons.Add(button.Bid);
                
            }
            AddHandler(NewButton.ClickEvent, new RoutedEventHandler(button_Click));
            
        }

        

        void button_Click(object sender, RoutedEventArgs e)
        {
            string idk = e.OriginalSource.ToString();

            if(idk == "MiDEWPF.Models.NewButton: Food Truck Support")
            {
                msListBox.Items.Add(ds.MiDEEValues.Rows[0][1].ToString());
            }
            else if(idk == "MiDEWPF.Models.NewButton: Welcome Events")
            {
                msListBox.Items.Add(ds.MiDEEValues.Rows[17][1].ToString());
            }
            else if(idk == "MiDEWPF.Models.NewButton: Welcome Parties")
            {
                msListBox.Items.Add(ds.MiDEEValues.Rows[19][1].ToString());
            }
            /*if(Buttons == 0)
            {
                msListBox.Items.Add(ds.MiDEEValues.Rows[0][1].ToString());
            }
            else if(button.Bid == 1)
            {
                msListBox.Items.Add(ds.MiDEEValues.Rows[1][1].ToString());
            }
            //string add = button_Click.ToString();
            //MessageBox.Show(NewButton.Bid.ToString());*/

        }
    }
}
