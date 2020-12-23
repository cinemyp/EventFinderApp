using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Interfaces;
using WpfApp1.Models;

namespace WpfApp1
{
    public class TransactionManager : IDbCrud
    {
        IDbRepository db;

        public TransactionManager(IDbRepository repos)
        {
            db = repos;
        }

        public List<EventModel> GetEvents()
        {
            return db.Events.GetAll().Select(i => new EventModel(i)).ToList();
        }

        public List<SessionModel> GetSessions(int eventId)
        {
            
            return db.Sessions.GetAll().Join(db.EventsOrganizers.GetAll(), s => s.EventsOrganizersId, eo => eo.ID, (s, eo) => eo)
                .Join(db.Events.GetAll(), eo => eo.EventId, e => e.ID, (eo, e) => eo)
                .Where(i => i.EventId == eventId)
                .Select(i => i.Session)
                .FirstOrDefault().ToList()
                .Select(i => new SessionModel(i)).ToList();
        }

        public List<CategoryModel> GetCategories()
        {
            return db.Categories.GetAll().Select(i => new CategoryModel(i)).ToList();
        }
        public List<TypeModel> GetTypes()
        {
            return db.Types.GetAll().Select(i => new TypeModel(i)).ToList();
        }

        public List<EventModel> GetEvents(Date d)
        {
            DateValue value = d.Value;
            switch (value)
            {
                case DateValue.Soon:
                default:
                    return db.Events.GetAll().Select(i => new EventModel(i)).ToList();
                case DateValue.Today:
                    return db.Events.GetAll()
                        .Join(db.EventsOrganizers.GetAll(), e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Sessions.GetAll(), e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => i.Date == DateTime.Today)
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();
                case DateValue.Tomorrow:
                    return db.Events.GetAll()
                        .Join(db.EventsOrganizers.GetAll(), e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Sessions.GetAll(), e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => i.Date == DateTime.Today.AddDays(1))
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();
                case DateValue.Weekend:
                    return db.Events.GetAll()
                        .Join(db.EventsOrganizers.GetAll(), e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Sessions.GetAll(), e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => (i.Date.DayOfWeek == DayOfWeek.Saturday || i.Date.DayOfWeek == DayOfWeek.Sunday) && (i.Date < DateTime.Today.AddDays(7)))
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();
                case DateValue.Week:
                    return db.Events.GetAll()
                        .Join(db.EventsOrganizers.GetAll(), e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Sessions.GetAll(), e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => i.Date <= DateTime.Today.AddDays(7))
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();
                case DateValue.Month:
                    return db.Events.GetAll() //TODO: разобраться с фильтром, добавляем 30 дней или все события только в этом месяце
                        .Join(db.EventsOrganizers.GetAll(), e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Sessions.GetAll(), e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => i.Date <= DateTime.Today.AddDays(30))
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();

            }
        }

        public UserModel SignIn(UserModel user)
        {
            User u = db.Users.GetAll()
                .Where(i => i.Login == user.Login)
                .Where(i => i.Password == user.Password)
                .FirstOrDefault();
            return new UserModel(u);
        }

        public bool SignOn(UserModel user)
        {
            bool isExist;
            isExist = db.Users.GetAll()
                .Where(i => i.Login == user.Login)
                .Where(i => i.Password == user.Password)
                .FirstOrDefault() == null ? false : true;
            if (isExist)
                return false;

            db.Users.Create(new User { Login = user.Login, Password = user.Password });
            Save();
            return true;
        }

        public User GetUser(int id)
        {
            return db.Users.GetItem(id);
        }
        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }

        public List<EventModel> GetUserSessions(int userId)
        {
            return db.Users.GetItem(userId).Session.Select(i => new EventModel(i.EventsOrganizers.Event, i)).ToList();
            
        }
    }
}
