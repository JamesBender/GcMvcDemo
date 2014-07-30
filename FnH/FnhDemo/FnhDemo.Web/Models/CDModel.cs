﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FnhDemo.Data;
using FnhDemo.Web.Models.ViewModels;



namespace FnhDemo.Web.Models
{
    public class CDModel
    {
        private Repository<Data.Entities.CD> _repository;

        public CDModel()
        {
            _repository = new Repository<Data.Entities.CD>();
        }

        public IEnumerable<CD> GetAllCD()
        {
            var listOfCdEntity = _repository.All;
            return listOfCdEntity.Select(Mapper.Map<Data.Entities.CD, CD>).ToList();
        }
    }
}