using System.Collections.Generic;
using System.Linq;
using SqlDemo.Data.Repositories;
using SqlDemo.Web.Models.ViewModels;

namespace SqlDemo.Web.Models
{
    public class CDModel
    {
        private readonly TrackRepository _trackRepository;
        private readonly CDRepository _cdRepository;

        public CDModel()
        {
            _cdRepository = new CDRepository();
            _trackRepository = new TrackRepository(_cdRepository);
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

            foreach (var track in cdEntity.Tracks.Select(trackEntity => new Track
            {
                Id = trackEntity.Id,
                Artist = trackEntity.Artist,
                Length = trackEntity.Length,
                Name = trackEntity.Name,
                CD = cd
            }))
            {
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
            var cdEntity = MapCDViewModelToCDEntity(cd);

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

        private static Data.Entities.CD MapCDViewModelToCDEntity(CD cd)
        {
            return new Data.Entities.CD
            {
                Id = cd.Id,
                Artist = cd.Artist,
                Genre = cd.Genre,
                Title = cd.Title,
                Year = cd.Year
            };
        }

        public void Delete(int id)
        {
            _cdRepository.Delete(id);
        }

        public Track GetTrackDetails(int id)
        {
            var trackEntity = _trackRepository.GetTrackDetails(id);

            var trackModel = MapTrackEntitToTrackModel(trackEntity);

            return trackModel;
        }

        private static Track MapTrackEntitToTrackModel(Data.Entities.Track trackEntity)
        {
            var cdModel = new CD
            {
                Artist = trackEntity.CD.Artist,
                Genre = trackEntity.CD.Genre,
                Id = trackEntity.CD.Id,
                Title = trackEntity.CD.Title,
                Year = trackEntity.CD.Year
            };

            var trackModel = new Track
            {
                Artist = trackEntity.Artist,
                Id = trackEntity.Id,
                Length = trackEntity.Length,
                Name = trackEntity.Name,
                CD = cdModel
            };
            
            return trackModel;
        }

        public void AddTrackToCD(int id, Track track)
        {
            var trackEntity = new Data.Entities.Track {Artist = track.Artist, Length = track.Length, Name = track.Name};
            var cdEntity = _cdRepository.GetById(id);
            trackEntity.CD = cdEntity;
            cdEntity.Tracks.Add(trackEntity);
            _cdRepository.Save(cdEntity);
        }

        public void Save(Track track)
        {
            var cdEntity = _cdRepository.GetById(track.CD.Id);
            var trackEntity = new Data.Entities.Track
            {
                Artist = track.Artist,
                Id = track.Id,
                Length = track.Length,
                Name = track.Name,
                CD = cdEntity
            };
            _trackRepository.Save(trackEntity);
        }

        public void DeleteTrack(int id)
        {
            _trackRepository.Delete(id);
        }
    }
}