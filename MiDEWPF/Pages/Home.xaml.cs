using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using MiDEWPF.Models;
using MiDEWPF.ViewModel;

namespace MiDEWPF.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        List<string> SelectionBox = new List<string>();
        List<string> ExclusionBox = new List<string>();
        int i = 1;
        public int ScenarioNumber;

        public Home()
        {

            InitializeComponent();

            MiDEDataSet ds = ((MiDEDataSet)(FindResource("mideDataSet")));

            MiDEDataSetTableAdapters.MiDEBuildingsTableAdapter adapter = new MiDEDataSetTableAdapters.MiDEBuildingsTableAdapter();
            MiDEDataSetTableAdapters.MiDEPopulationTableAdapter padapter = new MiDEDataSetTableAdapters.MiDEPopulationTableAdapter();
            MiDEDataSetTableAdapters.MiDEPopTypeTableAdapter ptadapter = new MiDEDataSetTableAdapters.MiDEPopTypeTableAdapter();
            MiDEDataSetTableAdapters.MiDESValuesTableAdapter sadapter = new MiDEDataSetTableAdapters.MiDESValuesTableAdapter();
            MiDEDataSetTableAdapters.MiDEStrategyGroupsTableAdapter stadapter = new MiDEDataSetTableAdapters.MiDEStrategyGroupsTableAdapter();
            MiDEDataSetTableAdapters.MiDEEValuesTableAdapter eadapter = new MiDEDataSetTableAdapters.MiDEEValuesTableAdapter();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();

            adapter.Fill(ds.MiDEBuildings);
            padapter.Fill(ds.MiDEPopulation);
            ptadapter.Fill(ds.MiDEPopType);
            sadapter.Fill(ds.MiDESValues);
            stadapter.Fill(ds.MiDEStrategyGroups);
            eadapter.Fill(ds.MiDEEValues);

            DataTable last;
            last = wadapter.GetDataByLast();
            int lastvalue = (int)last.Rows[0][1];
            ScenarioNumber = lastvalue + 1;

        }
        //int realLastValue = int(lastvalue);
        //TODO Figure out how to implement Menu style box for this menu 
      

        //buildingMenu.DataContext = this;

        
 

        /*private void selectedVacatingBuildingCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = selectedVacatingBuildingCB.SelectedValue.ToString();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();

            selectedVacatingBuildingCB.Items.Add(add);
            wadapter.Insert(ScenarioNumber, add, null, null);

            SelectionBox.Add(add);
        }

        private void selectedPopRangeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = selectedPopRangeCB.SelectedValue.ToString();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();

            selectionListBox.Items.Add(add);
            wadapter.Insert(ScenarioNumber, add, null, null);

            SelectionBox.Add(add);
        }

        private void selectedPopTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = selectBuildingCB.SelectedValue.ToString();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();

            selectedPopTypeCB.Items.Add(add);
            wadapter.Insert(ScenarioNumber, add, null, null);

            SelectionBox.Add(add);
        }*/

        private void selectBuildingCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = selectBuildingCB.SelectedValue.ToString();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();

            selectionListBox.Items.Add(add);
            wadapter.Insert(ScenarioNumber, add, null, null);

            SelectionBox.Add(add);
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = datePicker.SelectedDate.ToString();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();

            selectionListBox.Items.Add(add);

            wadapter.Insert(ScenarioNumber, add, null, null);
            SelectionBox.Add(add);
        }

        private void sFactors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = sFactorCB.SelectedValue.ToString();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();

            selectionListBox.Items.Add(add);

            wadapter.Insert(ScenarioNumber, add, null, null);
            SelectionBox.Add(add);
        }

        private void selectExclusionCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = strategyExclusionCB.SelectedValue.ToString();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            exclusionListBox.Items.Add(add);

            wadapter.Insert(ScenarioNumber, null, add, null);
            ExclusionBox.Add(add);
        }

        private void mitigationExclusion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = mitigationExclusionCB.SelectedValue.ToString();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            exclusionListBox.Items.Add(add);

            wadapter.Insert(ScenarioNumber, null, add, null);
            ExclusionBox.Add(add);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            //wadapter.InsertQuery(SelectionBox, ExclusionBox, null);

            NavigationService.Navigate(
                new Uri("Pages/MiDESelection.xaml", UriKind.Relative));
        }

    }
    
}
