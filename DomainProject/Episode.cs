using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainProject
{
    public class Episode
    {
        public string Title { get; set; }
        public string Route { get; set; }
        public string Poster { get; set; }
        public string Number { get; set; }
        public string About { get; set; }

        public Episode(string title, string about, string number, string route, string id)
        {
            Title = title;
            About = about;
            Number = number;
            Poster = Path.GetFullPath(@"Posters\") + Title.Replace(":", "").Replace(",", "").Replace(".", "").Replace("- ", "").Replace("? ", "") + id + ".jpg";
            Route = route;
        }
    }
}
