using System.Collections.Generic;

namespace FnhDemo.Web.Models.ViewModels
{
    public class CD
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        public IList<Track> Tracks { get; set; }  
    }
}