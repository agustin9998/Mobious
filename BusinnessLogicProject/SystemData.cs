using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DomainProject;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BusinnessLogicProject
{
    public class SystemData
    {
        public List<Movie> MoviesList { get; set; }
        public List<TvSeries> ShowList { get; set; }
        public Configuration SystemConfiguration { get; set; }

        private static readonly SystemData instance = new SystemData();
        private SystemData()
        {
            MoviesList = new List<Movie>();
            ShowList = new List<TvSeries>();
            SystemConfiguration = new Configuration();
        }
        static SystemData() { }

        public static SystemData Instance
        {
            get { return instance; }
        }

        public void SaveData()
        {
            string movieList = JsonConvert.SerializeObject(MoviesList);
            File.WriteAllText(@"Saved\MovieList.json", movieList);

            string tvSeriesList = JsonConvert.SerializeObject(ShowList);
            File.WriteAllText(@"Saved\TvSeriesList.json", tvSeriesList);

            string configuration = JsonConvert.SerializeObject(SystemConfiguration);
            File.WriteAllText(@"Saved\Configuration.json", configuration);
        }

        public void ReadData()
        {
            if (Directory.Exists(@"Saved")) {
                try {
                    MoviesList = JsonConvert.DeserializeObject<List<Movie>>(File.ReadAllText(@"Saved\MovieList.json"));
                }catch (Exception)
                {
                    string movieList = JsonConvert.SerializeObject(MoviesList);
                    File.WriteAllText(@"Saved\MovieList.json", movieList);
                }
                try
                {
                    ShowList = JsonConvert.DeserializeObject<List<TvSeries>>(File.ReadAllText(@"Saved\TvSeriesList.json"));
                }
                catch (Exception)
                {
                    string tvSeriesList = JsonConvert.SerializeObject(ShowList);
                    File.WriteAllText(@"Saved\TvSeriesList.json", tvSeriesList);
                }
                try
                {
                    SystemConfiguration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(@"Saved\Configuration.json"));
                }catch (Exception)
                {
                    string configuration = JsonConvert.SerializeObject(SystemConfiguration);
                    File.WriteAllText(@"Saved\Configuration.json", configuration);
                }
            }
            else
            {
                Directory.CreateDirectory(@"Saved");
                SaveData();
            }
        }
    }
}
