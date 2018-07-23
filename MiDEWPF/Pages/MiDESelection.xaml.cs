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
        #region Local Variables
        int i = 0;
        int b = 0;
        public static int ScenarioNumber;
        SqlCommand Cmd;
        int SValue;
        List<string> content = new List<string>();
        List<string> ExclusionBoxEach = new List<string>();
        
        List<int> Buttons = new List<int>();
        List<int> dispose = new List<int>();
        MiDEDataSet ds = new MiDEDataSet();
        #endregion

        #region Global Variables
        public static int EValuesSum;
        public List<int> EValues = new List<int>();
        public static List<string> MitigationSelection = new List<string>();
        Home newhome = new Home();
        #endregion

        public MiDESelection()
        {
            foreach (var item in Home.ExclusionBox)
            {
                string ebitem = Home.ExclusionBox[b].ToString();
                b++;
            }
            
            InitializeComponent();
            
            //import style for buttons
            Style style = FindResource("mButton") as Style;
            #region Get Data
            MiDEDataSetTableAdapters.MiDEEValuesTableAdapter eadapter = new MiDEDataSetTableAdapters.MiDEEValuesTableAdapter();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter2 = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            eadapter.Fill(ds.MiDEEValues);
            wadapter.Fill(ds.MiDEWrite);

            //Get sum of S values and current Scenario Number from Home page.  For some reason it increments by 1 before coming here
            //so we have to subtract 1 to make sure it all matches
            SValue = Home.SValuesSum;
            ScenarioNumber = newhome.ScenarioNumber;
            

            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();
            string sqlString = "SELECT ScenarioNumber, SelectionListBox, ExclusionListBox, CurrentMitigationListBox, wid FROM MiDEWrite WHERE(ScenarioNumber = @ScenarioNumber) AND(ExclusionListBox IS NOT NULL)";

            Cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter sda = new SqlDataAdapter(Cmd);
            Cmd.Parameters.AddWithValue("@ScenarioNumber", ScenarioNumber);
            DataTable dt = new DataTable("MiDEWrite");

            sda.Fill(dt);
            
            currentExclusionLB.ItemsSource = Home.ExclusionBox;
           

            wadapter.FillByScenarioNumber(ds.MiDEWrite, ScenarioNumber);

            //currentScenarioLB.Items.Clear();
            currentScenarioLB.ItemsSource = Home.SelectionBox;
            //if(currentScenarioLB.)
            //Home.SelectionBox.Clear();

            #endregion


            if (Home.ExclusionBox.Count() >= 1)
            {
                List<string> FilteredList = Shuffle(Home.ExclusionBox);

                foreach (var item in FilteredList)
                {
                    mitigationDisplay.Children.Add(CreateButtons(FilteredList, i));
                    i++;
                }
                AddHandler(NewButton.ClickEvent, new RoutedEventHandler(button_Click));
            }
            else
            {
                List<string> AlleValueList = AllEValueList();

                foreach (var item in ds.MiDEEValues)
                {
                    mitigationDisplay.Children.Add(CreateButtons(AlleValueList, i));
                    i++;
                }
                AddHandler(NewButton.ClickEvent, new RoutedEventHandler(button_Click));
            }

        }

        public List<string> Shuffle(List<string> exclusionBox)
        {
            List<string> FilteredList = new List<string>();
            int i = 0;
            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();

            exclusionBox = Home.ExclusionBox;

            string sqlString = "SELECT * FROM MiDEEValues WHERE StrategyName NOT IN ({StrategyName}) AND EVariable NOT IN ({EVariable})";
            cmd = new SqlCommand(sqlString, conn);

            cmd.AddArrayParameters("StrategyName", exclusionBox);
            cmd.AddArrayParameters("EVariable", exclusionBox);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dts = new DataTable("MiDEFilterWrite");

            da.Fill(dts);

            DataColumn col = dts.Columns["EVariable"];
            foreach (DataRow row in dts.Rows)
            {
                FilteredList.Add(row[col].ToString());
                i++;
            }
            return FilteredList;
        }

        public NewButton CreateButtons(List<string> list, int i)
        {
            NewButton button = new NewButton();
            Style style = FindResource("mButton") as Style;
            button.Content = list[i].ToString();
            button.Style = style;
            content.Add(button.Content.ToString());
            return button;
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

        /*public List<string> Deal(int svaluesum, int evalue)
        {
            int Svalue = svaluesum;
            List<string> ha = new List<string>();
            //MessageBox.Show(SValue.ToString());
            return ha;
        }*/
        
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
            

            NavigationService.Navigate(
                new Uri("Pages/MiDEResults.xaml", UriKind.Relative));

        }

        private void ClearSelection_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete All Current Selections?");
            currentMitigationListBox.Items.Clear();
            EValues.Clear();
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
            return;
        }
        #endregion

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            
            Home.SelectionBox.Clear();
            Home.ExclusionBox.Clear();
            NavigationService.Navigate(
                new Uri("Pages/Home.xaml", UriKind.Relative));
            
            
        }
    }
}
