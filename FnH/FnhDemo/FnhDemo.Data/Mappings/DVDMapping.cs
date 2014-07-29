using FluentNHibernate.Mapping;
using FnhDemo.Data.Entities;

namespace FnhDemo.Data.Mappings
{
    public class DVDMapping : ClassMap<DVD>
    {
        public DVDMapping()
        {
            Id(x => x.Id);
            Map(x => x.RunningTime);
            Map(x => x.IsSpecialEdition);
            Map(x => x.Synopsis);
            Map(x => x.Title);
            Map(x => x.Genre);
            Map(x => x.Year);
        }
    }
}