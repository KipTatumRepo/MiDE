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
        int b = 0;
        public static int ScenarioNumber;
        SqlCommand Cmd;
        int SValue;
        List<string> content = new List<string>();
        MiDEDataSet ds = new MiDEDataSet();
        //int AllEValueTotal;
        #endregion

        public MiDESelection()
        {
            //Import style for buttons
            Style style = FindResource("mButton") as Style;

            InitializeComponent();
            
            #region Get Data
            MiDEDataSetTableAdapters.MiDEEValuesTableAdapter eadapter = new MiDEDataSetTableAdapters.MiDEEValuesTableAdapter();
            eadapter.Fill(ds.MiDEEValues);

            //Get sum of S values and current Scenario Number from Home page.  
            SValue = Home.SValuesSum;
            ScenarioNumber = newhome.ScenarioNumber;
            

            //Connection stuff
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();
            string sqlString = "SELECT ScenarioNumber, SelectionListBox, ExclusionListBox, CurrentMitigationListBox, wid FROM MiDEWrite WHERE(ScenarioNumber = @ScenarioNumber) AND(ExclusionListBox IS NOT NULL)";

            Cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter sda = new SqlDataAdapter(Cmd);
            Cmd.Parameters.AddWithValue("@ScenarioNumber", ScenarioNumber);
            DataTable dt = new DataTable("MiDEWrite");

            sda.Fill(dt);
            
            currentExclusionLB.ItemsSource = Home.ExclusionBox;
            currentScenarioLB.ItemsSource = Home.SelectionBox;

            #endregion

            DataTable fdt  = new DataTable("FilteredDataTable");
            Dictionary<string, int> filteredDictionary = new Dictionary<string, int>();
            int i = 0;

            #region Input Scenarios
            //There are budget constraints and there are exclusions selected by the user
              if (Home.ExclusionBox.Count() >= 1 && Home.isThrottled == 1)
              {
                //Select Evalues that do not correspond to the exclusions selected by the user
                fdt = HomeSelectionFilter(Home.ExclusionBox);
                AllEValueSum = getAvailableEValue(fdt);

                //Take that datatable sort by ascending, add evariable as KEY and evalue as VALUE to dictionary
                filteredDictionary = Deal(fdt);

                  foreach(var item in filteredDictionary)
                  {
                      mitigationDisplay.Children.Add(CreateButtons(KeyList(filteredDictionary), i));
                      i++;
                  }
                  AddHandler(NewButton.ClickEvent, new RoutedEventHandler(button_Click));
              }

              //There are budget constraints, but no exclusions
              else if (Home.isThrottled == 1)
              {
                fdt = NoExclusions(Home.ExclusionBox);
                AllEValueSum = getAvailableEValue(fdt);

                //Take that datatable sort by ascending, add evariable as KEY and evalue as VALUE to dictionary
                filteredDictionary = Deal(fdt);

                  foreach(var item in filteredDictionary)
                  {
                      mitigationDisplay.Children.Add(CreateButtons(KeyList(filteredDictionary), i));
                      i++;
                  }
                  AddHandler(NewButton.ClickEvent, new RoutedEventHandler(button_Click));
              }

            //There is an unlimited budget but there are exclusions selected by the user
            else if (Home.ExclusionBox.Count() >= 1)
            {
                fdt = HomeSelectionFilter(Home.ExclusionBox);
                List<string> list = new List<string>();
                List<string> slist = new List<string>();
                DataColumn col = fdt.Columns["EVariable"];
                DataColumn col2 = fdt.Columns["StrategyName"];
                AllEValueSum = getAvailableEValue(fdt);

                foreach (DataRow row in fdt.Rows)
                {
                    list.Add(row[col].ToString());
                    slist.Add(row[col2].ToString());
                }

                foreach(var item in list)
                {
                    mitigationDisplay.Children.Add(CreateButtons(list, i));
                    i++;
                }
                AddHandler(NewButton.ClickEvent, new RoutedEventHandler(button_Click));
            }

            //Otherwise there is an unlimited budget and no exclusions
            else
            {
                List<string> AlleValueList = AllEValueList();
                List<string> slist = new List<string>();
                AllEValueSum = getAvailableEValue(AllDataTable());

                foreach (var item in ds.MiDEEValues)
                {
                    mitigationDisplay.Children.Add(CreateButtons(AlleValueList, i));
                    i++;
                }
                AddHandler(NewButton.ClickEvent, new RoutedEventHandler(button_Click));

            }
            #endregion
        }

        #region Algorithmic Functions
        //Get a datatable based on Strategies or Evariables that are selected by the user as exclusions from Home page
        public DataTable HomeSelectionFilter(List<string> exclusionBox)
        {
            DataTable dts = new DataTable("MiDEFilterWrite");
           
            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();

            exclusionBox = Home.ExclusionBox;

            string sqlString = "SELECT * FROM MiDEEValues WHERE StrategyName NOT IN ({StrategyName}) AND EVariable NOT IN ({EVariable})";
            cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.AddArrayParameters("StrategyName", exclusionBox);
            cmd.AddArrayParameters("EVariable", exclusionBox);
            da.Fill(dts);
           
            return dts;
        }

        //Get all Evariables because there are not any exclusions added be the user but budget is constrained so we order by ASC
        public DataTable NoExclusions(List<string> exclusionBox)
        {
            DataTable dts = new DataTable("MiDEFilterWrite");

            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();

            exclusionBox = Home.ExclusionBox;

            string sqlString = "Select * FROM MiDEEValues ORDER BY CostMoney ASC";

            cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dts);

            return dts;
        }

        //Load the datatable we got from the query based on having exclusions or not and order the table ascending by dollar cost because there are budget contraints
        //then the Evariable text and associated Evalue are loaded into a Dictionary where Evariable is the KEY and Evalue is the VALUE
        public Dictionary<string, int> Deal(DataTable dt)
        {
            Dictionary<string, int> EDictionary = new Dictionary<string, int>();
            DataTable SortedTable = new DataTable();
            dt.DefaultView.Sort = "[CostMoney] ASC";
            SortedTable = dt.DefaultView.ToTable();

            foreach (DataRow row in SortedTable.Rows)
            {
                string EVariable = row[2].ToString();
                string EValue = row[3].ToString();
                int EInt = int.Parse(EValue);
                EDictionary.Add(EVariable, EInt);
            }
            return EDictionary;
        }

        //Create a List<string> of keys from dictionary so we can put text in buttons
        public List<string> KeyList(Dictionary<string, int> dictionary)
        {
            List<string> keys = new List<string>();

            foreach (KeyValuePair<string, int> kvp in dictionary )
            {
                keys.Add(kvp.Key);
            }
            return keys;
        }

        public List<string> AllEValueList()
        {
            int i = 0;
            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();
            List<string> AllEValues = new List<string>();
           

            string sqlString = "SELECT * FROM MiDEEValues";
            cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dts = new DataTable("MiDEAllWrite");

            da.Fill(dts);

            DataColumn col = dts.Columns["EVariable"];
            
            foreach (DataRow row in dts.Rows)
            {
                AllEValues.Add(row[col].ToString());
                i++;
            }
            return AllEValues;
        }

        public DataTable AllDataTable()
        {
            int i = 0;
            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();
            
            string sqlString = "SELECT * FROM MiDEEValues";
            cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dts = new DataTable("TempTable");

            da.Fill(dts);

            return dts;
        }

        public int getAvailableEValue(DataTable t)
        {
            List<int> evalues = new List<int>();
            //MiDEDataSetTableAdapters.MiDEEValuesTableAdapter eadapter = new MiDEDataSetTableAdapters.MiDEEValuesTableAdapter();

           // eadapter.Fill(ds.MiDEEValues);
            //DataTable dts = new DataTable("TempTable");
            int Value = 0;
            DataColumn col = t.Columns["Evalue"];
            int i = 0;
            foreach (DataRow row in t.Rows)
            {
                string valueAdd = row[col].ToString();
                //string valueAdd = t.MiDEEValues[i][3].ToString();
                Value += Convert.ToInt32(valueAdd);
                i++;
            }

            return Value;
        }

        public List<string> RemainingMitigations(List<string> AllMitigations, List<string> SelectedMitigations)
        {
            List<string> Result = new List<string>();
            Result.AddRange(AllMitigations.Except(SelectedMitigations));
            return Result;
        }

        //Create buttons
        public NewButton CreateButtons(List<string> list, int i)
        {

            NewButton button = new NewButton();
            Style style = FindResource("mButton") as Style;
            button.Content = list[i].ToString();
            button.Style = style;
            content.Add(button.Content.ToString());
            AllCurrentMitigations.Add(list[i].ToString());
            return button;
        }
        #endregion

        #region Button Events
        void button_Click(object sender, RoutedEventArgs e)
        {
            string idk = e.OriginalSource.ToString();
            string bbase = "MiDEWPF.Models.NewButton: ";

            #region check for string match to Button RoutedEventArgs
            if (idk == bbase + "Food Truck Support")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[0][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[0][2].ToString());
                var evalue = ds.MiDEEValues.Rows[0][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Permanent Pop-Up")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[1][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[1][2].ToString());
                var evalue = ds.MiDEEValues.Rows[1][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Food Delivery Service")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[2][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[2][2].ToString());
                var evalue = ds.MiDEEValues.Rows[2][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Increase DTO Offerings/Stations")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[3][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[3][2].ToString());
                var evalue = ds.MiDEEValues.Rows[3][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Market@ Product Expansion")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[4][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[4][2].ToString());
                var evalue = ds.MiDEEValues.Rows[4][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Grab & Go ")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[5][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[5][2].ToString());
                var evalue = ds.MiDEEValues.Rows[5][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Simplify Existing Concepts")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[6][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[6][2].ToString());
                var evalue = ds.MiDEEValues.Rows[6][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Customer/Stakeholder Pre-Meetings")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[7][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[7][2].ToString());
                var evalue = ds.MiDEEValues.Rows[7][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Station Awareness Comms")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[8][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[8][2].ToString());
                var evalue = ds.MiDEEValues.Rows[8][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Readiness Tours")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[9][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[9][2].ToString());
                var evalue = ds.MiDEEValues.Rows[9][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "LTO Beverages")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[10][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[10][2].ToString());
                var evalue = ds.MiDEEValues.Rows[10][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Dedicated Shuttles")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[11][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[11][2].ToString());
                var evalue = ds.MiDEEValues.Rows[11][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Temp/Additional Water Stations")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[12][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[12][2].ToString());
                var evalue = ds.MiDEEValues.Rows[12][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Extended Service Times")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[13][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[13][2].ToString());
                var evalue = ds.MiDEEValues.Rows[13][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Discounts for Off-Time Service")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[14][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[14][2].ToString());
                var evalue = ds.MiDEEValues.Rows[14][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Replace Static Station(Local Brand)")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[15][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[15][2].ToString());
                var evalue = ds.MiDEEValues.Rows[15][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Welcome Events")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[16][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[16][2].ToString());
                var evalue = ds.MiDEEValues.Rows[16][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Goodbye Parties")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[17][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[17][2].ToString());
                var evalue = ds.MiDEEValues.Rows[17][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Welcome Parties")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[18][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[18][2].ToString());
                var evalue = ds.MiDEEValues.Rows[18][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Move Survival Kit")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[19][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[19][2].ToString());
                var evalue = ds.MiDEEValues.Rows[19][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Station Sampling")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[20][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[20][2].ToString());
                var evalue = ds.MiDEEValues.Rows[20][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Utilize Atrium & Non-Standard Spaces")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[21][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[21][2].ToString());
                var evalue = ds.MiDEEValues.Rows[21][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Replace Static Station(Our Concept)")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[26][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[26][2].ToString());
                var evalue = ds.MiDEEValues.Rows[26][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Temporary Pop-Up")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[22][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[22][2].ToString());
                var evalue = ds.MiDEEValues.Rows[22][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Mobile Pop-Up")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[24][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[24][2].ToString());
                var evalue = ds.MiDEEValues.Rows[24][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Nextep Bank Install")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[25][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[25][2].ToString());
                var evalue = ds.MiDEEValues.Rows[25][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Linebusting")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[23][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[23][2].ToString());
                var evalue = ds.MiDEEValues.Rows[23][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }

            #endregion
        }

        //Write to DB and navigate to next page
        private void ShowResults_Click(object sender, RoutedEventArgs e)
        {
            EValuesSum = EValues.Sum();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            int i = 0;
            int j = 0;
            int k = 0;

            foreach (var item in Home.SelectionBox)
            {
                string currentIterator = Home.SelectionBox[i].ToString();
                wadapter.Insert(ScenarioNumber, currentIterator, null, null);
                i++;
            }

            foreach (var item in Home.ExclusionBox)
            {
                string currentIterator = Home.ExclusionBox[j].ToString();
                wadapter.Insert(ScenarioNumber, null, currentIterator, null);
                j++;
            }
            foreach( var item in MitigationSelection)
            {
                string currentIterator = MitigationSelection[k].ToString();
                wadapter.Insert(ScenarioNumber, null, null, currentIterator);
                k++;
            }

            RemainingMitigationList = RemainingMitigations(AllCurrentMitigations, MitigationSelection);

            NavigationService.Navigate(
                new Uri("Pages/MiDEResults.xaml", UriKind.Relative));
        }

        private void ClearSelection_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete All Current Selections?");
            currentMitigationListBox.Items.Clear();
            EValues.Clear();
            MitigationSelection.Clear();
            return;
        }

        private void ClearLastSelection_Click(object sender, RoutedEventArgs e)
        {
            currentMitigationListBox.SelectedIndex = currentMitigationListBox.Items.Count - 1;
            int currentIterator = currentMitigationListBox.Items.Count - 1;
            currentMitigationListBox.SelectedItem = currentMitigationListBox.Items.Count - 1;

            MessageBox.Show("Remove " + currentMitigationListBox.SelectedItem + "?");
            currentMitigationListBox.Items.RemoveAt(currentMitigationListBox.SelectedIndex);
            EValues.RemoveAt(currentIterator);
            MitigationSelection.RemoveAt(currentIterator);
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
