using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainProject
{
    public class Movie
    {
        public string Title { get; set; }
        public string Route { get; set; }
        public string Poster { get; set; }
        public int MinDuration { get; set; }
        public string About { get; set; }
        public string ReleaseDate { get; set; }
        public List<Actor> Actors { get; set; }

        public Movie(string title, int duration, string about, string release, string route, List<Actor> actors, string id)
        {
            Title = title;
            MinDuration = duration;
            About = about;
            ReleaseDate = release;
            Route = route;
            Poster = Path.GetFullPath(@"Posters\") + Title.Replace(":","").Replace(",","").Replace(".","").Replace("- ","").Replace("? ", "").Replace("/", "") + id +".jpg";
            Actors = actors;
        }
    }
}
