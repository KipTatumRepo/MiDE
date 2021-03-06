﻿using System;
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

        Data ds = new Data();
        //public MiDEDataSet ds = new MiDEDataSet();
        public static int SValuesSum;
        public int ScenarioNumber;
        public static int isThrottled;
       
        #endregion

        #region Page Variables
        List<int> SValues = new List<int>();
        int PopRangeSValue;
        string PopRangeSVariable;
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

        public Home()
        {

            InitializeComponent();
            
            string applicationName = Properties.Settings.Default.ApplicationName;

            #region Get Data

            //ds = ((MiDEDataSet)(FindResource("mideDataSet")));
            //MiDEDataSetTableAdapters.PopulationTableAdapter padapter = new MiDEDataSetTableAdapters.PopulationTableAdapter();
            //MiDEDataSetTableAdapters.PopTypeTableAdapter ptadapter = new MiDEDataSetTableAdapters.PopTypeTableAdapter();
            //MiDEDataSetTableAdapters.SValuesTableAdapter sadapter = new MiDEDataSetTableAdapters.SValuesTableAdapter();
            //MiDEDataSetTableAdapters.StrategyGroupsTableAdapter stadapter = new MiDEDataSetTableAdapters.StrategyGroupsTableAdapter();
            //MiDEDataSetTableAdapters.EValuesTableAdapter eadapter = new MiDEDataSetTableAdapters.EValuesTableAdapter();
            //MiDEDataSetTableAdapters.WriteTableAdapter wadapter = new MiDEDataSetTableAdapters.WriteTableAdapter();
            //MiDEDataSetTableAdapters.MasterBuildingListTableAdapter adapter = new MiDEDataSetTableAdapters.MasterBuildingListTableAdapter();

            //adapter.Fill(ds.MasterBuildingList);
            //padapter.Fill(ds.Population);
            //ptadapter.Fill(ds.PopType);
            //sadapter.Fill(ds.SValues);
            //stadapter.Fill(ds.StrategyGroups);
            //eadapter.Fill(ds.EValues);
            

            //these loops initially populates selectedVacatingBuildingCB, sFactorCB, strategyExclusionCB, and mitigationExclusionCB
            foreach (var item in ds.MasterBuildingList())
            {
                string comboboxtext = item;
                string tcombotext = comboboxtext.Trim();
                selectedVacatingBuildingCB.Items.Add(comboboxtext);
                selectBuildingCB.Items.Add(comboboxtext);
                j++;
            }
           
            foreach (var item in ds.SValue())
            {
                string comboboxtext = item.Name;
                sFactorCB.Items.Add(comboboxtext);
                k++;
            }

            foreach (var item in ds.StrategyGroups())
            {
                StrategyExCB.Add(item.Value);
                string comboboxtext = item.Value;   
                strategyExclusionCB.Items.Add(comboboxtext);
                l++;
            }

            foreach (var item in ds.EValue())
            {
                StrategyExclusion.Add(item.EName);  //(ds.EValues.Rows[m][2].ToString());
                string comboboxtext = item.EName;//ds.EValues.Rows[m][2].ToString();
                mitigationExclusionCB.Items.Add(comboboxtext);
                m++;
            }

            foreach (var item in ds.Population())
            {
                string comboboxtext = item;
                selectedPopRangeCB.Items.Add(comboboxtext);
            }

            foreach (var item in ds.PopulationTypes())
            {
                string comboboxtext = item.PopTypeName;
                selectedPopTypeCB.Items.Add(comboboxtext);
            }

            #endregion

            #region Generate ScenarioNumber
            //For generating a scenario number, get the last value in the MiDEWrite and add 1.  And get sum of all evalues
            DataTable last;
            //last = wadapter.GetDataByLast();
            int lastvalue = 0;//(int)last.Rows[0][0];
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

            if (add == "< 500")
            {
                SVariableExclusion.Add("Population Move >1000 People");
                SVariableExclusion.Add("Population Move 500-999");
                SVariableExclusion.Add("Population Move <500 People");
                PopRangeSValue = 1;
                PopRangeSVariable = "Population Move <500 People";
            }
            else if (add == "500 - 999")
            {
                SVariableExclusion.Add("Population Move >1000 People");
                SVariableExclusion.Add("Population Move 500-999");
                SVariableExclusion.Add("Population Move <500 People");
                PopRangeSValue = 9;
                PopRangeSVariable = "Population Move 500-999";
            }
            else
            {
                SVariableExclusion.Add("Population Move >1000 People");
                SVariableExclusion.Add("Population Move 500-999");
                SVariableExclusion.Add("Population Move <500 People");
                PopRangeSValue = 10;
                PopRangeSVariable = "Population Move > 1000 People";
            }
            PopulateSFactor(SVariableExclusion);
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
            if (datePicker.SelectedDate == null)
            {
                return;
            }
            var add = datePicker.SelectedDate;

            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add.ToString());
            SelectionBox.Add(PopRangeSVariable);
            SValues.Add(PopRangeSValue);
            return;
        }

        private void sFactors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //List<string> SVariable = new List<string>();
            int SValue = 0;

            if (sFactorCB.SelectedIndex == -1 || sFactorCB.SelectedValue == null)
            {
                return;
            }
            string add = sFactorCB.SelectedValue.ToString();
            //string svariable = ds.SValue.Name;    //ds.SValues.Rows[0][1].ToString();

            //Because of the functionality of removing the selected item from the combobox array this function will
            //always make sure the correct svalue is being captured
            SValue = GetSValue(add);

            SValues.Add(SValue);

            SelectionListBox.Items.Add(add);
            SelectionBox.Add(add);
            //SVariable.Add(svariable);
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

            PopulateMitigationExclusion(StrategyExclusion);
            //InitialPopulateMitigationExclusion(StrategyExclusion);

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
            //StrategyExclusion.Add(add);
            PopulateMitigationExclusion(StrategyExclusion, MitigationExclusion);

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
            //make sure the selection box is not empty
            if (SelectionListBox.Items.Count < 1)
            {
                BiMessageBox.Show("Delete Confirmation", "There Are No Items To Clear");
                return;
            }
            k = 0;

            var MessageBoxResult =
            BiMessageBox.Show("Delete Confirmation", "Delete All Current Selections?", MessageBoxButton.YesNo);

            if (MessageBoxResult != MessageBoxResult.Yes)
            {
                return;
            }
            SelectionListBox.Items.Clear();
            SelectionBox.Clear();
            SValues.Clear();
            SVariableExclusion.Clear();
            sFactorCB.Items.Clear();

            foreach (var item in ds.SValue())
            {
                string comboboxtext = item.Name;//ds.SValues.Rows[k][1].ToString();
                sFactorCB.Items.Add(comboboxtext);
                k++;
            }

            selectedVacatingBuildingCB.Text = "Select Building";
            selectedPopRangeCB.Text = "Select Population Range";
            selectedPopTypeCB.Text = "Select Population Type";
            selectBuildingCB.Text = "Select Building";
            sFactorCB.Text = "Compression Factors";
            datePicker.SelectedDate = null;
            return;
        }

        private void ClearLastScenario_Click(object sender, RoutedEventArgs e)
        {
            
        //    DataTable dts = new DataTable("NewSValueList");
        //    SqlCommand cmd;
        //    SqlConnection conn = ConnectionHelper.GetConn();
        //    conn.Open();

        //    string sqlString = "SELECT * FROM mide.SValues WHERE svariable NOT IN ({SelectionBox})";
        //    SelectionListBox.SelectedIndex = SelectionListBox.Items.Count - 1;
        //    int currentIterator = SelectionListBox.Items.Count - 1;
        //    int listIterator = SValues.Count - 1;
        //    SelectionListBox.SelectedItem = SelectionListBox.Items.Count - 1;

        //    //Make sure there are the selection box is not empty
        //    if (SelectionListBox.SelectedIndex == -1)
        //    {
        //        BiMessageBox.Show("Delete Confirmation", "There Are No Items To Clear");
        //        return;
        //    }

        //    var MessageBoxResult =
        //    BiMessageBox.Show("Delete Confirmation", "Remove " + SelectionListBox.SelectedItem + "?", MessageBoxButton.YesNo);
        //    if (MessageBoxResult != MessageBoxResult.Yes)
        //    {
        //        return;
        //    }
            
        //    SelectionListBox.Items.RemoveAt(SelectionListBox.SelectedIndex);
        //    SelectionBox.RemoveAt(currentIterator);
        //    sFactorCB.Items.Clear();

        //    if (SelectionBox.Count == 0)
        //    {
        //        k = 0;

        //        if (SValues.Count() != 0)
        //        {
        //            SValues.RemoveAt(listIterator);
        //            SVariableExclusion.RemoveAt(listIterator);
        //        }

        //        foreach (var item in ds.SValues)
        //        {
        //            string comboboxtext = ds.SValues.Rows[k][1].ToString();
        //            sFactorCB.Items.Add(comboboxtext);
        //            k++;
        //        }
        //        return;
        //    }

        //    if (SValues.Count() != 0)
        //    {
        //        SValues.RemoveAt(listIterator);
        //        SVariableExclusion.RemoveAt(listIterator + 2);
        //    }

        //    cmd = new SqlCommand(sqlString, conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    cmd.AddArrayParameters("SelectionBox", SelectionBox);

        //    da.Fill(dts);

        //    DataColumn col = dts.Columns["svariable"];

        //    foreach (DataRow row in dts.Rows)
        //    {
        //        sFactorCB.Items.Add(row[col].ToString());
        //    }
        //    conn.Close();
        //    return;
        }

        private void ClearAllExclusion_Click(object sender, RoutedEventArgs e)
        {
            l = 0;
            m = 0;
            //Make sure exclusion box is not empty
            if (ExclusionListBox.Items.Count < 1)
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
            ExclusionListBox.Items.Clear();
            ExclusionBox.Clear();
            MitigationExclusion.Clear();
            strategyExclusionCB.Items.Clear();
            mitigationExclusionCB.Items.Clear();

            foreach (var item in ds.StrategyGroups())
            {
                StrategyExCB.Add(item.Value);//ds.StrategyGroups.Rows[l][1].ToString());
                string comboboxtext = item.Value;//ds.StrategyGroups.Rows[l][1].ToString();
                strategyExclusionCB.Items.Add(comboboxtext);
                l++;
            }

            foreach (var item in ds.EValue())
            {
                StrategyExclusion.Add(item.EName);//ds.EValues.Rows[m][2].ToString());
                string comboboxtext = item.EName;//ds.EValues.Rows[m][2].ToString();
                mitigationExclusionCB.Items.Add(comboboxtext);
                m++;
            }
            return;
        }

        private void ClearLastExclusion_Click(object sender, RoutedEventArgs e)
        {
            
        //    DataTable dts = new DataTable("NewSValueList");
        //    SqlCommand cmd;
        //    SqlConnection conn = ConnectionHelper.GetConn();
        //    conn.Open();

        //    string sqlString = "SELECT * FROM mide.EValues WHERE EVariable NOT IN ({ExclusionBox}) AND StrategyName NOT IN ({ExclusionBox})";

        //    ExclusionListBox.SelectedIndex = ExclusionListBox.Items.Count - 1;
        //    int currentIterator = ExclusionListBox.Items.Count - 1;
        //    ExclusionListBox.SelectedItem = ExclusionListBox.Items.Count - 1;

        //    //Make sure exclusion box is not empty
        //    if (ExclusionListBox.SelectedIndex == -1)
        //    {
        //        BiMessageBox.Show("Delete Confirmation", "There Are No Items To Clear");
        //        return;
        //    }

        //    var MessageBoxResult =
        //    BiMessageBox.Show("Delete Confirmation", "Remove " + ExclusionListBox.SelectedItem + "?", MessageBoxButton.YesNo);
        //    if (MessageBoxResult != MessageBoxResult.Yes)
        //    {
        //        return;
        //    }
        //    ExclusionListBox.Items.RemoveAt(ExclusionListBox.SelectedIndex);
        //    ExclusionBox.RemoveAt(currentIterator);
        //    mitigationExclusionCB.Items.Clear();

        //    cmd = new SqlCommand(sqlString, conn);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    cmd.AddArrayParameters("ExclusionBox", ExclusionBox);

        //    //all user selected exclusions have been removed so we should have a full list
        //    //of options in the mitigation exclusion combobox
        //    if (ExclusionBox.Count == 0)
        //    {
        //        m = 0;
        //        foreach (var item in ds.EValues)
        //        {
        //            StrategyExclusion.Add(ds.EValues.Rows[m][2].ToString());
        //            string comboboxtext = ds.EValues.Rows[m][2].ToString();
        //            mitigationExclusionCB.Items.Add(comboboxtext);
        //            m++;
        //        }
        //        return;
        //    }

        //    da.Fill(dts);
        //    DataColumn col = dts.Columns["EVariable"];

        //    foreach (DataRow row in dts.Rows)
        //    {
        //        mitigationExclusionCB.Items.Add(row[col].ToString());
        //    }
        //    conn.Close();
        //    return;
        }

        //Navigate to next page
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //The following if's check to make sure there are valid entries
            if (SelectionBox.Count == 0)
            {
                BiMessageBox.Show("There Are No Entries For This Scenario, Please Enter Some Scenario Variables");
                return;
            }

            if (selectedVacatingBuildingCB.SelectedIndex == -1)
            {
                var MessageBoxResult =
                BiMessageBox.Show("No Selection Check", "There Is No Selection For a Vacating Building, Is That Correct?", MessageBoxButton.YesNo);
                if (MessageBoxResult != MessageBoxResult.Yes)
                {
                    return;
                }
            }
            if (selectedPopRangeCB.SelectedIndex == -1)
            {
                var MessageBoxResult =
                BiMessageBox.Show("No Selection Check", "There Is No Selection For a Population Range, Is That Correct?", MessageBoxButton.YesNo);
                if (MessageBoxResult != MessageBoxResult.Yes)
                {
                    return;
                }
            }
            if (selectedPopTypeCB.SelectedIndex == -1)
            {
                var MessageBoxResult =
                BiMessageBox.Show("No Selection Check", "There Is No Selection For a Population Type, Is That Correct?", MessageBoxButton.YesNo);
                if (MessageBoxResult != MessageBoxResult.Yes)
                {
                    return;
                }
            }
            if (selectBuildingCB.SelectedIndex == -1)
            {
                var MessageBoxResult =
                BiMessageBox.Show("No Selection Check", "There Is No Selection For What Building The Population Is Moving To, Is That Correct?", MessageBoxButton.YesNo);
                if (MessageBoxResult != MessageBoxResult.Yes)
                {
                    return;
                }
            }
            if (datePicker.SelectedDate == null)
            {
                var MessageBoxResult =
                BiMessageBox.Show("No Selection Check", "There Is No Selection For When This Scenario Will Start, Please Enter a Date");
                return;
            }

            //Check to see if there budget throttle is applied
            if (Throttle == 1 && !(SelectionBox.Contains(BudgetThrottleText)))
            {
                BudgetThrottleText = "There are Budget Considerations";
                SelectionBox.Add(BudgetThrottleText);
            }

            SValuesSum = SValues.Sum();
            isThrottled = Throttle;

            NavigationService.Navigate(
                new Uri("Pages/MiDESelection.xaml", UriKind.Relative));
        }
        #endregion

        //Populate mitigationExclusion Combobox with only items that are not included with strategy exclusions
        private ComboBox InitialPopulateMitigationExclusion(List<string> se)
        {
            
            List<EValue> fullEValueList = new List<EValue>();
            fullEValueList = ds.EValue();
            
            for (int i = 0; i <= se.Count - 1; i++)
            {
                for (int j = 0; j <= fullEValueList.Count - 1; j++)
                {
                    if (se[i] == fullEValueList[j].StrategyName)
                    {
                        fullEValueList.Remove(fullEValueList[j]);
                    }
                }
            }
            mitigationExclusionCB.Items.Clear();
           
            foreach (var item in fullEValueList)
            {
                string comboboxtext = item.EName;
                mitigationExclusionCB.Items.Add(comboboxtext);
            }
            return mitigationExclusionCB;
        }

        //Repopulate mitigationExclusion Combobox with remaining mitigation exclusion options when a  mitigation
        // is selected to exclude
        private ComboBox PopulateMitigationExclusion(List<string> se, List<string> me = null)
        {
            List<EValue> fullEValueList = new List<EValue>();
           
            fullEValueList = ds.EValue();
            mitigationExclusionCB.Items.Clear();
            //for (int i = 0; i <= se.Count - 1; i++)
            //{
               
            //    for (int j = 0; j <= fullEValueList.Count - 1; j++)
            //    {
                    
            //        if (se[i] == fullEValueList[j].StrategyName)
            //        {
            //            fullEValueList.Remove(fullEValueList[j]);
            //        }
            //    }
                
            //}

            var filterEValue = fullEValueList.Where(j => !se.Contains(j.StrategyName)).ToList();

            if (me != null)
            {
                filterEValue = filterEValue.Where(j => !me.Contains(j.EName)).ToList();
            }

            foreach (var item in filterEValue) //fullEValueList)
            {
                string comboboxtext = item.EName;
                mitigationExclusionCB.Items.Add(comboboxtext);
            }

            //int i = 0;
            //List<string> MitigationExclusionText = new List<string>();
            //DataTable dts = new DataTable("NewMitigationList");
            //SqlCommand cmd;
            //SqlConnection conn = ConnectionHelper.GetConn();
            //conn.Open();

            //string sqlString = "SELECT * FROM mide.EValues WHERE EVariable NOT IN ({MitigationExclusionList}) AND StrategyName NOT IN ({StrategyNameList})";
            //cmd = new SqlCommand(sqlString, conn);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //cmd.AddArrayParameters("MitigationExclusionList", me);
            //cmd.AddArrayParameters("StrategyNameList", se);

            //da.Fill(dts);

            //DataColumn col = dts.Columns["EVariable"];

            //foreach (DataRow row in dts.Rows)
            //{
            //    MitigationExclusionText.Add(row[col].ToString());
            //}

            //foreach (var item in MitigationExclusionText)
            //{
            //    string comboboxtext = MitigationExclusionText[i];
            //    mitigationExclusionCB.Items.Add(comboboxtext);
            //    i++;
            //}
            //conn.Close();
            return mitigationExclusionCB;
        }

        //Repopulate sFactorCB, when a scenario S Factor has been chosen, with remaining S Factors
        //i.e. eliminate already chosen S Factor from combobox
        private ComboBox PopulateSFactor(List<string> se)
        {
            List<SValue> SVariableText = new List<SValue>();
            List<SValue> fromData = new List<SValue>();
            fromData = ds.SValue();
            for (int i = 0; i <= se.Count - 1; i++)
            {
                for (int j = 0; j <= fromData.Count - 1; j++)
                {
                    if (se[i] == fromData[j].Name)
                    {
                        fromData.Remove(fromData[j]);
                    }
                }
            }

            sFactorCB.Items.Clear();

            foreach (var item in fromData)
            {
                string comboboxtext = item.Name;
                sFactorCB.Items.Add(comboboxtext);
            }
            return sFactorCB;
        }

        //Get S Value of the selected S Factor
        private int GetSValue (string cbstring)
        {
            List<SValue> sValues = ds.SValue();
            int svalue = 0;
            var filter = sValues.Where(j => cbstring == j.Name).ToList();//.Contains(j.Name)).ToList();
            svalue = filter[0].Value;
            return svalue;
        }

        private void cmbItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            MiDEDataSetTableAdapters.SValuesTableAdapter adapter = new MiDEDataSetTableAdapters.SValuesTableAdapter();
            MiDEDataSet.SValuesDataTable table = new MiDEDataSet.SValuesDataTable();
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
