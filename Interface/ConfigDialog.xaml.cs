using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using BusinnessLogicProject;

namespace Interface
{
    /// <summary>
    /// Interaction logic for ConfigDialog.xaml
    /// </summary>
    public partial class ConfigDialog : Window
    {
        private AddMovieDirectory addMovieDirectory;
        private AddTvDirectory addTvDirectory;
        public ConfigDialog()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            foreach (string s in ConfigurationManager.GetEnteredMoviePaths())
            {
                LBMovieDir.Items.Add(s);
            }
            foreach (string s in ConfigurationManager.GetEnteredShowPaths())
            {
                LBTvDir.Items.Add(s);
            }
        }

        private void OnClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAddMovieDirectory(object sender, RoutedEventArgs e)
        {
            addMovieDirectory = new AddMovieDirectory();
            addMovieDirectory.ShowDialog();
            LBMovieDir.Items.Clear();
            foreach (string s in ConfigurationManager.GetEnteredMoviePaths())
            {
                LBMovieDir.Items.Add(s);
            }
        }

        private void BtnAddTvDirectory(object sender, RoutedEventArgs e)
        {
            addTvDirectory = new AddTvDirectory();
            addTvDirectory.ShowDialog();
            LBTvDir.Items.Clear();
            foreach (string s in ConfigurationManager.GetEnteredShowPaths())
            {
                LBTvDir.Items.Add(s);
            }
        }

        private void BtnRemoveMovieDirClick(object sender, RoutedEventArgs e)
        {
            if (LBMovieDir.SelectedItem != null)
            {
                ConfigurationManager.RemoveMoviePath((string)LBMovieDir.SelectedItem);
                LBMovieDir.Items.Clear();
                foreach (string s in ConfigurationManager.GetEnteredMoviePaths())
                {
                    LBMovieDir.Items.Add(s);
                }
            }
        }

        private void BtnRemoveTvDirClick(object sender, RoutedEventArgs e)
        {
            if (LBTvDir.SelectedItem != null)
            {
                ConfigurationManager.RemoveTvSeriesPath((string)LBTvDir.SelectedItem);
                LBTvDir.Items.Clear();
                foreach (string s in ConfigurationManager.GetEnteredShowPaths())
                {
                    LBTvDir.Items.Add(s);
                }
            }
        }
    }
}
