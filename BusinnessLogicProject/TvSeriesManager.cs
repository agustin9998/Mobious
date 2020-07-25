using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DomainProject;

namespace BusinnessLogicProject
{
    public static class TvSeriesManager
    {
        private static SystemData system = SystemData.Instance;

        public static List<string> GetShowFiles()
        {
            //Collects all files in the TvSeries directories
            List<string> showFiles = new List<string>();

            foreach (string path in ConfigurationManager.GetGeneratedShowPaths())
            {
                foreach (string f in Directory.GetFiles(path, "*.mp4"))
                {
                    showFiles.Add(f);
                }
                foreach (string f in Directory.GetFiles(path, "*.m4v"))
                {
                    showFiles.Add(f);
                }
                foreach (string f in Directory.GetFiles(path, "*.mkv"))
                {
                    showFiles.Add(f);
                }
                foreach (string f in Directory.GetFiles(path, "*.mov"))
                {
                    showFiles.Add(f);
                }
                foreach (string f in Directory.GetFiles(path, "*.wmv"))
                {
                    showFiles.Add(f);
                }
                foreach (string f in Directory.GetFiles(path, "*.flv"))
                {
                    showFiles.Add(f);
                }
                foreach (string f in Directory.GetFiles(path, "*.avi"))
                {
                    showFiles.Add(f);
                }
            }
            return showFiles;
        }

        public static List<TvSeries> GetShows()
        {
            return system.ShowList;
        }

        public static bool CheckShowIncluded(string name)
        {
            return system.ShowList.Find(x => FileManager.ReplaceForbiddenCharacters(x.Title) == name) != null;
        }

        public static bool CheckSeasonIncluded(string name, string season)
        {
            if (season.Length > 0 && season[0] == '0')
                season = season.Remove(0, 1);
            if (system.ShowList.Find(x => FileManager.ReplaceForbiddenCharacters(x.Title) == name) != null)
                return system.ShowList.Find(x => FileManager.ReplaceForbiddenCharacters(x.Title) == name).Seasons.Find(y => y.SeasonNumber == season) != null;
            return false;
        }

        public static bool CheckEpisodeIncluded(string name, string season, string episode)
        {
            if (season.Length > 0 && season[0] == '0')
                season = season.Remove(0, 1);
            if (episode[0] == '0')
                episode = episode.Remove(0, 1);
            if (system.ShowList.Find(x => FileManager.ReplaceForbiddenCharacters(x.Title) == name) != null &&
                system.ShowList.Find(x => FileManager.ReplaceForbiddenCharacters(x.Title) == name).Seasons.Find(y => y.SeasonNumber == season) != null)
                return system.ShowList.Find(x => FileManager.ReplaceForbiddenCharacters(x.Title) == name).Seasons.Find(y => y.SeasonNumber == season).Episodes.Find(z => z.Number == episode) != null;
            return false;
        }

        public static async Task AnalyseDirectories()
        {
            List<string> showFiles = TvSeriesManager.GetShowFiles();

            //Includes every Tv Series that was not already included
            foreach (string path in showFiles)
            {
                string fileName = Path.GetFileName(path);
                    string show = GetShowName(fileName);
                    string season = GetSeasonNumber(fileName);
                    string episode = GetEpisodeNumber(fileName);

                    if (!CheckShowIncluded(show))
                    {
                        ApiObject apiShow = await ApiManager.GetShowApiFile(show);
                        if (apiShow.name != null)
                            TvSeriesManager.AddShow(apiShow);
                    }
                    if (!CheckSeasonIncluded(show, season))
                    {
                        ApiObject apiSeason = await ApiManager.GetSeasonApiFile(show, season);
                        if (apiSeason.name != null)
                            TvSeriesManager.AddSeason(show, apiSeason);
                    }
                    if (!CheckEpisodeIncluded(show, season, episode))
                    {
                        ApiObject apiEpisode = await ApiManager.GetEpisodeApiFile(show, season, episode);
                        if (apiEpisode.name != null)
                            TvSeriesManager.AddEpisode(show, season, apiEpisode, path);
                    }
            }
        }

        private static void AddEpisode(string show, string season, ApiObject apiEpisode, string path)
        {
            if (season[0] == '0')
                season = season.Remove(0, 1);
            Episode episode = new Episode(apiEpisode.name, apiEpisode.overview, apiEpisode.episode_number, path, apiEpisode.id);
            int indexShow = system.ShowList.FindIndex(x => FileManager.ReplaceForbiddenCharacters(x.Title) == show);
            int indexSeason = system.ShowList.Find(x => FileManager.ReplaceForbiddenCharacters(x.Title) == show).Seasons.FindIndex(y => y.SeasonNumber == season);
            system.ShowList[indexShow].Seasons[indexSeason].Episodes.Add(episode);
        }

        private static void AddSeason(string show, ApiObject apiSeason)
        {
            Season season = new Season(apiSeason.name, apiSeason.overview, apiSeason.season_number, apiSeason.air_date, new List<Episode>(), apiSeason.id);
            system.ShowList.Find(x => FileManager.ReplaceForbiddenCharacters(x.Title) == show).Seasons.Add(season);
        }

        private static void AddShow(ApiObject apiShow)
        {
            TvSeries show = new TvSeries(apiShow.name, apiShow.overview, apiShow.first_air_date, apiShow.last_air_date, new List<Season>(), apiShow.id);
            system.ShowList.Add(show);
        }

        private static string GetEpisodeNumber(string fileName)
        {
            string regex = @"\bS\d{2}E\d{2}\b";
            string seasonEpisode = Regex.Match(fileName, regex).ToString();
            if (seasonEpisode.Split('E').Length > 1)
                return seasonEpisode.Split('E')[1];
            return "0";
        }

        private static string GetSeasonNumber(string fileName)
        {
            string regex = @"\bS\d{2}E\d{2}\b";
            string seasonEpisode = Regex.Match(fileName, regex).ToString();
            return seasonEpisode.Split('E')[0].Replace("S", "");
        }

        private static string GetShowName(string fileName)
        {
            string regex = @"\bS\d{2}E\d{2}\b";
            string[] splited = Regex.Split(fileName, regex);

            string show = splited[0];
            string ret = FileManager.ReplaceForbiddenCharacters(show.Replace("."," ")).Replace("-", "");
            while (ret[ret.Length -1] == ' ')
            {
                ret = ret.Remove(ret.Length - 1);
            }
            return ret;
        }
    }
}