﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using EfDemo.Data;
using CD = EfDemo.Web.Models.ViewModels.CD;
using Track = EfDemo.Web.Models.ViewModels.Track;

namespace EfDemo.Web.Models
{
    public class CdModel
    {
        private readonly MediaManagerEntities _db;

        public CdModel()
        {
            _db = new MediaManagerEntities();
        }

        public IEnumerable<CD> GetListOfAllCd()
        {
            //Normally I would use a open source framework for this called 
            //Automapper (which you can get through NuGet). I'm doing the
            //long-handed way here so you can see what I'm doing. I'll use
            //Automapper in the other examples
            var listOfEntityCd = _db.CDs.ToList();
            var listOfModelCd = new List<CD>();

            foreach (var entity in listOfEntityCd)
            {
                var model = new CD
                {
                    Artist = entity.Artist,
                    Genre = entity.Genre,
                    Id = entity.Id,
                    Title = entity.Title,
                    Year = entity.Year
                };
                listOfModelCd.Add(model);
            }

            return listOfModelCd;
        }

        public CD GetCdDetails(int? id)
        {
            var entity = GetCdEntityFromDataContext(id);

            var model = new CD
            {
                Artist = entity.Artist,
                Genre = entity.Genre,
                Id = entity.Id,
                Title = entity.Title,
                Year = entity.Year
            };
            if (model.Tracks == null)
            {
                model.Tracks = new Collection<Track>();
            }
            foreach (var track in entity.Tracks)
            {
                model.Tracks.Add(new Track
                {
                    Artist = track.Artist,
                    CD = model,
                    CDId = model.Id,
                    Id = track.Id,
                    Length = track.Length,
                    Name = track.Name
                });
            }
            return model;
        }

        private Data.CD GetCdEntityFromDataContext(int? id)
        {
            var entity = _db.CDs.FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                throw new ObjectNotFoundException("The requested CD is not in the databse");
            }
            return entity;
        }

        public int Create(CD cd)
        {
            var entity = new Data.CD {Artist = cd.Artist, Genre = cd.Genre, Title = cd.Title, Year = cd.Year, Id = -1};
            var x = _db.CDs.Add(entity);
            _db.SaveChanges();
            return x.Id;
        }

        public void Save(CD cd)
        {
            var entity = _db.CDs.FirstOrDefault(x => x.Id == cd.Id);

            if (entity == null)
            {
                throw new ObjectNotFoundException("The requested CD is not in the databse");
            }

            entity.Artist = cd.Artist;
            entity.Genre = cd.Genre;
            entity.Title = cd.Title;
            entity.Year = cd.Year;
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();            
        }

        public void Delete(CD cd)
        {
            var entity = _db.CDs.Find(cd.Id);
            _db.CDs.Remove(entity);
            _db.SaveChanges();            
        }

        public int AddTrackToCd(int? cdId, Track track)
        {
            var cdEntity = GetCdEntityFromDataContext(cdId);
            var trackEntity = new Data.Track
            {
                Artist = track.Artist,
                CD = cdEntity,
                CDId = cdId,
                Id = -1,
                Length = track.Length,
                Name = track.Name
            };

            if (cdEntity.Tracks == null)
            {
                cdEntity.Tracks = new Collection<Data.Track>();
            }

            cdEntity.Tracks.Add(trackEntity);
            _db.Entry(cdEntity).State = EntityState.Modified;
            _db.SaveChanges();
            return trackEntity.Id;
        }

        public Track GetTrackDetails(int id)
        {
            var trackEntity = _db.Tracks.FirstOrDefault(track => track.Id == id);
            if (trackEntity == null)
            {
                throw new ObjectNotFoundException("The requested Track is not in the databse");
            }

            var trackModel = new Track
            {
                Artist = trackEntity.Artist,
                CDId = trackEntity.CDId,
                Id = trackEntity.Id,
                Length = trackEntity.Length,
                Name = trackEntity.Name
            };

            return trackModel;
        }

        public void Save(Track track)
        {
            var entity = _db.Tracks.FirstOrDefault(x => x.Id == track.Id);

            if (entity == null)
            {
                throw new ObjectNotFoundException("The requested track is not in the databse");
            }

            var cdEntity = GetCdEntityFromDataContext(entity.CDId);

            entity.Artist = track.Artist;
            entity.CD = cdEntity;
            entity.CDId = cdEntity.Id;
            entity.Length = track.Length;
            entity.Name = track.Name;

            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Track track)
        {
            var entity = _db.Tracks.Find(track.Id);
            _db.Tracks.Remove(entity);
            _db.SaveChanges();            
        }
    }
}