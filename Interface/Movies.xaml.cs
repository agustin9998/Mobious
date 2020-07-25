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
    /// Interaction logic for Movies.xaml
    /// </summary>
    public partial class Movies : UserControl
    {
        public Movies()
        {
            InitializeComponent();
            ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
            foreach (Movie m in MovieManager.GetMovies())
            {
                movies.Add(m);
            }
            LBMovies.ItemsSource = movies;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (LBMovies.SelectedItem != null)
            {
                MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
                parentWindow.MovieInfo((Movie)LBMovies.SelectedItem);
            }
        }
    }
}
