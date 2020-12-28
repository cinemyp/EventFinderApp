
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ReportRepositorySQL : IReportRepository
    {
        private EventFinderContext db;
        public ReportRepositorySQL(EventFinderContext repos)
        {
            db = repos;
        }
        public ReportData MonthlyReport(int userId, int month)
        {
            List<DAL.Session> sessions = db.User.Find(userId).Session.Where(i => i.Date < DateTime.Now && i.Date.Month == month).ToList();
            int count = sessions.Count;
            if (count == 0)
                return null;
            int categoryId = sessions
                .Select(i => i.EventsOrganizers.Event.Category)
                .GroupBy(i => i.ID)
                .OrderByDescending(i => i.Count())
                .First()
                .Key;
            string category = db.Category.Find(categoryId).Name;
            int typeId = sessions
                .Select(i => i.EventsOrganizers.Event.Type)
                .GroupBy(i => i.ID)
                .OrderByDescending(i => i.Count())
                .First()
                .Key;
            string type = db.Type.Find(typeId).Name;

            List<Session> events = db.User.Find(userId).Session
                .Where(i => i.Date < DateTime.Now && i.Date.Month == month)
                .ToList();

            ReportData result = new ReportData()
            {
                CountFavouriteEvents = count,
                FavouriteCategory = category,
                FavouriteType = type,
                FavouriteEvents = events
            };
            return result;
        }
    }
}
