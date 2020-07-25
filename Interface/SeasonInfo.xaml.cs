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
using System.Windows.Media.Animation;
using System.Threading;

namespace Interface
{
    /// <summary>
    /// Interaction logic for SeasonInfo.xaml
    /// </summary>
    public partial class SeasonInfo : UserControl
    {
        private Season currentSeason;
        public SeasonInfo(Season season)
        {
            InitializeComponent();

            currentSeason = season;
            ObservableCollection<Episode> episodes = new ObservableCollection<Episode>();
            foreach (Episode e in season.Episodes)
            {
                episodes.Add(e);
            }
            LbEpisodes.ItemsSource = episodes;
            if (FileManager.FileExists(episodes[0].Poster))
                ImgPoster.ImageSource = new BitmapImage(new Uri(episodes[0].Poster));
            LblTitle.Content = episodes[0].Title;
            TBPlot.Text = episodes[0].About;
        }

        private void LbSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FileManager.FileExists(((Episode)LbEpisodes.SelectedItem).Poster))
                ImgPoster.ImageSource = new BitmapImage(new Uri(((Episode)LbEpisodes.SelectedItem).Poster));
            LblTitle.Content = ((Episode)LbEpisodes.SelectedItem).Title;
            TBPlot.Text = ((Episode)LbEpisodes.SelectedItem).About;
        }

        private void LbSelectionUp(object sender, MouseButtonEventArgs e)
        {
            Scroll.ScrollToVerticalOffset(0);
            Task timerTask = ScrollPeriodically(Scroll, TimeSpan.FromMilliseconds(1));
        }

        private async Task ScrollPeriodically(ScrollViewer sv, TimeSpan interval)
        {
            while (sv.VerticalOffset != 0)
            {
                sv.ScrollToVerticalOffset(sv.VerticalOffset - 50);
                await Task.Delay(interval);
            }
        }

        private void OnClickPlay(object sender, MouseButtonEventArgs e)
        {
            Process VLC = new Process();
            if (LbEpisodes.SelectedItem != null)
                VLC.StartInfo.FileName = ((Episode)LbEpisodes.SelectedItem).Route;
            else
                VLC.StartInfo.FileName = currentSeason.Episodes[0].Route;
            VLC.Start();
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
