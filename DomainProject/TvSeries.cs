using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DomainProject
{
    public class TvSeries
    {
        public string Title { get; set; }
        public string Poster { get; set; }
        public string About { get; set; }
        public string FirstDate { get; set; }
        public string LastDate { get; set; }
        public List<Season> Seasons { get; set; }

        public TvSeries(string title, string about, string firstDate, string lastDate, List<Season> seasons, string id)
        {
            Title = title;
            About = about;
            FirstDate = firstDate;
            LastDate = lastDate;
            Poster = Path.GetFullPath(@"Posters\") + Title.Replace(":", "").Replace(",", "").Replace(".", "").Replace("- ", "").Replace("? ", "").Replace("/", "") + id + ".jpg";
            Seasons = seasons;
        }
    }
}
