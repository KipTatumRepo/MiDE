using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
using MiDEWPF.Resources;
using MiDEWPF.ViewModel;

namespace MiDEWPF.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        #region Page Variables
        //TODO This will be needed for menu style
        //public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        
        List<int> SValues = new List<int>();
        int i = 0;
        int j = 0;
        int k = 0;
        int l = 0;
        #endregion
        #region Global Variables
        public static List<string> SelectionBox = new List<string>();
        public static List<string> ExclusionBox = new List<string>();
        public MiDEDataSet ds = new MiDEDataSet();
        public static int SValuesSum;
        public int ScenarioNumber;
       // public int ScenarioNumberB;
        #endregion
        
        public Home()
        {

            InitializeComponent();

            #region Get Data

            ds = ((MiDEDataSet)(FindResource("mideDataSet")));
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
           
            //these loops populate sFactorCB and strategyExclusionCB
            foreach(var item in ds.MiDESValues)
            {
                string comboboxtext = ds.MiDESValues.Rows[k][1].ToString();
                sFactorCB.Items.Add(comboboxtext);
                k++;
            }

            foreach(var item in ds.MiDEStrategyGroups)
            {
                string comboboxtext = ds.MiDEStrategyGroups.Rows[l][1].ToString();
                strategyExclusionCB.Items.Add(comboboxtext);
                l++;
            }
            #endregion


            #region Generate ScenarioNumber
            //For generating a scenario number, get the last value in the writeDB and add 1
            DataTable last;
            last = wadapter.GetDataByLast();
            int lastvalue = (int)last.Rows[0][1];
            ScenarioNumber = lastvalue + 1;
           
            #endregion

        }
        #region TODO
        //int realLastValue = int(lastvalue);
        //TODO Figure out how to implement Menu style box for this menu 
        //buildingMenu.DataContext = this;
        #endregion

        #region Handling Combobox Selections
        //Add selections to appropriate list box when selection is changed
        private void selectedVacatingBuildingCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedVacatingBuildingCB.SelectedIndex == -1 || selectedVacatingBuildingCB.SelectedValue == null)
            {
                return;
            }
            string add = selectedVacatingBuildingCB.SelectedValue.ToString();

            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add);
            return;
        }

        private void selectedPopRangeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedPopRangeCB.SelectedIndex == -1 || selectedPopRangeCB.SelectedValue == null)
            {
                return;
            }
            string add = selectedPopRangeCB.SelectedValue.ToString();

            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add);
            return;
        }

        private void selectedPopTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedPopTypeCB.SelectedIndex == -1 || selectedPopTypeCB.SelectedValue == null)
            {
                return;
            }
            string add = selectedPopTypeCB.SelectedValue.ToString();

            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add);
            return;
        }

        private void selectBuildingCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectBuildingCB.SelectedIndex == -1 || selectBuildingCB.SelectedValue == null)
            {
                return;
            }
            string add = selectBuildingCB.SelectedValue.ToString();

            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add);
            return;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            string add = datePicker.SelectedDate.ToString();

            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add);
            return;
        }

        private void sFactors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            List<string> SVariable = new List<string>();
            if (sFactorCB.SelectedIndex == -1 || sFactorCB.SelectedValue == null)
            { 
                return;
            }
            //MiDEDataSet ds = new MiDEDataSet();
            //MiDEDataSetTableAdapters.MiDESValuesTableAdapter svadapter = new MiDEDataSetTableAdapters.MiDESValuesTableAdapter();
            string add = sFactorCB.SelectedValue.ToString();
            //svadapter.FillBySValue(ds.MiDESValues, add);

            string svariable = ds.MiDESValues.Rows[0][1].ToString();
            var svalue = ds.MiDESValues.Rows[0][2].ToString();
            int Svalue = int.Parse(svalue);
            
            SValues.Add(Svalue);
            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add);
            SVariable.Add(svariable);

            // when a new item is added to Selection list box, select it and show it
            // this will keep the last item highlighted and as the list grows beyond
            // the view of the list box, the last item will always be shown
            SelectionListBox.SelectedIndex = SelectionListBox.Items.Count - 1;
            SelectionListBox.ScrollIntoView(SelectionListBox.SelectedItem);

            sFactorCB.SelectedIndex = -1;
        }

        private void strategyExclusionCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (strategyExclusionCB.SelectedIndex == -1 || strategyExclusionCB.SelectedValue == null)
            {
                return;
            }
            string add = strategyExclusionCB.SelectedValue.ToString();
            
            ExclusionListBox.Items.Add(add);
            ExclusionBox.Add(add);

            // when a new item is added to Exclusion list box, select it and show it
            // this will keep the last item highlighted and as the list grows beyond
            // the view of the list box, the last item will always be shown
            ExclusionListBox.SelectedIndex = ExclusionListBox.Items.Count - 1;
            ExclusionListBox.ScrollIntoView(ExclusionListBox.SelectedItem);

            strategyExclusionCB.SelectedIndex = -1;
            //return;
        }

        private void mitigationExclusion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mitigationExclusionCB.SelectedIndex == -1 || mitigationExclusionCB.SelectedValue == null)
            {
                return;
            }
            
            string add = mitigationExclusionCB.SelectedValue.ToString();

            ExclusionListBox.Items.Add(add);
            ExclusionBox.Add(add);

            // when a new item is added to Exclusion list box, select it and show it
            // this will keep the last item highlighted and as the list grows beyond
            // the view of the list box, the last item will always be shown
            ExclusionListBox.SelectedIndex = ExclusionListBox.Items.Count - 1;
            ExclusionListBox.ScrollIntoView(ExclusionListBox.SelectedItem);

            mitigationExclusionCB.SelectedIndex = -1;
            //return;
        }
        #endregion

        #region Button Events
        //Buttons for clearing boxed and handling click events
        private void ClearAllScenario_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete All Current Selections?");
            SelectionListBox.Items.Clear();
            SelectionBox.Clear();
            return;
        }

        private void ClearLastScenario_Click(object sender, RoutedEventArgs e)
        {

            SelectionListBox.SelectedIndex = SelectionListBox.Items.Count - 1;
            int currentIterator = SelectionListBox.Items.Count - 1;
            int listIterator = SValues.Count - 1;
            SelectionListBox.SelectedItem = SelectionListBox.Items.Count - 1;
           
            MessageBox.Show("Remove " + SelectionListBox.SelectedItem + "?");
            SelectionListBox.Items.RemoveAt(SelectionListBox.SelectedIndex);
            SelectionBox.RemoveAt(currentIterator);
            if(SValues.Count() != 0)
            { 
                SValues.RemoveAt(listIterator);
            }
            
            return;
        }

        private void ClearAllExclusion_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete All Current Selections?");
            ExclusionListBox.Items.Clear();
            ExclusionBox.Clear();
            return;
        }

        private void ClearLastExclusion_Click(object sender, RoutedEventArgs e)
        {

            ExclusionListBox.SelectedIndex = ExclusionListBox.Items.Count - 1;
            int currentIterator = ExclusionListBox.Items.Count - 1;
            ExclusionListBox.SelectedItem = ExclusionListBox.Items.Count - 1;
            
            MessageBox.Show("Remove " + ExclusionListBox.SelectedItem + "?");
            ExclusionListBox.Items.RemoveAt(ExclusionListBox.SelectedIndex);
            ExclusionBox.RemoveAt(currentIterator);
            return;
        }

        //write to DB and Navigate to next page
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            selectedVacatingBuildingCB.Text = "Select Building";
            selectedPopRangeCB.Text = "Select Population Range";
            selectedPopTypeCB.Text = "Select Population Type";
            selectBuildingCB.Text = "Select Building";
            sFactorCB.Text = "Compression Factors";
            strategyExclusionCB.Text = "Select Strategy Exclusions";
            mitigationExclusionCB.Text = "Select Mitigation Exclusions";
           
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            SValuesSum = SValues.Sum();
            

            NavigationService.Navigate(
                new Uri("Pages/MiDESelection.xaml", UriKind.Relative));
            
        }
        #endregion
    }
}
