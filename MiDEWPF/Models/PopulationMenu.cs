using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MiDEWPF.Models
{
    public class PopulationMenu : MenuItem
    {
        public void SampleMenuItem()
        {
            BuildingMenu mi = new BuildingMenu() { Header = "Small move" };
            BuildingMenu mi1 = new BuildingMenu() { Header = "Medium move" };
            BuildingMenu mi2 = new BuildingMenu() { Header = "Big Move" };
            AddChild(mi);
            AddChild(mi1);
            AddChild(mi2);
        }
    }
}
