using System.Collections.Generic;

namespace SqlDemo.Data.Entities
{
    public class CD
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
    }
}