using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MiDEWPF.Models
{
    public class BuildingMenu : MenuItem
    {
        public void SampleSubMenuItem()
        {
            MenuItem si = new MenuItem() { Header = "Jerks" };
            MenuItem si1 = new MenuItem() { Header = "Douchebags" };
            MenuItem si2 = new MenuItem() { Header = "Entitled children" };
            /*si.Click += MyFunction;
            si1.Click += MyFunction;
            si2.Click += MyFunction;*/
            AddChild(si);
            AddChild(si1);
            AddChild(si2);
        }
        /*private void MyFunction(object sender, RoutedEventArgs e)
        {
            MenuItem s = sender;
            MenuItem p = s.Parent;
            MenuItem p1 = p.Parent;
            MessageBox.Show(p1.Header + " is a " + p.Header + " comprised of " + s.Header + ".");
        }*/
    }
}
