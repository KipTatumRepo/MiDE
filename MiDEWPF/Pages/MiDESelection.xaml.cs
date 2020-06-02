using MiDEWPF.MiDEDataSetTableAdapters;
using MiDEWPF.Models;
using MiDEWPF.Resources;
using System;
using System.Collections.Generic;
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

namespace MiDEWPF.Pages
{
    /// <summary>
    /// Interaction logic for MiDESelection.xaml
    /// </summary>
    public partial class MiDESelection : Page
    {
        #region Global Variables
        public static int EValuesSum;
        public List<int> EValues = new List<int>();
        public static List<string> MitigationSelection = new List<string>();
        public static List<string> AllCurrentMitigations = new List<string>();
        public static List<string> RemainingMitigationList = new List<string>();
        Home newhome = new Home();
        public static int AllEValueSum;
        #endregion

        #region Local Variables
        public static int ScenarioNumber;
        int i = 0;
        SqlCommand Cmd;
        int SValue;
        List<string> content = new List<string>();
        //MiDEDataSet ds = new MiDEDataSet();
        List<EValue> rdt = new List<EValue>();
        DataTable sortedTable = new DataTable();
        List<EValue> fdt = new List<EValue>(); //new DataTable("FilteredDataTable");
        List<EValue> list = new List<EValue>();
        NewButton b = new NewButton();
        Data ds = new Data();
        #endregion

        public MiDESelection()
        {
            //Import style for buttons
            Style style = FindResource("mButton") as Style;

            InitializeComponent();

            #region Get Data
            //Get sum of S values and current Scenario Number from Home page.  
            SValue = Home.SValuesSum;
            ScenarioNumber = newhome.ScenarioNumber;
            currentExclusionLB.ItemsSource = Home.ExclusionBox;
            currentScenarioLB.ItemsSource = Home.SelectionBox;
            #endregion

            fdt = HomeSelectionFilter(Home.ExclusionBox);

            rdt = Deal(fdt, SValue);//sortedTable, SValue);

            AllEValueSum = getAvailableEValue(rdt);

            foreach (var item in rdt)
            {
                mitigationDisplay.Children.Add(CreateButtons(rdt, i));
                i++;
            }
            AddHandler(NewButton.ClickEvent, new RoutedEventHandler(MitigationOptionButton_Click));
        }

        #region Algorithmic Functions
        //Get a datatable that filters out Strategies or Evariables that are selected by the user as exclusions from Home page
        private List<EValue> HomeSelectionFilter(List<string> exclusionBox)
        {
            int i = 0;
            List<string> dts = new List<string>();
            var allSGroups = ds.EValue();
            
            var list = allSGroups.Where(j => !exclusionBox.Contains(j.StrategyName)).ToList();
            var updatedList = list.Where(j => !exclusionBox.Contains(j.EName)).ToList();
                
            return updatedList;
        }

        //Take DataTable returned by HomeSelectionFilter and Sort
        //private DataTable SortedTable(DataTable FilteredTable)
        //{
        //    DataTable SortedTable = new DataTable("MiDESortedTable");
        //    DataView dv = FilteredTable.DefaultView;
        //    dv.Sort = "CostMoney ASC, Evalue DESC, CostEffort ASC";
        //    SortedTable = dv.ToTable();
        //    return SortedTable;
        //}

        //Load a DataTable and sum of S values.  Create 2 new DataTables 1 for legit values, one for throw away.  Look at E Value in each DataRow and add to EValueSum
        //if EValueSum is less than SValueSum and that DataRow to the legit DataTable.  If we come across a value that make EValueSum > SValueSum
        //add that DataRow to throw away table and move on to next value if there is one.
        private List<EValue> Deal(List<EValue> dt, int SValueSum)
        {
            dt.Sort();
            List<EValue> table = new List<EValue>();
            List<EValue> otherList = new List<EValue>();
            string evalue;
            int EValue;
            int EValueSum = 0;

            foreach (var item in dt)
            {
                EValue = item.EScore;
                if (EValue > SValueSum)
                {
                    otherList.Add(item);
                }
                else if (EValue == SValueSum)
                {
                    table.Add(item);
                    return table;
                }
                else
                {
                    EValueSum += EValue;
                    if (EValueSum < SValueSum)
                    {
                        table.Add(item);
                    }

                    else if (EValueSum > SValueSum)
                    {
                        otherList.Add(item);
                        EValueSum -= EValue;

                    }
                    else if (EValueSum == SValueSum)
                    {
                        table.Add(item);
                        return table;
                    }
                }
            }
            return table;
        }

        //sum of the Evalue from datatable that results from exclusion selections
        private int getAvailableEValue(List<EValue> t)
        {
            List<int> evalues = new List<int>();
            int Value = 0;
           
            foreach (var item in t)
            {
                Value += item.EScore;
            }
            return Value;
        }

		//Remove Selected Mitigations
        private List<string> RemainingMitigations(List<string> AllCurrentMitigations, List<string> SelectedMitigations)
        {
            List<string> result = new List<string>();
            result = AllCurrentMitigations.Except(SelectedMitigations).ToList();
            return result;
        }

        //Create buttons
        private NewButton CreateButtons(List<EValue> list, int i)
        {
            string eid = list[i].EName;
            NewButton button = new NewButton();
            Style style = FindResource("mButton") as Style;
            button.Tag = eid;
            button.Content = list[i].EName;
            button.Style = style;
            button.Margin = new Thickness(0, 3, 3, 3);
            AllCurrentMitigations.Add(eid);
            
            return button;
        }

        #endregion

        #region Button Events
        void MitigationOptionButton_Click(object sender, RoutedEventArgs e)
        {
            
            b = e.OriginalSource as NewButton;
            List<EValue> allEValues = ds.EValue();
            if (b != null)
            {
                string Bid = b.Tag.ToString();

                #region check for string match to Button RoutedEventArgs
                currentMitigationListBox.Items.Add(Bid);
                
                EValue evalue = allEValues.FirstOrDefault(j => Bid == j.EName);
                MitigationSelection.Add(evalue.EName);
                EValues.Add(evalue.EScore);
                b.Visibility = Visibility.Collapsed;
                #endregion
            }

        }

        //Write to DB and navigate to next page
        private void ShowResults_Click(object sender, RoutedEventArgs e)
        {
            EValuesSum = EValues.Sum();
            MiDEDataSetTableAdapters.WriteTableAdapter wadapter = new MiDEDataSetTableAdapters.WriteTableAdapter();

            RemainingMitigationList = RemainingMitigations(AllCurrentMitigations, MitigationSelection);

            NavigationService.Navigate(
                new Uri("Pages/MiDEResults.xaml", UriKind.Relative));
        }

        private void ClearSelection_Click(object sender, RoutedEventArgs e)
        {
            i = 0;
            if (currentMitigationListBox.Items.Count < 1)
            {
                BiMessageBox.Show("Delete Confirmation", "There Are No Items To Clear");
                return;
            }

            var MessageBoxResult =
           BiMessageBox.Show("Delete Confirmation", "Delete All Current Selections?", MessageBoxButton.YesNo);

            if (MessageBoxResult != MessageBoxResult.Yes)
            {
                return;
            }
            
            currentMitigationListBox.Items.Clear();
            EValues.Clear();
            MitigationSelection.Clear();
            mitigationDisplay.Children.Clear();
            
            foreach (var item in rdt)
            {
                mitigationDisplay.Children.Add(CreateButtons(rdt, i));
                i++;
            }
            return;
        }

        private void ClearLastSelection_Click(object sender, RoutedEventArgs e)
        {
            currentMitigationListBox.SelectedIndex = currentMitigationListBox.Items.Count - 1;
            int currentIterator = currentMitigationListBox.Items.Count - 1;
            currentMitigationListBox.SelectedItem = currentMitigationListBox.Items.Count - 1;

            if (currentMitigationListBox.SelectedIndex == -1)
            {
                BiMessageBox.Show("Delete Confirmation", "There Are No Items To Clear");
                return;
            }

            var MessageBoxResult =
            BiMessageBox.Show("Delete Confirmation", "Remove " + currentMitigationListBox.SelectedItem + "?", MessageBoxButton.YesNo);
            if (MessageBoxResult != MessageBoxResult.Yes)
            {
                return;
            }
            currentMitigationListBox.Items.RemoveAt(currentMitigationListBox.SelectedIndex);
            
            if (EValues.Count > 0)
            {
                EValues.RemoveAt(currentIterator);
            }

            if (MitigationSelection.Count > 0)
            {
                MitigationSelection.RemoveAt(currentIterator);
            }
            
            b.Visibility = Visibility.Visible; //maybe remove
            return;
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {

            Home.SelectionBox.Clear();
            Home.ExclusionBox.Clear();
            NavigationService.Navigate(
                new Uri("Pages/Home.xaml", UriKind.Relative));
        }
        #endregion

    }
}
