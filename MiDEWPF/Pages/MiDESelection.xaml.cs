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
        int i = 0;
        Home newhome = new Home();
        int ScenarioNumber;
        SqlCommand Cmd;

        List<String> content = new List<string>();
        MiDEDataSet ds = new MiDEDataSet();
        
        List<int> Buttons = new List<int>();
        
        public MiDESelection()
        {
            Style style = FindResource("mButton") as Style;
            
            InitializeComponent();
            
            MiDEDataSetTableAdapters.MiDEEValuesTableAdapter eadapter = new MiDEDataSetTableAdapters.MiDEEValuesTableAdapter();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            MiDEDataSetTableAdapters.MiDEWriteTableAdapter wadapter2 = new MiDEDataSetTableAdapters.MiDEWriteTableAdapter();
            eadapter.Fill(ds.MiDEEValues);
            wadapter.Fill(ds.MiDEWrite);
           
            

            ScenarioNumber = newhome.ScenarioNumber;
            ScenarioNumber--;

            wadapter.FillByScenarioNumber(ds.MiDEWrite, ScenarioNumber);
            currentScenarioLB.ItemsSource = ds.MiDEWrite;

            

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

            foreach (var item in ds.MiDEEValues)
            {
                NewButton button = new NewButton();
                button.Content = ds.MiDEEValues.Rows[i][1].ToString();
                button.Bid = i;
                button.Style = style;
                mitigationDisplay.Children.Add(button); 
                i++;
                content.Add(button.Content.ToString());
                Buttons.Add(button.Bid);
            }
            AddHandler(NewButton.ClickEvent, new RoutedEventHandler(button_Click));
        }

        

        void button_Click(object sender, RoutedEventArgs e)
        {
            string idk = e.OriginalSource.ToString();
            string bbase = "MiDEWPF.Models.NewButton: ";

            #region check for string match to Button RoutedEventArgs
            if (idk ==  bbase + "Food Truck Support")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[0][1].ToString());
            }
            else if(idk == bbase + "Full Service Pop-Ups")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[1][1].ToString());
            }
            else if(idk == bbase + "Food Delivery Service")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[2][1].ToString());
            }
            else if (idk == bbase + "Increase DTO Offerings/Stations")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[3][1].ToString());
            }
            else if (idk == bbase + "Market@ Product Expansion")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[4][1].ToString());
            }
            else if (idk == bbase + "Grab & Go ")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[5][1].ToString());
            }
            else if (idk == bbase + "Simplify Existing Concepts")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[6][1].ToString());
            }
            else if (idk == bbase + "Leveraging Ambassadors")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[7][1].ToString());
            }
            else if (idk == bbase + "Customer/Stakeholder Pre-Meetings")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[8][1].ToString());
            }
            else if (idk == bbase + "Station Awareness Comms")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[9][1].ToString());
            }
            else if (idk == bbase + "Readiness Tours")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[10][1].ToString());
            }
            else if (idk == bbase + "LTO Beverages")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[11][1].ToString());
            }
            else if (idk == bbase + "Dedicated Shuttles")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[12][1].ToString());
            }
            else if (idk == bbase + "Temp/Additional Water Stations")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[13][1].ToString());
            }
            else if (idk == bbase + "Extended Service Times")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[14][1].ToString());
            }
            else if (idk == bbase + "Discounts for Off-Time Service")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[15][1].ToString());
            }
            else if (idk == bbase + "Temp Service Pop-Ups")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[16][1].ToString());
            }
            else if (idk == bbase + "Welcome Events")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[17][1].ToString());
            }
            else if (idk == bbase + "Goodbye Parties")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[18][1].ToString());
            }
            else if (idk == bbase + "Welcome Parties")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[19][1].ToString());
            }
            else if (idk == bbase + "Move Survival Kit")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[20][1].ToString());
            }
            else if (idk == bbase + "Station Sampling")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[21][1].ToString());
            }
            else if (idk == bbase + "Utilize Atrium & Non-Standard Spaces")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[22][1].ToString());
            }
            else if (idk == bbase + "Replace Static Station")
            {
                currentMitigationListBox.Items.Add(ds.MiDEEValues.Rows[23][1].ToString());
            }

            #endregion
        }
    }
}
