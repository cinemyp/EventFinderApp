using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDbRepository
    {
        IRepository<User> Users { get; }
        IRepository<Session> Sessions { get; }
        IRepository<Event> Events { get; }
        IRepository<Place> Places { get; }
        IRepository<City> Cities { get; }
        IRepository<EventsOrganizers> EventsOrganizers { get; }
        IRepository<Category> Categories { get; }
        IRepository<Type> Types { get; }
        IReportRepository Reports { get; }
        int Save();
    }
}
