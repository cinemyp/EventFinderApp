using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TransactionManager
    {
        EventFinderContext db;

        public TransactionManager()
        {
            db = new EventFinderContext();
        }

        public List<Event> GetEvents()
        {
            return db.Event.ToList();
        }

        public List<Session> GetSessions(int eventId)
        {
            return db.Session.Join(db.EventsOrganizers, s => s.EventsOrganizersId, eo => eo.ID, (s, eo) => eo)
                .Join(db.Event, eo => eo.EventId, e => e.ID, (eo, e) => eo)
                .Where(i => i.EventId == eventId)
                .Select(i => i.Session).FirstOrDefault().ToList();
        }


    }
}
