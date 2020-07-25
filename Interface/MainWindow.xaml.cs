using System;
using System.Xaml;
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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinnessLogicProject;
using DomainProject;

namespace Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserControl movies;
        private UserControl series;
        private SystemData system;
        private ConfigDialog configuration;

        public MainWindow()
        {
            system = SystemData.Instance;
            system.ReadData();
            
            movies = new Movies();
            series = new Shows();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            
            ContentControl.Content = movies;
        }

        private async void LoadingMovies()
        {
            UnablesMiddleBar.Visibility = Visibility.Visible;
            UnablesTopBar.Visibility = Visibility.Visible;
            UnablesUserControl.Visibility = Visibility.Visible;
            LblLoading.Visibility = Visibility.Visible;

            await MovieManager.AnalyseMovieDirectories();
            await TvSeriesManager.AnalyseDirectories();

            UnablesMiddleBar.Visibility = Visibility.Hidden;
            UnablesTopBar.Visibility = Visibility.Hidden;
            UnablesUserControl.Visibility = Visibility.Hidden;
            LblLoading.Visibility = Visibility.Hidden;
        }

        public void RefreshMovies()
        {
            movies = new Movies();
            ContentControl.Content = movies;
        }

        public void MovieInfo(Movie movie)
        {
            ContentControl.Content = new MovieInfo(movie);
        }

        public void ShowInfo(TvSeries show)
        {
            ContentControl.Content = new ShowInfo(show);
        }

        public void SeasonInfo(Season season)
        {
            ContentControl.Content = new SeasonInfo(season);
        }

        private void BtnMovieClick(object sender, RoutedEventArgs e)
        {
            RefreshMovies();
        }

        private void OnClickClose(object sender, RoutedEventArgs e)
        {
            system.SaveData();
            Close();
        }

        private void OnClickMin(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OnClickMax(object sender, RoutedEventArgs e)
        {
            if (WindowState.Maximized == WindowState)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
                this.MaxHeight = System.Windows.SystemParameters.WorkArea.Height + 14;
            }
        }

        private void RectangleDragWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnSeriesClick(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = series;
        }

        private void BtnConfigClick(object sender, RoutedEventArgs e)
        {
            configuration = new ConfigDialog();
            configuration.ShowDialog();
        }

        private void BtnUpdateClick(object sender, RoutedEventArgs e)
        {
            LoadingMovies();
            RefreshMovies();
        }
    }
}
