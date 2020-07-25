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
using DomainProject;
using BusinnessLogicProject;
using System.Diagnostics;

namespace Interface
{
    /// <summary>
    /// Interaction logic for MovieInfo.xaml
    /// </summary>
    public partial class MovieInfo : UserControl
    {
        private Movie currentMovie;
        public MovieInfo(Movie movie)
        {
            InitializeComponent();

            currentMovie = movie;
            ImgPoster.ImageSource = new BitmapImage(new Uri(movie.Poster));
            LblTitle.Content = movie.Title;
            TBPlot.Text = movie.About;

            ObservableCollection<Actor> actors = new ObservableCollection<Actor>();
            foreach (Actor a in movie.Actors)
            {
                actors.Add(a);
            }
            ObservableCollection<Movie> relatedMovies = new ObservableCollection<Movie>();
            foreach (Movie m in MovieManager.GetRelatedMovies(movie))
            {
                relatedMovies.Add(m);
            }

            LblActors.ItemsSource = actors;
            LbMovies.ItemsSource = relatedMovies;
        }

        private void OnClickPlay(object sender, MouseButtonEventArgs e)
        {
            Process VLC = new Process();
            VLC.StartInfo.FileName = currentMovie.Route;
            VLC.Start();
        }

        private void MoviePosterMouseOver(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (MoviePoster.IsMouseDirectlyOver || PlayBackground.IsMouseDirectlyOver)
            {
                PlayBackground.Visibility = Visibility.Visible;
                BtnPlay.Visibility = Visibility.Visible;
            }
        }

        private void PlayBackgroundMouseOver(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!MoviePoster.IsMouseDirectlyOver && !PlayBackground.IsMouseDirectlyOver && !BtnPlay.IsMouseDirectlyOver)
            {
                PlayBackground.Visibility = Visibility.Hidden;
                BtnPlay.Visibility = Visibility.Hidden;
            }
        }

        private void ClickRelatedMovie(object sender, MouseButtonEventArgs e)
        {
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.MovieInfo((Movie)LbMovies.SelectedItem);
        }

        private void HandlePreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
    }
}
