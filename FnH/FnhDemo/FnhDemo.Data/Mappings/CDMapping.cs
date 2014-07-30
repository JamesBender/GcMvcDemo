using FluentNHibernate.Mapping;
using FnhDemo.Data.Entities;


namespace FnhDemo.Data.Mappings
{
    public class CDMapping : ClassMap<CD>
    {
        public CDMapping()
        {
            /*public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }

        public virtual IList<Track> Tracks { get; set; } */
            
            Id(x => x.Id);
            Map(x => x.Artist);
            Map(x => x.Title);
            Map(x => x.Genre);
            Map(x => x.Year);
            HasMany(x => x.Tracks).KeyColumn("CDId").Not.LazyLoad().Cascade.All().Inverse();

        } 
    }
}