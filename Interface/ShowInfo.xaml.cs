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
    /// Interaction logic for ShowInfo.xaml
    /// </summary>
    public partial class ShowInfo : UserControl
    {
        private TvSeries currentShow;
        public ShowInfo(TvSeries show)
        {
            InitializeComponent();

            currentShow = show;
            if (FileManager.FileExists(show.Poster))
                ImgPoster.ImageSource = new BitmapImage(new Uri(show.Poster));
            LblTitle.Content = show.Title;
            TBPlot.Text = show.About;
            ObservableCollection<Season> seasons = new ObservableCollection<Season>();
            foreach (Season s in show.Seasons)
            {
                seasons.Add(s);
            }
            LbSeasons.ItemsSource = seasons;
        }

        private void BtnEpisodeUp(object sender, SelectionChangedEventArgs e)
        {
            MainWindow parentWindow = (MainWindow)Window.GetWindow(this);
            parentWindow.SeasonInfo((Season)LbSeasons.SelectedItem);
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
