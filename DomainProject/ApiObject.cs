using System;
using System.Collections.Generic;
using System.Text;

namespace DomainProject
{
    public class ApiObject
    {
        //Movie properties
        public string title { get; set; }
        public string vote_average { get; set; }
        public string release_date { get; set; }
        public int runtime { get; set; }
        public string poster_path { get; set; }
        public string overview { get; set; }
        public string id { get; set; }
        public List<Actor> cast { get; set; }



        //Episode properties
        public string name { get; set; }
        public string still_path { get; set; }
        public string air_date { get; set; }
        public string first_air_date { get; set; }
        public string last_air_date { get; set; }
        public string episode_number { get; set; }
        public string season_number { get; set; }
    }
}
