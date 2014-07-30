using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FnhDemo.Data;
using FnhDemo.Web.Models.ViewModels;



namespace FnhDemo.Web.Models
{
    public class CDModel
    {
        private readonly Repository<Data.Entities.CD> _cdRepository;
        private Repository<Data.Entities.Track> _trackRepository; 

        public CDModel()
        {
            _cdRepository = new Repository<Data.Entities.CD>();
        }

        public IEnumerable<CD> GetAllCD()
        {
            var listOfCdEntity = _cdRepository.All;
            return listOfCdEntity.Select(Mapper.Map<Data.Entities.CD, CD>).ToList();
        }

        public CD GetCd(int id)
        {
            var cdEntity = _cdRepository.GetById(id);
            return Mapper.Map<Data.Entities.CD, CD>(cdEntity);
        }

        public void Save(CD cd)
        {
            var cdEntity = Mapper.Map<CD, Data.Entities.CD>(cd);
            _cdRepository.Save(cdEntity);
        }

        public void Delete(int id)
        {
            _cdRepository.Delete(id);
        }

        public Track GetTrackDetails(int id)
        {
            if (_trackRepository == null)
            {
                _trackRepository = new Repository<Data.Entities.Track>();
            }

            var track = _trackRepository.GetById(id);
            return Mapper.Map<Data.Entities.Track, Track>(track);
        }

        public void AddTrackToCd(int id, Track track)
        {
            var cdEntity = _cdRepository.GetById(id);
            var trackEntity = new Data.Entities.Track
            {
                Artist = track.Artist,
                CD = cdEntity,
                Length = track.Length,
                Name = track.Name
            };

            if (cdEntity.Tracks == null)
            {
                cdEntity.Tracks = new List<Data.Entities.Track>();
            }

            cdEntity.Tracks.Add(trackEntity);
            _cdRepository.Save(cdEntity);
        }
    }
}