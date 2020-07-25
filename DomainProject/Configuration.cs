using System;
using System.Collections.Generic;
using System.Text;

namespace DomainProject
{
    public class Configuration
    {
        public List<string> MoviesPathsGenerated { get; set; }
        public List<string> MoviesPathsEntered { get; set; }
        public List<string> ShowPathsGenerated { get; set; }
        public List<string> ShowPathsEntered { get; set; }
        public string MediaPlayerPath { get; set; }

        public Configuration()
        {
            MoviesPathsGenerated = new List<string>();
            MoviesPathsEntered = new List<string>();
            ShowPathsGenerated = new List<string>();
            ShowPathsEntered = new List<string>();
        }
    }
}
