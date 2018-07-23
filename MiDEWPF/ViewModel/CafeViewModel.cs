using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MiDEWPF.Models;
using MiDEWPF.Resources;
using System.Data.SqlClient;
using System.ComponentModel;

namespace MiDEWPF.ViewModel
{
    public class CafeViewModel : INotifyPropertyChanged
    {
       

        public ObservableCollection<Cafe> Cafes
        {
            get;
            set;
        }

        public ObservableCollection<Population> Populations
        {
            get;
            set;
        }

       /* public ObservableCollection<PopulationType> PopulationTypes
        {
            get;
            set;
        }*/

        public void LoadCafes()
        {

            ObservableCollection<Cafe> cafes = new ObservableCollection<Cafe>();

            cafes.Add(new Cafe { CafeName = "H" });
            cafes.Add(new Cafe { CafeName = "83" });
            cafes.Add(new Cafe { CafeName = "40/41" });
            cafes.Add(new Cafe { CafeName = "Millenium" });
            cafes.Add(new Cafe { CafeName = "Samm-C" });

            Cafes = cafes;
        }

        /*public void LoadPopulations()
        {
            ObservableCollection<Population> populations = new ObservableCollection<Population>();

            populations.Add(new Population { PopAmount = "< 500" });
            populations.Add(new Population { PopAmount = "500 - 999" });
            populations.Add(new Population { PopAmount = "1000 - 1299" });
            populations.Add(new Population { PopAmount = "1300 - 1500" });
            populations.Add(new Population { PopAmount = "> 1500" });

            Populations = populations;
        }*/

       /* public void LoadPopulationTypes()
        {
            ObservableCollection<PopulationType> populationtypes = new ObservableCollection<PopulationType>();

            populationtypes.Add(new PopulationType { PopType = "Executive" });
            populationtypes.Add(new PopulationType { PopType = "Marketing" });
            populationtypes.Add(new PopulationType { PopType = "Sales" });
            populationtypes.Add(new PopulationType { PopType = "Engineers" });
            populationtypes.Add(new PopulationType { PopType = "Support" });

            PopulationTypes = populationtypes;
        }*/

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
