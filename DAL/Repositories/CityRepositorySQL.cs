using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CityRepositorySQL : IRepository<City>
    {
        private EventFinderContext db;
        public CityRepositorySQL(EventFinderContext dbContext)
        {
            db = dbContext;
        }
        public void Create(City item)
        {
            db.City.Add(item);
        }

        public void Delete(int id)
        {
            City city = db.City.Find(id);
            if (city != null)
                db.City.Remove(city);
        }

        public List<City> GetAll()
        {
            return db.City.ToList();
        }

        public City GetItem(int id)
        {
            return db.City.Find(id);
        }

        public void Update(City item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
