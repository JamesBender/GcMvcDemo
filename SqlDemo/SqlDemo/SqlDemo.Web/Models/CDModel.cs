using System.Collections.Generic;
using System.Linq;
using SqlDemo.Data.Repositories;
using SqlDemo.Web.Models.ViewModels;

namespace SqlDemo.Web.Models
{
    public class CDModel
    {
        private readonly CDRepository _cdRepository;

        public CDModel()
        {
            _cdRepository = new CDRepository();
        }

        public IEnumerable<CD> GetListOfAllCD()
        {
            var listOfCdEntities = _cdRepository.All;

            return listOfCdEntities.Select(MapCdEntityToCdViewModel).ToList();
        }

        private static CD MapCdEntityToCdViewModel(Data.Entities.CD cdEntity)
        {
            var cd = new CD
            {
                Id = cdEntity.Id,
                Artist = cdEntity.Artist,
                Genre = cdEntity.Genre,
                Title = cdEntity.Title,
                Year = cdEntity.Year,
                Tracks = new List<Track>()
            };

            foreach (var trackEntity in cdEntity.Tracks)
            {
                var track = new Track
                {
                    Id = trackEntity.Id,
                    Artist = trackEntity.Artist,
                    Length = trackEntity.Length,
                    Name = trackEntity.Name,
                    CD = cd
                };

                cd.Tracks.Add(track);
            }
            return cd;
        }

        public CD GetCdDetails(int id)
        {
            var cdEntity = _cdRepository.GetById(id);
            return MapCdEntityToCdViewModel(cdEntity);
        }

        public int Save(CD cd)
        {
            var cdEntity = new Data.Entities.CD
            {
                Id = cd.Id,
                Artist = cd.Artist,
                Genre = cd.Genre,
                Title = cd.Title,
                Year = cd.Year
            };

            if (cd.Tracks != null)
            {
                cdEntity.Tracks = new List<Data.Entities.Track>();

                foreach (var trackModel in cd.Tracks)
                {
                    var trackEntity = new Data.Entities.Track
                    {
                        Id = trackModel.Id,
                        Artist = trackModel.Artist,
                        CD = cdEntity,
                        Length = trackModel.Length,
                        Name = trackModel.Name
                    };

                    cdEntity.Tracks.Add(trackEntity);
                }
            }

            return _cdRepository.Save(cdEntity);
        }

        public void Delete(int id)
        {
            _cdRepository.Delete(id);
        }
    }
}