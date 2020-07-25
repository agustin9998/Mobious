using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinnessLogicProject
{
    public static class ConfigurationManager
    {
        private static SystemData system = SystemData.Instance;

        public static void AddMoviesPath(string path)
        {
            system.SystemConfiguration.MoviesPathsEntered.Add(path);
            system.SystemConfiguration.MoviesPathsGenerated.Add(path);
            foreach (string d in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
            {
                if (!(system.SystemConfiguration.MoviesPathsEntered.Contains(d) && system.SystemConfiguration.MoviesPathsGenerated.Contains(d)))
                    system.SystemConfiguration.MoviesPathsGenerated.Add(d);
            }
        }

        public static void RemoveMoviePath(string path)
        {
            system.SystemConfiguration.MoviesPathsEntered.Remove(path);
            system.SystemConfiguration.MoviesPathsGenerated.Remove(path);
            foreach (string d in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
            {
                system.SystemConfiguration.MoviesPathsGenerated.Remove(d);
            }
        }

        public static void AddTvSeriesPath(string path)
        {
            system.SystemConfiguration.ShowPathsEntered.Add(path);
            system.SystemConfiguration.ShowPathsGenerated.Add(path);
            foreach (string d in Directory.GetDirectories(path, "*",SearchOption.AllDirectories))
            {
                if (!(system.SystemConfiguration.ShowPathsEntered.Contains(d) && system.SystemConfiguration.ShowPathsGenerated.Contains(d)))
                    system.SystemConfiguration.ShowPathsGenerated.Add(d);
            }
        }

        public static void RemoveTvSeriesPath(string path)
        {
            system.SystemConfiguration.ShowPathsEntered.Remove(path);
            system.SystemConfiguration.ShowPathsGenerated.Remove(path);
            foreach (string d in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
            {
                system.SystemConfiguration.ShowPathsGenerated.Remove(d);
            }
        }

        public static List<string> GetGeneratedMoviePaths()
        {
            return system.SystemConfiguration.MoviesPathsGenerated;
        }

        public static List<string> GetEnteredMoviePaths()
        {
            return system.SystemConfiguration.MoviesPathsEntered;
        }

        public static List<string> GetGeneratedShowPaths()
        {
            return system.SystemConfiguration.ShowPathsGenerated;
        }

        public static List<string> GetEnteredShowPaths()
        {
            return system.SystemConfiguration.ShowPathsEntered;
        }

        public static void ChangeMediaPlayerPath(string path)
        {
            system.SystemConfiguration.MediaPlayerPath = path;
        }

        public static string GetMediaPlayerPath()
        {
            return system.SystemConfiguration.MediaPlayerPath;
        }

        public static bool IsMoviePathIncluded(string path)
        {
            return system.SystemConfiguration.MoviesPathsEntered.Contains(path) || system.SystemConfiguration.MoviesPathsGenerated.Contains(path);
        }
    }
}
