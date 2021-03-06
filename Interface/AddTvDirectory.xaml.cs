﻿using System;
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
using System.Windows.Shapes;
using DomainProject;
using BusinnessLogicProject;
using System.IO;

namespace Interface
{
    /// <summary>
    /// Interaction logic for AddMovieDirectory.xaml
    /// </summary>
    public partial class AddTvDirectory : Window
    {
        public AddTvDirectory()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void OnClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnApplyClick(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(TBTvPath.Text))
            {
                ConfigurationManager.AddTvSeriesPath(TBTvPath.Text);
                this.Close();
            }
            else
            {
                LblError.Visibility = Visibility.Visible;
            }
        }
    }
}
