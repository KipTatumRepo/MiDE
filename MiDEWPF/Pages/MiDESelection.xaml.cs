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
        List<String> content = new List<string>();
        MiDEDataSet ds = new MiDEDataSet();
        List<int> Buttons = new List<int>();
        Random random = new Random();
        int row;
        List<int> dispose = new List<int>();
        #endregion

        #region Global Variables
        public static int EValuesSum;
        public List<int> EValues = new List<int>();
        Home newhome = new Home();
        #endregion

        //Random Number Generator for testing purposes
        
        
        public MiDESelection()
        {
            //import style for buttons
            Style style = FindResource("mButton") as Style;
            
            InitializeComponent();

            #region Get Data
            MiDEDataSetTableAdapters.MiDEEValuesTableAdapter eadapter = new MiDEDataSetTableAdapters.MiDEEValuesTableAdapter();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter2 = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            eadapter.Fill(ds.MiDEEValues);
            wadapter.Fill(ds.MiDEWrite);

            SqlConnection conn = ConnectionHelper.GetConn();
            conn.Open();
            string sqlString = "SELECT ScenarioNumber, SelectionListBox, ExclusionListBox, CurrentMitigationListBox, wid FROM MiDEWrite WHERE(ScenarioNumber = @ScenarioNumber) AND(ExclusionListBox IS NOT NULL)";
            Cmd = new SqlCommand(sqlString, conn);
            SqlDataAdapter sda = new SqlDataAdapter(Cmd);
            Cmd.Parameters.AddWithValue("@ScenarioNumber", ScenarioNumber);
            DataTable dt = new DataTable("MiDEWrite");

            sda.Fill(dt);

            currentExclusionLB.ItemsSource = dt.DefaultView;
            conn.Close();

            wadapter.FillByScenarioNumber(ds.MiDEWrite, ScenarioNumber);
            currentScenarioLB.ItemsSource = ds.MiDEWrite;
            #endregion

            //Get sum of S values and current Scenario Number from Home page.  For some reason it increments by 1 before coming here
            //so we have to subtract 1 to make sure it all matches
            SValue = Home.SValuesSum;
            ScenarioNumber = newhome.ScenarioNumber;
            ScenarioNumber--;

            
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
                        button.Content = ds.MiDEEValues.Rows[row][1].ToString();
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
                FinalButton.Content = ds.MiDEEValues.Rows[26][1].ToString();
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
                        button.Content = ds.MiDEEValues.Rows[row][1].ToString();
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
                FinalButton.Content = ds.MiDEEValues.Rows[26][1].ToString();
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
                        button.Content = ds.MiDEEValues.Rows[row][1].ToString();
                       
                        button.Style = style;
                        mitigationDisplay.Children.Add(button);
                        
                        content.Add(button.Content.ToString());
                        
                        dispose.Add(row);
                        
                    }
                }

                NewButton FinalButton = new NewButton();
                FinalButton.Content = ds.MiDEEValues.Rows[26][1].ToString();
                
                FinalButton.Style = style;
                mitigationDisplay.Children.Add(FinalButton);
                i++;
                content.Add(FinalButton.Content.ToString());
               
                
            }
            
            //foreach (var item in ds.MiDEEValues)
            /*for(int q = 0; q <= 26; q++)
            {
                NewButton button = new NewButton();
                button.Content = ds.MiDEEValues.Rows[q][1].ToString();
                button.Bid = q;
                button.Style = style;
                mitigationDisplay.Children.Add(button); 
                b++;
                content.Add(button.Content.ToString());
                Buttons.Add(button.Bid);
            }*/
            AddHandler(NewButton.ClickEvent, new RoutedEventHandler(button_Click));
        }
        #region Button Events
        void button_Click(object sender, RoutedEventArgs e)
        {
            string idk = e.OriginalSource.ToString();
            string bbase = "MiDEWPF.Models.NewButton: ";

            #region check for string match to Button RoutedEventArgs
            if (idk == bbase + "Food Truck Support")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[0][1].ToString());
                var evalue = ds.MiDEEValues.Rows[0][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Permanent Pop-Up")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[1][1].ToString());
                var evalue = ds.MiDEEValues.Rows[1][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Food Delivery Service")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[2][1].ToString());
                var evalue = ds.MiDEEValues.Rows[2][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Increase DTO Offerings/Stations")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[3][1].ToString());
                var evalue = ds.MiDEEValues.Rows[3][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Market@ Product Expansion")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[4][1].ToString());
                var evalue = ds.MiDEEValues.Rows[4][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Grab & Go ")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[5][1].ToString());
                var evalue = ds.MiDEEValues.Rows[5][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Simplify Existing Concepts")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[6][1].ToString());
                var evalue = ds.MiDEEValues.Rows[6][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Customer/Stakeholder Pre-Meetings")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[7][1].ToString());
                var evalue = ds.MiDEEValues.Rows[7][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Station Awareness Comms")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[8][1].ToString());
                var evalue = ds.MiDEEValues.Rows[8][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Readiness Tours")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[9][1].ToString());
                var evalue = ds.MiDEEValues.Rows[9][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "LTO Beverages")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[10][1].ToString());
                var evalue = ds.MiDEEValues.Rows[10][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Dedicated Shuttles")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[11][1].ToString());
                var evalue = ds.MiDEEValues.Rows[11][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Temp/Additional Water Stations")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[12][1].ToString());
                var evalue = ds.MiDEEValues.Rows[12][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Extended Service Times")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[13][1].ToString());
                var evalue = ds.MiDEEValues.Rows[13][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Discounts for Off-Time Service")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[14][1].ToString());
                var evalue = ds.MiDEEValues.Rows[14][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Replace Static Station(Local Brand)")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[15][1].ToString());
                var evalue = ds.MiDEEValues.Rows[15][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Welcome Events")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[16][1].ToString());
                var evalue = ds.MiDEEValues.Rows[16][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Goodbye Parties")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[17][1].ToString());
                var evalue = ds.MiDEEValues.Rows[17][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Welcome Parties")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[18][1].ToString());
                var evalue = ds.MiDEEValues.Rows[18][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Move Survival Kit")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[19][1].ToString());
                var evalue = ds.MiDEEValues.Rows[19][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Station Sampling")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[20][1].ToString());
                var evalue = ds.MiDEEValues.Rows[20][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Utilize Atrium & Non-Standard Spaces")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[21][1].ToString());
                var evalue = ds.MiDEEValues.Rows[21][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Replace Static Station(Our Concepts)")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[22][1].ToString());
                var evalue = ds.MiDEEValues.Rows[22][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Temporary Pop-Up")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[23][1].ToString());
                var evalue = ds.MiDEEValues.Rows[23][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Mobile Pop-Up")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[24][1].ToString());
                var evalue = ds.MiDEEValues.Rows[24][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Nextep Bank Install")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[25][1].ToString());
                var evalue = ds.MiDEEValues.Rows[25][2].ToString();
                int Evalue = int.Parse(evalue);

                EValues.Add(Evalue);
            }
            else if (idk == bbase + "Linebusting")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[26][1].ToString());
                var evalue = ds.MiDEEValues.Rows[26][2].ToString();
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
