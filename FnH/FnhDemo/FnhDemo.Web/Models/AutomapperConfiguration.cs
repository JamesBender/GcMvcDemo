using AutoMapper;

namespace FnhDemo.Web.Models
{
    public static class AutomapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Data.Entities.DVD, ViewModels.DVD>();
            Mapper.CreateMap<ViewModels.DVD, Data.Entities.DVD>();
            Mapper.CreateMap<Data.Entities.CD, ViewModels.CD>();
            Mapper.CreateMap<ViewModels.CD, Data.Entities.CD>();
            Mapper.CreateMap<Data.Entities.Track, ViewModels.Track>();
            Mapper.CreateMap<ViewModels.Track, Data.Entities.Track>();
        }
    }
}