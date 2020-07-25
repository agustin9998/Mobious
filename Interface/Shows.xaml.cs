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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinnessLogicProject;
using DomainProject;

namespace Interface
{
    /// <summary>
    /// Interaction logic for Shows.xaml
    /// </summary>
    public partial class Shows : UserControl
    {
        public Shows()
        {
            InitializeComponent();
            ObservableCollection<TvSeries> shows = new ObservableCollection<TvSeries>();
            foreach (TvSeries s in TvSeriesManager.GetShows())
            {
                shows.Add(s);
            }
            LBShows.ItemsSource = shows;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.ShowInfo((TvSeries)LBShows.SelectedItem);
        }
    }
}
