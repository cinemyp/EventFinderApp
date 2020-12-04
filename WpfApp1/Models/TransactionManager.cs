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
        public List<TypeModel> GetTypes()
        {
            return db.Type.ToList().Select(i => new TypeModel(i)).ToList();
        }

        public List<EventModel> GetEvents(Date d)
        {
            DateValue value = d.Value;
            switch (value)
            {
                case DateValue.Soon:
                default:
                    return db.Event.ToList().Select(i => new EventModel(i)).ToList();
                case DateValue.Today:
                    return db.Event.ToList()
                        .Join(db.EventsOrganizers, e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Session, e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => i.Date == DateTime.Today)
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();
                case DateValue.Tomorrow:
                    return db.Event.ToList()
                        .Join(db.EventsOrganizers, e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Session, e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => i.Date == DateTime.Today.AddDays(1))
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();
                case DateValue.Weekend:
                    return db.Event.ToList()
                        .Join(db.EventsOrganizers, e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Session, e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => (i.Date.DayOfWeek == DayOfWeek.Saturday || i.Date.DayOfWeek == DayOfWeek.Sunday) && (i.Date < DateTime.Today.AddDays(7)))
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();
                case DateValue.Week:
                    return db.Event.ToList()
                        .Join(db.EventsOrganizers, e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Session, e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => i.Date <= DateTime.Today.AddDays(7))
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();
                case DateValue.Month:
                    return db.Event.ToList() //TODO: разобраться с фильтром, добавляем 30 дней или все события только в этом месяце
                        .Join(db.EventsOrganizers, e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Session, e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => i.Date <= DateTime.Today.AddDays(30))
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();

            }
        }


    }
}
