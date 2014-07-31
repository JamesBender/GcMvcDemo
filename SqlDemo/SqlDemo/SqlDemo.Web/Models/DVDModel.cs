using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;
using SqlDemo.Data.Repositories;
using SqlDemo.Web.Models.ViewModels;

namespace SqlDemo.Web.Models
{
    public class DVDModel
    {
        private DVDRepository _dvdRepository;

        public DVDModel()
        {
            _dvdRepository = new DVDRepository();
        }

        public IEnumerable<DVD> GetListOfAllDVD()
        {
            var listOfDvdEntities = _dvdRepository.All;

            // This is an example of how LINQ can make list
            // manipulation a lot easier. In the first step
            // I used a foreach loop to loop through the list
            // of dvd entities and convert each on from the
            // dvd entity type to the dvd model type by hand.

            // Step 1:
//            foreach (var dvdEntity in listOfDvdEntities)
//            {
//                var dvdModel = MapDvdEntityToDvdModel(dvdEntity);
//                listOfDvdModel.Add(dvdModel);
//            }

            // In Step two I was able to replace the foreach
            // loop with a select statement off of the 
            // list of entities (the list I want to "pull"
            // the data from) and feed the result of that
            // select into the MapDvdEntityToDvdModel that
            // does the converstion. The result of that is 
            // sent to a list via the ToList function at the
            // end of the statement
            
            //Step 2:
//            return listOfDvdEntities.Select(dvdEntity => MapDvdEntityToDvdModel(dvdEntity)).ToList();
            
            // In the final step I was able to use some 
            // "syntactic sugar" to just supply the name 
            // of the method that I want the outpu of the 
            // select to be sent to.

            //Step 3:
            return listOfDvdEntities.Select(MapDvdEntityToDvdModel).ToList();
        }

        private static DVD MapDvdEntityToDvdModel(Data.Entities.DVD dvdEntity)
        {
            var dvdModel = new DVD
            {
                Genre = dvdEntity.Genre,
                Id = dvdEntity.Id,
                IsSpecialEdition = dvdEntity.IsSpecialEdition,
                RunningTime = dvdEntity.RunningTime,
                Synopsis = dvdEntity.Synopsis,
                Title = dvdEntity.Title,
                Year = dvdEntity.Year
            };
            return dvdModel;
        }

        public DVD GetDvdDetails(int id)
        {
            var dvdEntity = _dvdRepository.GetById(id);
            return MapDvdEntityToDvdModel(dvdEntity);
        }

        public void Save(DVD dvd)
        {
            var dvdEntity = new Data.Entities.DVD
            {
                Genre = dvd.Genre,
                Id = dvd.Id,
                IsSpecialEdition = dvd.IsSpecialEdition,
                RunningTime = dvd.RunningTime,
                Synopsis = dvd.Synopsis,
                Title = dvd.Title,
                Year = dvd.Year
            };

            _dvdRepository.Save(dvdEntity);
        }

        public void Delete(DVD dvd)
        {
            _dvdRepository.Delete(dvd.Id);
        }
    }
}