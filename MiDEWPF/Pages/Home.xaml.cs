using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MiDEWPF.ViewModel;

namespace MiDEWPF.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        List<string> SelectionArray = new List<string>();
        List<string> ExclusionArray = new List<string>();
        List<string> PopArray = new List<string>();
        List<string> TypeArray = new List<string>();
        

        

        public Home()
        {
            InitializeComponent();
            
            MiDEDataSet ds = ((MiDEDataSet)(FindResource("mideDataSet")));

            //CollectionViewSource buildingViewSource = ((CollectionViewSource)(FindResource("buildingMenuViewSource")));
            //CollectionViewSource sFactorViewSource = ((CollectionViewSource)(FindResource("sFactorViewSource")));
            //CollectionViewSource sGFactorViewSource = ((CollectionViewSource)(FindResource("sGFactorViewSource")));
            //CollectionViewSource eFactorViewSource = ((CollectionViewSource)(FindResource("eFactorViewSource")));

            MiDEDataSetTableAdapters.MiDEBuildingsTableAdapter adapter = new MiDEDataSetTableAdapters.MiDEBuildingsTableAdapter();
            MiDEDataSetTableAdapters.MiDEPopulationTableAdapter padapter = new MiDEDataSetTableAdapters.MiDEPopulationTableAdapter();
            MiDEDataSetTableAdapters.MiDESValuesTableAdapter sadapter = new MiDEDataSetTableAdapters.MiDESValuesTableAdapter();
            MiDEDataSetTableAdapters.MiDEStrategyGroupsTableAdapter stadapter = new MiDEDataSetTableAdapters.MiDEStrategyGroupsTableAdapter();
            MiDEDataSetTableAdapters.MiDEEValuesTableAdapter eadapter = new MiDEDataSetTableAdapters.MiDEEValuesTableAdapter();

            adapter.Fill(ds.MiDEBuildings);
            padapter.Fill(ds.MiDEPopulation);
            sadapter.Fill(ds.MiDESValues);
            stadapter.Fill(ds.MiDEStrategyGroups);
            eadapter.Fill(ds.MiDEEValues);

            PopArray.Add("< 500");
            PopArray.Add("500 - 999");
            PopArray.Add("1000 - 1499");
            PopArray.Add("> 1500");

            TypeArray.Add("Executives");
            TypeArray.Add("Engineers");
            TypeArray.Add("Sales");
            TypeArray.Add("Marketing");
            TypeArray.Add("Support");
        }


        private void selectBuildingCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = selectBuildingCB.SelectedValue.ToString();
            selectionListBox.Items.Add(add);
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = datePicker.SelectedDate.ToString();
            selectionListBox.Items.Add(add);
        }

        private void sFactors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = sFactorCB.SelectedValue.ToString();
            selectionListBox.Items.Add(add);
        }

        private void selectExclusionCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = strategyExclusionCB.SelectedValue.ToString();
            exclusionSelectionListBox.Items.Add(add);
        }

        private void mitigationExclusion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string add = mitigationExclusionCB.SelectedValue.ToString();
            exclusionSelectionListBox.Items.Add(add);
        }
    }
}
