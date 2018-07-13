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
    /// Interaction logic for MiDESuggestions.xaml
    /// </summary>
    public partial class MiDESuggestions : Page
    {
        int i = 0;
        public MiDESuggestions()
        {
            Style style = this.FindResource("ButtonsPick") as Style;
            
            InitializeComponent();
            MiDEDataSet ds = ((MiDEDataSet)(FindResource("mideDataSet")));

            MiDEDataSetTableAdapters.MiDEBuildingsTableAdapter adapter = new MiDEDataSetTableAdapters.MiDEBuildingsTableAdapter();
            MiDEDataSetTableAdapters.MiDEPopulationTableAdapter padapter = new MiDEDataSetTableAdapters.MiDEPopulationTableAdapter();
            MiDEDataSetTableAdapters.MiDESValuesTableAdapter sadapter = new MiDEDataSetTableAdapters.MiDESValuesTableAdapter();
            MiDEDataSetTableAdapters.MiDEStrategyGroupsTableAdapter stadapter = new MiDEDataSetTableAdapters.MiDEStrategyGroupsTableAdapter();
            MiDEDataSetTableAdapters.MiDEEValuesTableAdapter eadapter = new MiDEDataSetTableAdapters.MiDEEValuesTableAdapter();

            adapter.Fill(ds.MiDEBuildings);
            padapter.Fill(ds.MiDEPopulation);
            sadapter.Fill(ds.MiDESValues);
            stadapter.Fill(ds.MiDEStrategyGroups);
            eadapter.Fill(ds.MiDEEValues);

            foreach(var row in ds.MiDEEValues)
            {
                string content = ds.MiDEEValues.Rows[i][1].ToString();
                MyPanel.Children.Add(new Button
                {

                    Style = style,
                    FontSize = 10,
                    
                    Content = content,
                    Margin = new Thickness(5)
                });
                i++;
                
            }
           
            /*or (var i = 0; i < 5; i++)
            {
                MyPanel.Children.Add(new Button
                {
                    Style = style,
                    Margin = new Thickness(5)
                });
            }*/
        }

        
    }
}
