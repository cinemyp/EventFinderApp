using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using WpfApp1.Models;

namespace WpfApp1
{
    public class TransactionManager
    {
        EventFinderContext db;

        public TransactionManager()
        {
            db = new EventFinderContext();
        }

        public List<EventModel> GetEvents()
        {
            return db.Event.ToList().Select(i => new EventModel(i)).ToList();
        }

        public List<SessionModel> GetSessions(int eventId)
        {
            return db.Session.Join(db.EventsOrganizers, s => s.EventsOrganizersId, eo => eo.ID, (s, eo) => eo)
                .Join(db.Event, eo => eo.EventId, e => e.ID, (eo, e) => eo)
                .Where(i => i.EventId == eventId)
                .Select(i => i.Session)
                .FirstOrDefault().ToList()
                .Select(i => new SessionModel(i)).ToList();
        }

        public List<CategoryModel> GetCategories()
        {
            return db.Category.ToList().Select(i => new CategoryModel(i)).ToList();
        }

    }
}
