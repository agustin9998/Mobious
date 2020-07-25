using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DomainProject;

namespace BusinnessLogicProject
{
    public static class MovieManager
    {
        private static SystemData system = SystemData.Instance;

        public static void Add(ApiObject apiFile, string path)
        {
            Movie movie = new Movie(apiFile.title, apiFile.runtime, apiFile.overview, apiFile.release_date, path, apiFile.cast, apiFile.id);
            system.MoviesList.Add(movie);
        }

        public static bool IsIncluded(string path)
        {
            List<Movie> movies = GetMovies();
            foreach(Movie m in movies)
            {
                if (m.Route.Equals(path))
                    return true;
            }
            return false;
        }

        public static List<string> GetMovieFiles()
        {
            //Collects all files in the Movies directories
            List<string> movieFiles = new List<string>();

            foreach (string path in ConfigurationManager.GetGeneratedMoviePaths())
            {
                foreach (string f in Directory.GetFiles(path, "*.mp4"))
                {
                    movieFiles.Add(f);
                }
                foreach (string f in Directory.GetFiles(path, "*.mkv"))
                {
                    movieFiles.Add(f);
                }
                foreach (string f in Directory.GetFiles(path, "*.mov"))
                {
                    movieFiles.Add(f);
                }
                foreach (string f in Directory.GetFiles(path, "*.wmv"))
                {
                    movieFiles.Add(f);
                }
                foreach (string f in Directory.GetFiles(path, "*.flv"))
                {
                    movieFiles.Add(f);
                }
                foreach (string f in Directory.GetFiles(path, "*.avi"))
                {
                    movieFiles.Add(f);
                }
            }
            return movieFiles;
        }

        public static string GetProperMovieName(string fileName)
        {
            string ret = "";
            string pattern = @"\b(19|20)\d{2}\b";

            //Splits Movie Name from quality info
            string[] splited = Regex.Split(fileName, pattern);
            fileName = splited[0];

            ret = fileName.Replace(".", " ").Remove(fileName.Length - 1);

            return ret;
        }

        public static string GetProperMovieYear(string fileName)
        {
            string ret = "";
            string pattern = "\\d\\d\\d[\\d]*p";

            //Splits Movie Name from quality info
            string[] splited = Regex.Split(fileName, pattern);
            fileName = splited[0];

            splited = fileName.Split('.');
            ret = splited[splited.Length - 2];
            return ret;
        }

        public static async Task AnalyseMovieDirectories()
        {
            List<string> movieFiles = GetMovieFiles();

            //Includes every movie that was not already included
            foreach (string path in movieFiles)
            {
                if (!MovieManager.IsIncluded(path))
                {
                    string fileName = Path.GetFileName(path);

                    string name = GetProperMovieName(fileName);
                    string year = GetProperMovieYear(fileName);
                    var apiFile = await ApiManager.GetMovieApiFile(name, year);
                    if (apiFile != null)
                    {
                        Add(apiFile, path);
                    }
                }
            }
        }

        public static List<Movie> GetMovies()
        {
            return system.MoviesList;
        }

        public static List<Movie> GetRelatedMovies(Movie movie)
        {
            List<Movie> ret = new List<Movie>();
            foreach(Movie m in GetMovies())
            {
                bool included = false;
                for (int currentActor = 0; currentActor < 4 && currentActor < movie.Actors.Count; currentActor++)
                {
                    if ((m.Actors.Contains(movie.Actors[currentActor]) || CheckIsFranchise(m, movie)) && !included && !m.Equals(movie))
                    {
                        ret.Add(m);
                        included = true;
                    }
                }
            }
            return ret;
        }

        public static bool CheckIsFranchise(Movie movieToCheck, Movie movieFranchise)
        {
            return movieFranchise.Title.Contains(movieToCheck.Title.Split(':')[0]) || movieToCheck.Title.Contains(movieFranchise.Title.Split(':')[0]);
        }
    }
    }
