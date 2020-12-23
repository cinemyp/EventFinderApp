using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EventsOrganizersRepositorySQL : IRepository<EventsOrganizers>
    {
        private EventFinderContext db;
        public EventsOrganizersRepositorySQL(EventFinderContext dbContext)
        {
            db = dbContext;
        }
        public void Create(EventsOrganizers item)
        {
            db.EventsOrganizers.Add(item);
        }

        public void Delete(int id)
        {
            EventsOrganizers eventsOrganizers = db.EventsOrganizers.Find(id);
            if (eventsOrganizers != null)
                db.EventsOrganizers.Remove(eventsOrganizers);
        }

        public List<EventsOrganizers> GetAll()
        {
            return db.EventsOrganizers.ToList();
        }

        public EventsOrganizers GetItem(int id)
        {
            return db.EventsOrganizers.Find(id);
        }

        public void Update(EventsOrganizers item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
