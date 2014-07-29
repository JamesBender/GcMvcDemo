using AutoMapper;

namespace FnhDemo.Web.Models
{
    public static class AutomapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Data.Entities.DVD, ViewModels.DVD>();
            Mapper.CreateMap<ViewModels.DVD, Data.Entities.DVD>();
        }
    }
}