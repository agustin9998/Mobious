using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainProject
{
    public class Season
    {
        public string Title { get; set; }
        public string Poster { get; set; }
        public string About { get; set; }
        public string Date { get; set; }
        public string SeasonNumber { get; set; }
        public List<Episode> Episodes { get; set; }

        public Season(string title, string about, string number, string date, List<Episode> episodes, string id)
        {
            Title = title;
            About = about;
            SeasonNumber = number;
            Date = date;
            Poster = Path.GetFullPath(@"Posters\") + Title.Replace(":", "").Replace(",", "").Replace(".", "").Replace("- ", "").Replace("? ", "").Replace("/", "") + id + ".jpg";
            Episodes = episodes;
        }
    }
}
