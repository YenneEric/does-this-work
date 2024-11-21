using System;
using System.Collections.Generic;
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

namespace View
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {

        public event EventHandler<CustomizeEventArgs>? CustomChange;
        public HomePage()
        {
            InitializeComponent();
        }

        private void EventClick(object sender, RoutedEventArgs e)
        {
            if(sender is Button b)
            {
                string name = b.Name;
                if(name == "InsertButton") CustomChange?.Invoke(this, new CustomizeEventArgs(name));
                
                if(name == "MostTd") CustomChange?.Invoke(this, new CustomizeEventArgs(name));
                
                if (name == "TopScoring") CustomChange?.Invoke(this, new CustomizeEventArgs(name));
                
                if (name == "ConfrenceWins") CustomChange?.Invoke(this, new CustomizeEventArgs(name));
                
                if (name == "MostTeamYards") CustomChange?.Invoke(this, new CustomizeEventArgs(name));

                if (name == "AddOthers") CustomChange?.Invoke(this, new CustomizeEventArgs(name));


            }


        }

        
    }
}
