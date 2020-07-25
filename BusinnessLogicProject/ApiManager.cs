using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainProject;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.IO;

namespace BusinnessLogicProject
{
    public static class ApiManager
    {
        private static readonly string key = "?api_key=76ced4ab0df86c2fbcf9f1ad2838357f";

        private static async Task DownloadImageAsync(string directoryPath, string fileName, Uri uri)
        {
            using (var httpClient = new HttpClient())
            {
                // Get the file extension
                var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
                var fileExtension = Path.GetExtension(uriWithoutQuery);

                // Create file path and ensure directory exists
                var path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
                Directory.CreateDirectory(directoryPath);

                // Download the image and write to the file
                var imageBytes = await httpClient.GetByteArrayAsync(uri);
                File.WriteAllBytes(path, imageBytes);
            }
        }

        public static async Task<ApiObject> GetMovieApiFile(string file, string year)
        {
            ApiObject apiFile = null;
            List<string> isNot = new List<string>();
            string id = "Not null";

            using (var client = new HttpClient())
            {
                //Initializes client
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                while (apiFile == null && !id.Equals(""))
                {
                    id = GetFileId(file, isNot, @"IdJSON\movie_ids.JSON");
                    isNot.Add(id);
                    string url = "https://api.themoviedb.org/3/movie/" + id;

                    apiFile = await GetJsonFromResponse(client, url);
                    if (apiFile.title != "" && apiFile.release_date.Split('-')[0].Equals(year))
                    {
                        apiFile.cast = await GetActors(id, client);
                        DownloadActorsImages(apiFile.cast, id);

                        DownloadPoster(apiFile.poster_path, apiFile.title, id);
                    }
                    else
                    {
                        apiFile = null;
                    }
                }
            }
            return apiFile;
        }

        public static async Task<ApiObject> GetShowApiFile(string series)
        {
            ApiObject apiFile = null;
            string id = "Not null";

            using (var client = new HttpClient())
            {
                //Initializes client
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                id = GetFileId(series, new List<string>(), @"IdJSON\tv_series_ids.JSON");
                string url = "https://api.themoviedb.org/3/tv/" + id;
                apiFile = await GetJsonFromResponse(client, url);
                if (apiFile.name != null && apiFile.poster_path != null)
                {
                    DownloadPoster(apiFile.poster_path, apiFile.name, id);
                }
            }
            return apiFile;
        }

        public static async Task<ApiObject> GetSeasonApiFile(string series, string season)
        {
            ApiObject apiFile = null;
            string id = "Not null";

            using (var client = new HttpClient())
            {
                //Initializes client
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                id = GetFileId(series, new List<string>(), @"IdJSON\tv_series_ids.JSON");
                string url = "https://api.themoviedb.org/3/tv/" + id + "/season/" + season;
                apiFile = await GetJsonFromResponse(client, url);
                if (apiFile.name != null && apiFile.poster_path != null)
                {
                    DownloadPoster(apiFile.poster_path, apiFile.name, apiFile.id);
                }
            }
            return apiFile;
        }

        public static async Task<ApiObject> GetEpisodeApiFile(string series, string season, string episode)
        {
            ApiObject apiFile = null;
            string id = "Not null";

            using (var client = new HttpClient())
            {
                //Initializes client
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                id = GetFileId(series, new List<string>(), @"IdJSON\tv_series_ids.JSON");
                string url = "https://api.themoviedb.org/3/tv/" + id + "/season/" + season + "/episode/" + episode;
                apiFile = await GetJsonFromResponse(client, url);
                if (apiFile.name != null && apiFile.still_path != null)
                {
                    DownloadPoster(apiFile.still_path, apiFile.name, apiFile.id);
                }
            }
            return apiFile;
        }  

        public static async Task<ApiObject> GetJsonFromResponse(HttpClient client, string url)
        {
            ApiObject apiData = null;

            using (HttpResponseMessage response = await client.GetAsync(url + key))
            {
                try
                {
                    apiData = await response.Content.ReadAsAsync<ApiObject>();
                }
                catch (Exception) { }
            }
            return apiData;
        }

        public static async void DownloadPoster(string posterPath, string title, string id)
        {
            Uri imageUrl = new Uri("https://image.tmdb.org/t/p/w500/" + posterPath);
            await DownloadImageAsync(Path.GetFullPath(@"Posters\"), FileManager.ReplaceForbiddenCharacters(title) + id, imageUrl);
        }

        public static async void DownloadActorsImages(List<Actor> actors, string id)
        {
            int cant = 0;

            foreach (Actor a in actors)
            {
                string combined = Path.Combine(@"ActorImgs\", id, ".jpg");
                if (cant <= 15 && a.ProfilePath != null && !FileManager.FileExists(Path.GetFullPath(combined)))
                {
                    await DownloadImageAsync(Path.GetFullPath(@"ActorImgs\"), a.Id, new Uri("https://image.tmdb.org/t/p/w500/" + a.ProfilePath));
                    cant++;
                }
            }
        }

        public static string GetFileId(string file, List<string> isNot, string fileJson)
        {
            string id = "";
            using (StreamReader reader = new StreamReader(fileJson))
            {
                JObject line;
                bool found = false;

                while (reader.Peek() >= 0 && !found)
                {
                    line = JObject.Parse(reader.ReadLine());
                    string token = (string)line.GetValue("original_title");
                    if (token == null)
                        token = (string)line.GetValue("original_name");
                    token = FileManager.ReplaceForbiddenCharacters(token).ToLower();
                    if (token.Equals(file.ToLower()) && !isNot.Contains((string)line.Children().ElementAt(1)))
                    {
                        found = true;
                        id = (string)line.GetValue("id");
                    }
                }
            }
            return id;
        }

        private static async Task<List<Actor>> GetActors(string id, HttpClient client)
        {
            List<Actor> ret = new List<Actor>();
            string baseUrl = "https://api.themoviedb.org/3/movie/";
            string url = baseUrl + id + "/credits" + key;
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                try
                {
                    JObject credits = await response.Content.ReadAsAsync<JObject>();
                    JToken actors = credits.GetValue("cast");
                    
                    JToken actor = actors.First;
                    while (actor != null)
                    {
                        Actor toAdd = new Actor();
                        toAdd.Character = (string)actor["character"];
                        toAdd.CreditId = (string)actor["credit_id"];
                        toAdd.Id = (string)actor["id"];
                        toAdd.Name = (string)actor["name"];
                        toAdd.ProfilePath = (string)actor["profile_path"];
                        toAdd.Photo = Path.GetFullPath(@"ActorImgs\") + toAdd.Id + ".jpg";
                        ret.Add(toAdd);
                        actor = actor.Next;
                    }
                }
                catch (Exception) {   }
            }
            return ret;
        }
    }
}
