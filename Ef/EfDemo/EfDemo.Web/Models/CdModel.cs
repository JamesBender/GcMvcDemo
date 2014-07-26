using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EfDemo.Data;
using CD = EfDemo.Web.Models.ViewModels.CD;

namespace EfDemo.Web.Models
{
    public class CdModel
    {
        private MediaManagerEntities _db;

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

        public CD GetCdDetails(int id)
        {
            var entity = _db.CDs.FirstOrDefault(x => x.Id == id);
            
            if (entity == null)
            {
                return new CD();
            }

            var model = new CD
            {
                Artist = entity.Artist,
                Genre = entity.Genre,
                Id = entity.Id,
                Title = entity.Title,
                Year = entity.Year
            };
            return model;
        }

        public int Create(CD cd)
        {
            var entity = new Data.CD {Artist = cd.Artist, Genre = cd.Genre, Title = cd.Title, Year = cd.Year, Id = -1};
            var x = _db.CDs.Add(entity);
            _db.SaveChanges();
            return x.Id;
        }

        public int Save(CD cd)
        {
            var entity = _db.CDs.FirstOrDefault(x => x.Id == cd.Id);
            entity.Artist = cd.Artist;
            entity.Genre = cd.Genre;
            entity.Title = cd.Title;
            entity.Year = cd.Year;
            var y = _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
            return entity.Id;
        }

        public bool Delete(CD cd)
        {
            var entity = _db.CDs.Find(cd.Id);
            _db.CDs.Remove(entity);
            _db.SaveChanges();
            return true;
        }
    }
}