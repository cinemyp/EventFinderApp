using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using WpfApp1.Models;

namespace WpfApp1
{
    public class TransactionManager : IDbCrud
    {
        IDbRepository db;
        IReportRepository reports;

        public TransactionManager(IDbRepository repos, IReportRepository reports)
        {
            db = repos;
            this.reports = reports;
        }

        public List<EventModel> GetEvents(int cityId)
        {
            return db.Events.GetAll().Select(i => new EventModel(i)).Where(i => i.Sessions.Where(s => s.Organizer.Place.CityId == cityId).Count() > 0)
                .Where(i => i.Sessions.Where(s => s.IsDone == false).Count() > 0).ToList();
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

        public List<EventModel> GetEvents(int cityId, Date d)
        {
            DateValue value = d.Value;
            switch (value)
            {
                case DateValue.Soon:
                default:
                    return GetEvents(cityId);
                case DateValue.Today:
                    return GetEvents(cityId)
                        .Join(db.EventsOrganizers.GetAll(), e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Sessions.GetAll(), e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => i.Date >= DateTime.Today && i.Date < DateTime.Today.AddDays(1))
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();
                case DateValue.Tomorrow:
                    return GetEvents(cityId)
                        .Join(db.EventsOrganizers.GetAll(), e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Sessions.GetAll(), e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => i.Date > DateTime.Today.AddDays(1) && i.Date < DateTime.Today.AddDays(2))
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();
                case DateValue.Weekend:
                    return GetEvents(cityId)
                        .Join(db.EventsOrganizers.GetAll(), e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Sessions.GetAll(), e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => (i.Date.DayOfWeek == DayOfWeek.Saturday || i.Date.DayOfWeek == DayOfWeek.Sunday) && (i.Date < DateTime.Today.AddDays(7)))
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();
                case DateValue.Week:
                    return GetEvents(cityId)
                        .Join(db.EventsOrganizers.GetAll(), e => e.ID, eo => eo.EventId, (e, eo) => eo)
                        .Join(db.Sessions.GetAll(), e => e.ID, s => s.EventsOrganizersId, (eo, s) => s)
                        .Where(i => i.Date <= DateTime.Today.AddDays(7))
                        .Select(i => new EventModel(i.EventsOrganizers.Event))
                        .Distinct()
                        .ToList();
                case DateValue.Month:
                    return GetEvents(cityId) //TODO: разобраться с фильтром, добавляем 30 дней или все события только в этом месяце
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
            if (string.IsNullOrWhiteSpace(user.Login) || string.IsNullOrWhiteSpace(user.Password))
                return false;

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

        public UserModel GetUser(int id)
        {
            return new UserModel( db.Users.GetItem(id));
        }
        public bool Save()
        {
            if (db.Save() > 0) return true;
            return false;
        }

        public List<EventModel> GetUserSessions(int userId)
        {
            return db.Users.GetItem(userId).Session.Select(i => new EventModel(i.EventsOrganizers.Event, new SessionModel(i))).ToList();
        }
        //Лучше конечно разбить на три функции, но мне впадлу
        /// <summary>
        /// Возвращает true, если добавили в избранное; false, если убрали из избранного
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool MakeFavourite(int userId, int sessionId)
        {
            User user = db.Users.GetItem(userId);
            Session session = user.Session.FirstOrDefault(i => i.ID == sessionId);
            bool result;
            if (session != null)
            {
                user.Session.Remove(session);
                result = false;
            }
            else
            {
                user.Session.Add(db.Sessions.GetItem(sessionId));
                result = true;
            }

            Save();
            return result;
        }

        public List<EventModel> GetTodaysEvent(int userId)
        {
            return db.Users.GetItem(userId).Session.Select(i => new EventModel(i.EventsOrganizers.Event, new SessionModel(i))).Where(i => i.CurrentSession.Date.DayOfYear == DateTime.Now.DayOfYear).ToList();
        }

        public List<CityModel> GetCities()
        {
            return db.Cities.GetAll().Select(i => new CityModel(i)).ToList();
        }

        public ReportData MonthlyReport(int userId, int month)
        {
            var rep = reports.MonthlyReport(userId, month);
            if (rep == null)
                return null;
            return new ReportData()
            {
                CountFavouriteEvents = rep.CountFavouriteEvents,
                FavouriteCategory = rep.FavouriteCategory,
                FavouriteType = rep.FavouriteType,
                FavouriteEvents = rep.FavouriteEvents.Select(i => new EventModel(i.EventsOrganizers.Event, new SessionModel(i))).ToList()
            };
        }
    }
}
