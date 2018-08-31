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
        MiDEDataSet ds = new MiDEDataSet();
        DataTable rdt = new DataTable();
        DataTable sortedTable = new DataTable();
        DataTable fdt = new DataTable("FilteredDataTable");
        List<string> list = new List<string>();
        
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

            fdt = HomeSelectionFilter(Home.ExclusionBox);

            sortedTable = SortedTable(fdt);
               
            rdt = Deal(sortedTable, SValue);

            AllEValueSum = getAvailableEValue(rdt);

            DataColumn col = rdt.Columns["EVariable"];

            foreach (DataRow row in rdt.Rows)
            {
                list.Add(row[col].ToString());
            }
            
            foreach (var item in list)
            {
                mitigationDisplay.Children.Add(CreateButtons(list, rdt, i));
                i++;
            }
            AddHandler(NewButton.ClickEvent, new RoutedEventHandler(button_Click));
                
        }

        #region Algorithmic Functions
        //Get a datatable that filters out Strategies or Evariables that are selected by the user as exclusions from Home page
        private DataTable HomeSelectionFilter(List<string> exclusionBox)
        {
            DataTable dts = new DataTable("MiDEExclusionReturnedTable");
           
            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();

            exclusionBox = Home.ExclusionBox;
            if (Home.isThrottled == 1)
            {
                if (exclusionBox.Count() == 0)
                {
                    string sqlString = "SELECT * FROM MiDEEValues WHERE CostMoney < 3";
                    cmd = new SqlCommand(sqlString, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dts);
                }

                else
                {
                    string sqlString = "SELECT * FROM MiDEEValues WHERE StrategyName NOT IN ({StrategyName}) AND EVariable NOT IN ({EVariable}) AND CostMoney < 3";
                    cmd = new SqlCommand(sqlString, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.AddArrayParameters("StrategyName", exclusionBox);
                    cmd.AddArrayParameters("EVariable", exclusionBox);
                    da.Fill(dts);
                }
            }
            else
            {
                if (exclusionBox.Count() == 0)
                {
                    string sqlString = "SELECT * FROM MiDEEValues";
                    cmd = new SqlCommand(sqlString, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dts);
                }
                else
                {
                    string sqlString = "SELECT * FROM MiDEEValues WHERE StrategyName NOT IN ({StrategyName}) AND EVariable NOT IN ({EVariable})";
                    cmd = new SqlCommand(sqlString, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    cmd.AddArrayParameters("StrategyName", exclusionBox);
                    cmd.AddArrayParameters("EVariable", exclusionBox);
                    da.Fill(dts);
                }
            }
            conn.Close();
            return dts;
        }

        //Take DataTable returned by HomeSelectionFilter and Sort
        private DataTable SortedTable(DataTable FilteredTable)
        {
            DataTable SortedTable = new DataTable("MiDESortedTable");
            DataView dv = FilteredTable.DefaultView;
            dv.Sort = "CostMoney ASC, Evalue DESC, CostEffort ASC";
            SortedTable = dv.ToTable();
            return SortedTable;
        }

        //Load a DataTable and sum of S values.  Create 2 new DataTables 1 for legit values, one for throw away.  Look at E Value in each DataRow and add to EValueSum
        //if EValueSum is less than SValueSum and that DataRow to the legit DataTable.  If we come across a value that make EValueSum > SValueSum
        //add that DataRow to throw away table and move on to next value if there is one.
        private DataTable Deal(DataTable dt, int SValueSum)
        {
            DataTable table = new DataTable();
            DataTable otherTable = new DataTable();
            otherTable = dt.Clone();
            table = dt.Clone();
            DataColumn col = dt.Columns["Evalue"];
            string evalue;
            int EValue;
            int EValueSum = 0;
           
            foreach (DataRow row in dt.Rows)
            {
               
                evalue = row[col].ToString();
                EValue = int.Parse(evalue);
                if (EValue > SValueSum)
                {
                    otherTable.Rows.Add(row.ItemArray);
                }
                else if (EValue == SValueSum)
                {
                    table.Rows.Add(row.ItemArray);
                    return table;
                }
                else 
                {
                    EValueSum += EValue;

                    if (EValueSum < SValueSum)
                    {
                        table.Rows.Add(row.ItemArray);
                    }

                    else if (EValueSum > SValueSum)
                    {
                        otherTable.Rows.Add(row.ItemArray);
                        EValueSum -= EValue;

                    }
                    else if (EValueSum == SValueSum)
                    {
                        table.Rows.Add(row.ItemArray);
                        return table;
                    }
                }
            }
            return table;
        }

        //sum of the Evalue from datatable that results from exclusion selections
        private int getAvailableEValue(DataTable t)
        {
            List<int> evalues = new List<int>();
            int Value = 0;
            DataColumn col = t.Columns["Evalue"];
            int i = 0;
            foreach (DataRow row in t.Rows)
            {
                string valueAdd = row[col].ToString();
                Value += Convert.ToInt32(valueAdd);
                i++;
            }
            return Value;
        }

        private List<string> RemainingMitigations(List<string> AllCurrentMitigations, List<string> SelectedMitigations)
        {
            List<string> Result = new List<string>();
            Result.AddRange(AllCurrentMitigations.Except(SelectedMitigations));
            //Result.AddRange()
            return Result;
        }

        //Create buttons
        private NewButton CreateButtons(List<string> list, DataTable dt, int i)
        {
            string eid = dt.Rows[i][0].ToString();
            NewButton button = new NewButton();
            Style style = FindResource("mButton") as Style;
            button.Tag = Int32.Parse(eid);
            button.Content = list[i].ToString();
            button.Style = style;
            button.Margin = new Thickness(0, 3, 3, 3);
            AllCurrentMitigations.Add(list[i].ToString());
            
            return button;
        }

        #endregion

        #region Button Events
        void button_Click(object sender, RoutedEventArgs e)
        {
           
            NewButton b = new NewButton();
            b = e.OriginalSource as NewButton;
            if (b != null)
            {
                string Bid = b.Tag.ToString();
                int bid;
                bid = int.Parse(Bid);
                bid -= 1;


                #region check for string match to Button RoutedEventArgs


                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[bid][2].ToString());
                MitigationSelection.Add(ds.MiDEEValues.Rows[bid][2].ToString());
                var evalue = ds.MiDEEValues.Rows[bid][3].ToString();
                int Evalue = int.Parse(evalue);
                EValues.Add(Evalue);

                #region after continued testing, erase me
                /*if (bid == 1)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[0][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[0][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[0][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 2)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[1][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[1][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[1][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 3)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[2][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[2][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[2][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 4)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[3][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[3][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[3][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 5)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[4][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[4][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[4][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 6)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[5][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[5][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[5][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 7)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[6][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[6][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[6][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 8)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[7][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[7][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[7][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 9)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[8][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[8][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[8][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 10)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[9][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[9][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[9][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 11)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[10][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[10][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[10][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 12)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[11][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[11][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[11][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 13)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[12][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[12][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[12][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 14)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[13][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[13][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[13][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 15)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[14][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[14][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[14][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 16)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[15][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[15][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[15][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 17)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[16][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[16][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[16][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 18)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[17][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[17][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[17][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 19)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[18][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[18][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[18][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 20)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[19][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[19][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[19][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 21)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[20][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[20][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[20][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 22)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[21][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[21][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[21][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 27)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[26][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[26][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[26][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 23)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[22][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[22][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[22][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 25)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[24][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[24][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[24][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 26)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[25][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[25][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[25][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);
                }
                else if (bid == 24)
                {
                    currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[23][2].ToString());
                    MitigationSelection.Add(ds.MiDEEValues.Rows[23][2].ToString());
                    var evalue = ds.MiDEEValues.Rows[23][3].ToString();
                    int Evalue = int.Parse(evalue);

                    EValues.Add(Evalue);

                }*/
                #endregion
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
