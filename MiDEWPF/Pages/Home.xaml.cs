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
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        List<string> SelectionBox = new List<string>();
        List<string> ExclusionBox = new List<string>();
        List<int> SValues = new List<int>();
        public static int SValuesSum;
        int i = 0;
        int j = 0;
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

        //Add selections to appropriate list box when selection is changed
        private void selectedVacatingBuildingCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = selectedVacatingBuildingCB.SelectedValue.ToString();

            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add);
            return;
        }

        private void selectedPopRangeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = selectedPopRangeCB.SelectedValue.ToString();

            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add);
            return;
        }

        private void selectedPopTypeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = selectedPopTypeCB.SelectedValue.ToString();

            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add);
            return;
        }

        private void selectBuildingCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
            MiDEDataSet ds = new MiDEDataSet();
            MiDEDataSetTableAdapters.MiDESValuesTableAdapter svadapter = new MiDEDataSetTableAdapters.MiDESValuesTableAdapter();
            string add = sFactorCB.SelectedValue.ToString();
            svadapter.FillBySValue(ds.MiDESValues, add);

            var svalue = ds.MiDESValues.Rows[0][2].ToString();
            int Svalue = int.Parse(svalue);
            
            SValues.Add(Svalue);
            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add);

            // when a new item is added to Selection list box, select it and show it
            // this will keep the last item highlighted and as the list grows beyond
            // the view of the list box, the last item will always be shown
            SelectionListBox.SelectedIndex = SelectionListBox.Items.Count - 1;
            SelectionListBox.ScrollIntoView(SelectionListBox.SelectedItem);
            
        }

        private void selectExclusionCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = strategyExclusionCB.SelectedValue.ToString();

            ExclusionListBox.Items.Add(add);
            ExclusionBox.Add(add);

            // when a new item is added to Exclusion list box, select it and show it
            // this will keep the last item highlighted and as the list grows beyond
            // the view of the list box, the last item will always be shown
            ExclusionListBox.SelectedIndex = ExclusionListBox.Items.Count - 1;
            ExclusionListBox.ScrollIntoView(ExclusionListBox.SelectedItem);
            return;
        }

        private void mitigationExclusion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = mitigationExclusionCB.SelectedValue.ToString();

            ExclusionListBox.Items.Add(add);
            ExclusionBox.Add(add);

            // when a new item is added to Exclusion list box, select it and show it
            // this will keep the last item highlighted and as the list grows beyond
            // the view of the list box, the last item will always be shown
            ExclusionListBox.SelectedIndex = ExclusionListBox.Items.Count - 1;
            ExclusionListBox.ScrollIntoView(ExclusionListBox.SelectedItem);
            return;
        }

        


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
            SValues.RemoveAt(listIterator);
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            SValuesSum = SValues.Sum();

            foreach (var item in SelectionBox)
            {
                string currentIterator = SelectionBox[i].ToString();
                wadapter.Insert(ScenarioNumber, currentIterator, null, null);
                i++;
            }

            foreach (var item in ExclusionBox)
            {
                string currentIterator = ExclusionBox[j].ToString();
                wadapter.Insert(ScenarioNumber, null, currentIterator, null);
                j++;
            }

            NavigationService.Navigate(
                new Uri("Pages/MiDESelection.xaml", UriKind.Relative));
        }


    }
    
}
