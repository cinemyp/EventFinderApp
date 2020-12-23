using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EventRepositorySQL : IRepository<Event>
    {
        private EventFinderContext db;
        public EventRepositorySQL(EventFinderContext dbContext)
        {
            db = dbContext;
        }
        public void Create(Event item)
        {
            db.Event.Add(item);
        }

        public void Delete(int id)
        {
            Event ev = db.Event.Find(id);
            if (ev != null)
                db.Event.Remove(ev);
        }

        public List<Event> GetAll()
        {
            return db.Event.ToList();
        }

        public Event GetItem(int id)
        {
            return db.Event.Find(id);
        }

        public void Update(Event item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
