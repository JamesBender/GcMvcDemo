using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FnhDemo.Data;
using FnhDemo.Web.Models.ViewModels;

namespace FnhDemo.Web.Models
{
    public class DvdModel
    {
        private readonly Repository<Data.Entities.DVD> _repository;

        public DvdModel()
        {
            _repository = new Repository<Data.Entities.DVD>();
        }

        public IEnumerable<DVD> GetAllDvd()
        {
            var listOfDvdEntities = _repository.All;
            return listOfDvdEntities.Select(Mapper.Map<Data.Entities.DVD, DVD>).ToList();
        }
    }
}