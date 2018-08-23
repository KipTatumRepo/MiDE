using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        #region Global Variables
        public static List<string> SelectionBox = new List<string>();
        public static List<string> ExclusionBox = new List<string>();

        public MiDEDataSet ds = new MiDEDataSet();
        public static int SValuesSum;
        public int ScenarioNumber;
        public static int isThrottled;
        #endregion

        #region Page Variables
        //TODO This will be needed for menu style
        //public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
        List<int> SValues = new List<int>();
        int j = 0;
        int k = 0;
        int l = 0;
        int m = 0;
        int Throttle;
        List<string> StrategyExCB = new List<string>();
        List<string> StrategyExclusion = new List<string>();
        List<string> MitigationExclusion = new List<string>();
        List<string> SVariableExclusion = new List<string>();
        ObservableCollection<string> comboboxlist = new ObservableCollection<string>();
        

        string BudgetThrottleText;
       
        #endregion

        public ObservableCollection<ItemWithToolTip> TheItems { get; set; }


        public Home()
        {

            InitializeComponent();


            #region Get Data

            ds = ((MiDEDataSet)(FindResource("mideDataSet")));
            MiDEDataSetTableAdapters.MiDEPopulationTableAdapter padapter = new MiDEDataSetTableAdapters.MiDEPopulationTableAdapter();
            MiDEDataSetTableAdapters.MiDEPopTypeTableAdapter ptadapter = new MiDEDataSetTableAdapters.MiDEPopTypeTableAdapter();
            MiDEDataSetTableAdapters.MiDESValuesTableAdapter sadapter = new MiDEDataSetTableAdapters.MiDESValuesTableAdapter();
            MiDEDataSetTableAdapters.MiDEStrategyGroupsTableAdapter stadapter = new MiDEDataSetTableAdapters.MiDEStrategyGroupsTableAdapter();
            MiDEDataSetTableAdapters.MiDEEValuesTableAdapter eadapter = new MiDEDataSetTableAdapters.MiDEEValuesTableAdapter();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            MiDEDataSetTableAdapters.MasterBuildingListTableAdapter adapter = new MiDEDataSetTableAdapters.MasterBuildingListTableAdapter();

            adapter.Fill(ds.MasterBuildingList);
            padapter.Fill(ds.MiDEPopulation);
            ptadapter.Fill(ds.MiDEPopType);
            sadapter.Fill(ds.MiDESValues);
            stadapter.Fill(ds.MiDEStrategyGroups);
            eadapter.Fill(ds.MiDEEValues);


            //these loops initially populates selectedVacatingBuildingCB, sFactorCB, strategyExclusionCB, and mitigationExclusionCB
            foreach (var item in ds.MasterBuildingList)
            {

                string comboboxtext = ds.MasterBuildingList.Rows[j][1].ToString();
                string tcombotext = comboboxtext.Trim();
                selectedVacatingBuildingCB.Items.Add(tcombotext);
                selectBuildingCB.Items.Add(tcombotext);
                j++;
            }
           
            foreach (var item in ds.MiDESValues)
            {
                //ComboBoxItem text = new ComboBoxItem();
                //text.Content = ds.MiDESValues.Rows[k][1].ToString();
                string comboboxtext = ds.MiDESValues.Rows[k][1].ToString();
                sFactorCB.Items.Add(comboboxtext);
                k++;
            }

            foreach (var item in ds.MiDEStrategyGroups)
            {
                StrategyExCB.Add(ds.MiDEStrategyGroups.Rows[l][1].ToString());
                string comboboxtext = ds.MiDEStrategyGroups.Rows[l][1].ToString();
                strategyExclusionCB.Items.Add(comboboxtext);
                l++;
            }

            foreach (var item in ds.MiDEEValues)
            {
                StrategyExclusion.Add(ds.MiDEEValues.Rows[m][2].ToString());
                string comboboxtext = ds.MiDEEValues.Rows[m][2].ToString();
                mitigationExclusionCB.Items.Add(comboboxtext);
                m++;
            }

            #endregion

            #region Generate ScenarioNumber
            //For generating a scenario number, get the last value in the MiDEWrite and add 1.  And get sum of all evalues
            DataTable last;
            last = wadapter.GetDataByLast();
            int lastvalue = (int)last.Rows[0][1];
            ScenarioNumber = lastvalue + 1;
            #endregion

        }

        void text_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("something");
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
            int SValue = 0;

            if (sFactorCB.SelectedIndex == -1 || sFactorCB.SelectedValue == null)
            {
                return;
            }
            string add = sFactorCB.SelectedValue.ToString();
            string svariable = ds.MiDESValues.Rows[0][1].ToString();

            //Because of the functionality of removing the selected item from the combobox array this function will
            //always make sure the correct svalue is being captured
            SValue = GetSValue(ds.MiDESValues, add);

            SValues.Add(SValue);

            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add);
            SVariable.Add(svariable);
            SVariableExclusion.Add(add);
            PopulateSFactor(SVariableExclusion);

            // when a new item is added to Selection list box, select it and show it
            // this will keep the last item highlighted and as the list grows beyond
            // the view of the list box, the last item will always be shown
            SelectionListBox.SelectedIndex = SelectionListBox.Items.Count - 1;
            SelectionListBox.ScrollIntoView(SelectionListBox.SelectedItem);

            sFactorCB.SelectedIndex = -1;

            SFactorDef.Text = "";
            SFactorDef.Background = Brushes.White;
            SFactorDef.Visibility = Visibility.Hidden;


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

            StrategyExclusion.Add(add);

            InitialPopulateMitigationExclusion(StrategyExclusion);

            // when a new item is added to Exclusion list box, select it and show it
            // this will keep the last item highlighted and as the list grows beyond
            // the view of the list box, the last item will always be shown
            ExclusionListBox.SelectedIndex = ExclusionListBox.Items.Count - 1;
            ExclusionListBox.ScrollIntoView(ExclusionListBox.SelectedItem);

            strategyExclusionCB.SelectedIndex = -1;
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
            MitigationExclusion.Add(add);
            PopulateMitigationExclusion(MitigationExclusion, StrategyExclusion);

            // when a new item is added to Exclusion list box, select it and show it
            // this will keep the last item highlighted and as the list grows beyond
            // the view of the list box, the last item will always be shown
            ExclusionListBox.SelectedIndex = ExclusionListBox.Items.Count - 1;
            ExclusionListBox.ScrollIntoView(ExclusionListBox.SelectedItem);

            mitigationExclusionCB.SelectedIndex = -1;
        }

        private void BudgetThrottle_Checked(object sender, RoutedEventArgs e)
        {
            if (BudgetThrottle.IsChecked == true)
            {
                Throttle = 1;
            }
            else
            {
                Throttle = 0;
            }

        }
        #endregion

        #region Button Events
        //Buttons for clearing boxed and handling click events
        private void ClearAllScenario_Click(object sender, RoutedEventArgs e)
        {
            k = 0;
            MessageBox.Show("Delete All Current Selections?");
            SelectionListBox.Items.Clear();
            SelectionBox.Clear();
            SValues.Clear();
            SVariableExclusion.Clear();
            sFactorCB.Items.Clear();

            foreach (var item in ds.MiDESValues)
            {
                string comboboxtext = ds.MiDESValues.Rows[k][1].ToString();
                sFactorCB.Items.Add(comboboxtext);
                k++;
            }

            return;
        }

        private void ClearLastScenario_Click(object sender, RoutedEventArgs e)
        {
            DataTable dts = new DataTable("NewSValueList");
            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();

            string sqlString = "SELECT * FROM MiDESValues WHERE svariable NOT IN ({SelectionBox})";
            SelectionListBox.SelectedIndex = SelectionListBox.Items.Count - 1;
            int currentIterator = SelectionListBox.Items.Count - 1;
            int listIterator = SValues.Count - 1;
            SelectionListBox.SelectedItem = SelectionListBox.Items.Count - 1;


            MessageBox.Show("Remove " + SelectionListBox.SelectedItem + "?");
            SelectionListBox.Items.RemoveAt(SelectionListBox.SelectedIndex);
            SelectionBox.RemoveAt(currentIterator);
            sFactorCB.Items.Clear();

            if (SelectionBox.Count == 0)
            {

                k = 0;

                if (SValues.Count() != 0)
                {
                    SValues.RemoveAt(listIterator);
                }

                foreach (var item in ds.MiDESValues)
                {
                    string comboboxtext = ds.MiDESValues.Rows[k][1].ToString();
                    sFactorCB.Items.Add(comboboxtext);
                    k++;
                }
                return;
            }

            if (SValues.Count() != 0)
            {
                SValues.RemoveAt(listIterator);
            }

            cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.AddArrayParameters("SelectionBox", SelectionBox);

            da.Fill(dts);

            DataColumn col = dts.Columns["svariable"];

            foreach (DataRow row in dts.Rows)
            {
                sFactorCB.Items.Add(row[col].ToString());
            }
            conn.Close();
            return;
        }

        private void ClearAllExclusion_Click(object sender, RoutedEventArgs e)
        {
            l = 0;
            m = 0;
            MessageBox.Show("Delete All Current Selections?");
            ExclusionListBox.Items.Clear();
            ExclusionBox.Clear();
            strategyExclusionCB.Items.Clear();
            mitigationExclusionCB.Items.Clear();

            foreach (var item in ds.MiDEStrategyGroups)
            {
                StrategyExCB.Add(ds.MiDEStrategyGroups.Rows[l][1].ToString());
                string comboboxtext = ds.MiDEStrategyGroups.Rows[l][1].ToString();
                strategyExclusionCB.Items.Add(comboboxtext);
                l++;
            }

            foreach (var item in ds.MiDEEValues)
            {
                StrategyExclusion.Add(ds.MiDEEValues.Rows[m][2].ToString());
                string comboboxtext = ds.MiDEEValues.Rows[m][2].ToString();
                mitigationExclusionCB.Items.Add(comboboxtext);
                m++;
            }
            return;
        }

        private void ClearLastExclusion_Click(object sender, RoutedEventArgs e)
        {
            DataTable dts = new DataTable("NewSValueList");
            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();

            string sqlString = "SELECT * FROM MiDEEValues WHERE EVariable NOT IN ({ExclusionBox}) AND StrategyName NOT IN ({ExclusionBox})";

            ExclusionListBox.SelectedIndex = ExclusionListBox.Items.Count - 1;
            int currentIterator = ExclusionListBox.Items.Count - 1;
            ExclusionListBox.SelectedItem = ExclusionListBox.Items.Count - 1;

            MessageBox.Show("Remove " + ExclusionListBox.SelectedItem + "?");
            ExclusionListBox.Items.RemoveAt(ExclusionListBox.SelectedIndex);
            ExclusionBox.RemoveAt(currentIterator);
            mitigationExclusionCB.Items.Clear();

            cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.AddArrayParameters("ExclusionBox", ExclusionBox);

            //all user selected exclusions have been removed so we should have a full list
            //of options in the mitigation exclusion combobox
            if (ExclusionBox.Count == 0)
            {
                m = 0;
                foreach (var item in ds.MiDEEValues)
                {
                    StrategyExclusion.Add(ds.MiDEEValues.Rows[m][2].ToString());
                    string comboboxtext = ds.MiDEEValues.Rows[m][2].ToString();
                    mitigationExclusionCB.Items.Add(comboboxtext);
                    m++;
                }
                return;
            }

            da.Fill(dts);
            DataColumn col = dts.Columns["EVariable"];

            foreach (DataRow row in dts.Rows)
            {
                mitigationExclusionCB.Items.Add(row[col].ToString());
            }
            conn.Close();
            return;
        }

        //Navigate to next page
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Check to see if there budget throttle is applied
            if (Throttle == 1)
            {
                BudgetThrottleText = "There are Budget Considerations";
                SelectionBox.Add(BudgetThrottleText);
            }

            selectedVacatingBuildingCB.Text = "Select Building";
            selectedPopRangeCB.Text = "Select Population Range";
            selectedPopTypeCB.Text = "Select Population Type";
            selectBuildingCB.Text = "Select Building";
            sFactorCB.Text = "Compression Factors";
            strategyExclusionCB.Text = "Select Strategy Exclusions";
            mitigationExclusionCB.Text = "Select Mitigation Exclusions";


            SValuesSum = SValues.Sum();
            isThrottled = Throttle;
            NavigationService.Navigate(
                new Uri("Pages/MiDESelection.xaml", UriKind.Relative));
        }

        #endregion

        //Populate mitigationExclusion Combobox with only items that are not included with strategy exclusions
        private ComboBox InitialPopulateMitigationExclusion(List<string> se)
        {
            mitigationExclusionCB.Items.Clear();
            int i = 0;
            List<string> MitigationExclusionText = new List<string>();
            DataTable dts = new DataTable("InitialMitigationList");
            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();

            string sqlString = "SELECT * FROM MiDEEValues WHERE StrategyName NOT IN ({StrategyName})";
            cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.AddArrayParameters("StrategyName", se);

            da.Fill(dts);

            DataColumn col = dts.Columns["EVariable"];

            foreach (DataRow row in dts.Rows)
            {
                MitigationExclusionText.Add(row[col].ToString());
            }

            foreach (var item in MitigationExclusionText)
            {
                string comboboxtext = MitigationExclusionText[i];
                mitigationExclusionCB.Items.Add(comboboxtext);
                i++;
            }

            return mitigationExclusionCB;
        }

        //Repopulate mitigationExclusion Combobox with remaining mitigation exclusion options when a  mitigation
        // is selected to exclude
        private ComboBox PopulateMitigationExclusion(List<string> me, List<string> se)
        {
            mitigationExclusionCB.Items.Clear();
            int i = 0;
            List<string> MitigationExclusionText = new List<string>();
            DataTable dts = new DataTable("NewMitigationList");
            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();

            string sqlString = "SELECT * FROM MiDEEValues WHERE EVariable NOT IN ({MitigationExclusionList}) AND StrategyName NOT IN ({StrategyNameList})";
            cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.AddArrayParameters("MitigationExclusionList", me);
            cmd.AddArrayParameters("StrategyNameList", se);

            da.Fill(dts);

            DataColumn col = dts.Columns["EVariable"];

            foreach (DataRow row in dts.Rows)
            {
                MitigationExclusionText.Add(row[col].ToString());
            }

            foreach (var item in MitigationExclusionText)
            {
                string comboboxtext = MitigationExclusionText[i];
                mitigationExclusionCB.Items.Add(comboboxtext);
                i++;
            }

            return mitigationExclusionCB;
        }

        //Repopulate sFactorCB, when a scenario S Factor has been chosen, with remaining S Factors
        //i.e. eliminate already chose S Factor from combobox
        private ComboBox PopulateSFactor(List<string> se)
        {
            //sFactorCB.Items.Clear();
            comboboxlist.Clear();
            int i = 0;
            List<string> SVariableText = new List<string>();
            DataTable dts = new DataTable("InitialMitigationList");
            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();

            string sqlString = "SELECT * FROM MiDESValues WHERE svariable NOT IN ({SVariableList})";
            cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.AddArrayParameters("SVariableList", se);

            da.Fill(dts);

            DataColumn col = dts.Columns["svariable"];

            foreach (DataRow row in dts.Rows)
            {
                SVariableText.Add(row[col].ToString());
            }

            foreach (var item in SVariableText)
            {
                ComboBoxItem text = new ComboBoxItem();
                //sFactorCB.Items.Add(text.Content = SVariableText[i]);
                comboboxlist.Add(dts.Rows[i][1].ToString());
                string comboboxtext = SVariableText[i];
                sFactorCB.Items.Add(comboboxtext);
                i++;
            }

            //sFactorCB.ItemsSource = comboboxlist;

            return sFactorCB;
        }

        //Get S Value of the selected S Factor
        private int GetSValue(DataTable ds, string cbstring)
        {
            int svalue = 0;
            DataTable dts = new DataTable("GetSValue");
            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();

            string sqlString = "SELECT svalue FROM MiDESValues WHERE (svariable = @SVariable)";
            cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("SVariable", cbstring);

            da.Fill(dts);

            DataColumn col = dts.Columns["svalue"];

            foreach (DataRow row in dts.Rows)
            {
                string Svalue = row[col].ToString();
                svalue = int.Parse(Svalue);
            }

            return svalue;
        }

        private void cmbItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MiDEDataSetTableAdapters.MiDESValuesTableAdapter adapter = new MiDEDataSetTableAdapters.MiDESValuesTableAdapter();

            MiDEDataSet.MiDESValuesDataTable table = new MiDEDataSet.MiDESValuesDataTable();
            string SelectedItem = sender.ToString();
            string TrimmedSelectedItem = SelectedItem.Remove(0, 38);
            string Definition;

            DataColumn col = table.Columns["definition"];

            adapter.FillByDefinition(table, TrimmedSelectedItem);


            Definition = table.Rows[0][col].ToString();

            SFactorDef.Text = Definition;
            SFactorDef.Background = Brushes.AliceBlue;
            SFactorDef.Visibility = Visibility.Visible;

        }

        private void sFactorCB_MouseLeave(object sender, MouseEventArgs e)
        {
            SFactorDef.Visibility = Visibility.Hidden;
        }
    }
}
