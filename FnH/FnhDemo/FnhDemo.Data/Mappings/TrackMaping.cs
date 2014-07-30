using FluentNHibernate.Mapping;
using FnhDemo.Data.Entities;

namespace FnhDemo.Data.Mappings
{
    public class TrackMaping : ClassMap<Track>
    {
        public TrackMaping()
        {
            Id(x => x.Id);
            Map(x => x.Artist);
            Map(x => x.Length);
            Map(x => x.Name);
            References(x => x.CD).Column("Id");
        }
    }
}