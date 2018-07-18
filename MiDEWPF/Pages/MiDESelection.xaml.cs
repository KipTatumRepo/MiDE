﻿using MiDEWPF.Models;
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
        int ScenarioNumber;
        SqlCommand Cmd;
        int SValue;
        List<string> content = new List<string>();
        List<string> ExclusionBoxEach = new List<string>();
        List<int> Buttons = new List<int>();
        List<int> dispose = new List<int>();
        
        MiDEDataSet ds = new MiDEDataSet();
        //Random Number Generator for testing purposes
        Random random = new Random();
        int row;

        
        #endregion

        #region Global Variables
        public static int EValuesSum;
        public List<int> EValues = new List<int>();
        Home newhome = new Home();
        #endregion

        
        
        
        public MiDESelection()
        {
            foreach(var item in Home.ExclusionBox)
            {
                string ebitem = Home.ExclusionBox[b].ToString();
                b++;
            }
            //import style for buttons
            
            
            InitializeComponent();
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
            ScenarioNumber--;

            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();
            string sqlString = "SELECT ScenarioNumber, SelectionListBox, ExclusionListBox, CurrentMitigationListBox, wid FROM MiDEWrite WHERE(ScenarioNumber = @ScenarioNumber) AND(ExclusionListBox IS NOT NULL)";
            
            Cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter sda = new SqlDataAdapter(Cmd);
            Cmd.Parameters.AddWithValue("@ScenarioNumber", ScenarioNumber);
            DataTable dt = new DataTable("MiDEWrite");

            sda.Fill(dt);

            currentExclusionLB.ItemsSource = dt.DefaultView;
            //conn.Close();

            wadapter.FillByScenarioNumber(ds.MiDEWrite, ScenarioNumber);
            currentScenarioLB.ItemsSource = ds.MiDEWrite;
            #endregion

            FilterButtons(Home.ExclusionBox);
            CreateButtons(ExclusionBoxEach);
            


            if(SValue < 10)
            {
                //for demo purposes a random number is generated to select rows from DB, for production need to get rid of 
                //row variable assignment and change [row] back to [i]
                for(int c = 0; c <= 2; c++)
                {
                    row = random.Next(27);
                    if (dispose.Contains(row))
                    {
                        row = random.Next(27);
                    }
                    else
                    { 
                        NewButton button = new NewButton();
                        button.Content = ds.MiDEEValues.Rows[row][2].ToString();
                        //button.Bid = i;
                        button.Style = style;
                        mitigationDisplay.Children.Add(button);
                        i++;
                        content.Add(button.Content.ToString());
                        //Buttons.Add(button.Bid);
                        dispose.Add(row);
                        
                    }
                }
                NewButton FinalButton = new NewButton();
                FinalButton.Content = ds.MiDEEValues.Rows[26][2].ToString();
                //FinalButton.Bid = i;
                FinalButton.Style = style;
                mitigationDisplay.Children.Add(FinalButton);
                i++;
                content.Add(FinalButton.Content.ToString());
                //Buttons.Add(FinalButton.Bid);
                
            }
            else if(SValue >= 10 && SValue <= 29)
            {
                for(int c = 0; c <= 4; c++)
                {
                    row = random.Next(27);
                    if (dispose.Contains(row))
                    {
                        row = random.Next(27);
                    }
                    else
                    {
                        NewButton button = new NewButton();
                        button.Content = ds.MiDEEValues.Rows[row][2].ToString();
                        //button.Bid = i;
                        button.Style = style;
                        mitigationDisplay.Children.Add(button);
                        i++;
                        content.Add(button.Content.ToString());
                        //Buttons.Add(button.Bid);
                        dispose.Add(row);
                        
                    }
                }
                NewButton FinalButton = new NewButton();
                FinalButton.Content = ds.MiDEEValues.Rows[26][2].ToString();
                //FinalButton.Bid = i;
                FinalButton.Style = style;
                mitigationDisplay.Children.Add(FinalButton);
                i++;
                content.Add(FinalButton.Content.ToString());
                //Buttons.Add(FinalButton.Bid);
                
            }
            else if(SValue >= 30)
            {
                for(int c = 0; c < 10; c++)
                {
                    row = random.Next(27);
                    if (dispose.Contains(row))
                    {
                        row = random.Next(27);
                    }
                    else
                    {
                        NewButton button = new NewButton();
                        button.Content = ds.MiDEEValues.Rows[row][2].ToString();
                        button.Style = style;
                        mitigationDisplay.Children.Add(button);
                        content.Add(button.Content.ToString());
                        dispose.Add(row);
                        
                    }
                }

                NewButton FinalButton = new NewButton();
                FinalButton.Content = ds.MiDEEValues.Rows[26][2].ToString();
                
                FinalButton.Style = style;
                mitigationDisplay.Children.Add(FinalButton);
                i++;
                content.Add(FinalButton.Content.ToString());
               
                
            }
            
            //foreach (var item in ds.MiDEEValues)
            /*for(int q = 0; q <= 26; q++)
            {
                NewButton button = new NewButton();
                button.Content = ds.MiDEEValues.Rows[q][2].ToString();
                button.Bid = q;
                button.Style = style;
                mitigationDisplay.Children.Add(button); 
                b++;
                content.Add(button.Content.ToString());
                Buttons.Add(button.Bid);
            }*/
            AddHandler(NewButton.ClickEvent, new RoutedEventHandler(button_Click));
        }

        public List<string> FilterButtons(List<string> exclusionBox)
        {
            SqlCommand cmd;
            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();

            MiDEDataSet ds = new MiDEDataSet();
            MiDEDataSet newds = new MiDEDataSet();
            MiDEDataSetTableAdapters.MiDEEValuesTableAdapter adapter = new MiDEDataSetTableAdapters.MiDEEValuesTableAdapter();
            MiDEDataSetTableAdapters.MiDEFilterWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEFilterWriteTableAdapter();
            
            List<string> FilteredList = new List<string>();
            
            exclusionBox = Home.ExclusionBox;

            string sqlString = "SELECT * FROM MiDEEValues WHERE StrategyName NOT IN ({StrategyName})";
            cmd = new SqlCommand(sqlString, conn);
            //var cmd = new SqlCommand("SELECT * FROM MiDEEValues WHERE StrategyName IN ({StrategyName})");
            cmd.AddArrayParameters("StrategyName", exclusionBox);
           
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dts = new DataTable("MiDEFilterWrite");

            da.Fill(dts);

            string EVariable;
            string StrategyName;
            int i = 0;
            foreach(var item in exclusionBox)
            {
                if (item == "Variety" || item == "Awareness" || item == "Surprise & Delight" || item == "Convenience")
                {
                    int t = 0;
                    StrategyName = item;
                    
                    wadapter.FilterByStrategyName(ds.MiDEFilterWrite, StrategyName);
                    foreach(var cell in ds.MiDEFilterWrite)
                    {
                        string strategyname = ds.MiDEFilterWrite.Rows[t][1].ToString();
                        string evariable = ds.MiDEFilterWrite.Rows[t][2].ToString();
                        wadapter.Insert(strategyname, evariable);
                        t++;
                    }
                    

                   
                       
                }
                else
                {
                    EVariable = item;
                    //adapter.FillByEVariable(ds.MiDEEValues, EVariable);
                    foreach (var text in ds.MiDEEValues)
                    {
                        FilteredList.Add(ds.MiDEEValues[i][2].ToString());
                        i++;
                    }
                }
            }



            
            
            
            
            
            Cmd.Parameters.AddWithValue("@StrategyName", exclusionBox);
           
            //sda.Fill(dt);

            return FilteredList;

            /*for(int j = 0; j <= exclusionBox.Count() - 1; j++)
            {
                ExclusionBoxEach.Add(exclusionBox[i]);
                i++;
            }*/

           // return ExclusionBoxEach;
        }

        public Button CreateButtons(List<string> exclusions)
        {
            

            
            int i = 0;
            

            
            
            NewButton button = new NewButton();
            Style style = FindResource("mButton") as Style;
            foreach (var item in exclusions)
            {
                
                
                button.Content = 
                //button.Content = ds.MiDEEValues.Rows[row][2].ToString();
                button.Style = style;
                mitigationDisplay.Children.Add(button);
                content.Add(button.Content.ToString());
                dispose.Add(row);
                i++;
            }
            return button;
        }

        #region Button Events
        void button_Click(object sender, RoutedEventArgs e)
        {
            string idk = e.OriginalSource.ToString();
            string bbase = "MiDEWPF.Models.NewButton: ";

            #region check for string match to Button RoutedEventArgs
            if (idk == bbase + "Food Truck Support")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[0][2].ToString());
                var evalue = ds.MiDEEValues.Rows[0][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Permanent Pop-Up")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[1][2].ToString());
                var evalue = ds.MiDEEValues.Rows[1][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Food Delivery Service")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[2][2].ToString());
                var evalue = ds.MiDEEValues.Rows[2][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Increase DTO Offerings/Stations")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[3][2].ToString());
                var evalue = ds.MiDEEValues.Rows[3][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Market@ Product Expansion")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[4][2].ToString());
                var evalue = ds.MiDEEValues.Rows[4][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Grab & Go ")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[5][2].ToString());
                var evalue = ds.MiDEEValues.Rows[5][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Simplify Existing Concepts")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[6][2].ToString());
                var evalue = ds.MiDEEValues.Rows[6][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Customer/Stakeholder Pre-Meetings")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[7][2].ToString());
                var evalue = ds.MiDEEValues.Rows[7][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Station Awareness Comms")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[8][2].ToString());
                var evalue = ds.MiDEEValues.Rows[8][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Readiness Tours")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[9][2].ToString());
                var evalue = ds.MiDEEValues.Rows[9][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "LTO Beverages")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[10][2].ToString());
                var evalue = ds.MiDEEValues.Rows[10][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Dedicated Shuttles")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[11][2].ToString());
                var evalue = ds.MiDEEValues.Rows[11][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Temp/Additional Water Stations")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[12][2].ToString());
                var evalue = ds.MiDEEValues.Rows[12][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Extended Service Times")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[13][2].ToString());
                var evalue = ds.MiDEEValues.Rows[13][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Discounts for Off-Time Service")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[14][2].ToString());
                var evalue = ds.MiDEEValues.Rows[14][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Replace Static Station(Local Brand)")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[15][2].ToString());
                var evalue = ds.MiDEEValues.Rows[15][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Welcome Events")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[16][2].ToString());
                var evalue = ds.MiDEEValues.Rows[16][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Goodbye Parties")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[17][2].ToString());
                var evalue = ds.MiDEEValues.Rows[17][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Welcome Parties")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[18][2].ToString());
                var evalue = ds.MiDEEValues.Rows[18][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Move Survival Kit")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[19][2].ToString());
                var evalue = ds.MiDEEValues.Rows[19][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Station Sampling")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[20][2].ToString());
                var evalue = ds.MiDEEValues.Rows[20][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Utilize Atrium & Non-Standard Spaces")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[21][2].ToString());
                var evalue = ds.MiDEEValues.Rows[21][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Replace Static Station(Our Concepts)")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[22][2].ToString());
                var evalue = ds.MiDEEValues.Rows[22][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Temporary Pop-Up")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[23][2].ToString());
                var evalue = ds.MiDEEValues.Rows[23][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Mobile Pop-Up")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[24][2].ToString());
                var evalue = ds.MiDEEValues.Rows[24][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Nextep Bank Install")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[25][2].ToString());
                var evalue = ds.MiDEEValues.Rows[25][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Linebusting")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[26][2].ToString());
                var evalue = ds.MiDEEValues.Rows[26][3].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }

            #endregion
        }

        private void ShowResults_Click(object sender, RoutedEventArgs e)
        {
            EValuesSum = EValues.Sum();

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
    }
}
