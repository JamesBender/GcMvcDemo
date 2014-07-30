using System.Collections.Generic;

namespace FnhDemo.Data.Entities
{
    public class CD : BaseEntity
    {
        public virtual string Artist { get; set; }
        public virtual string Title { get; set; }
        public virtual string Genre { get; set; }
        public virtual int Year { get; set; }

        public virtual IList<Track> Tracks { get; set; } 
    }
}