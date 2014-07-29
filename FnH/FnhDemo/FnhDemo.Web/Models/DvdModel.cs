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

        public DVD GetDvd(int id)
        {
            var dvd = _repository.GetById(id);
            return Mapper.Map<Data.Entities.DVD, DVD>(dvd);
        }

        public void Save(DVD dvd)
        {
            var dvdEntity = Mapper.Map<DVD, Data.Entities.DVD>(dvd);
            _repository.Save(dvdEntity);
        }
    }
}