using System.Collections.Generic;

namespace EfDemo.Web.Models.ViewModels
{
    public class TrackIndex
    {
        public int CdId { get; set; }
        public IEnumerable<Track> Tracks { get; set; }
    }
}