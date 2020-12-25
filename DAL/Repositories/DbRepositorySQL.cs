using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DbRepositorySQL : IDbRepository
    {
        private EventFinderContext db;

        private UserRepositorySQL userRepository;
        private SessionRepositorySQL sessionRepository;
        private EventRepositorySQL eventRepository;
        private PlaceRepositorySQL placeRepository;
        private CityRepositorySQL cityRepository;
        private EventsOrganizersRepositorySQL eventsOrganizersRepository;
        private CategoryRepositorySQL categoryRepository;
        private TypeRepositorySQL typeRepository;
        public DbRepositorySQL()
        {
            db = new EventFinderContext();
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepositorySQL(db);
                return userRepository;
            }
        }

        public IRepository<Session> Sessions
        {
            get
            {
                if (sessionRepository == null)
                    sessionRepository = new SessionRepositorySQL(db);
                return sessionRepository;
            }
        }

        public IRepository<Event> Events
        {
            get
            {
                if (eventRepository == null)
                    eventRepository = new EventRepositorySQL(db);
                return eventRepository;
            }
        }

        public IRepository<Place> Places
        {
            get
            {
                if (placeRepository == null)
                    placeRepository = new PlaceRepositorySQL(db);
                return placeRepository;
            }
        }
        public IRepository<City> Cities
        {
            get
            {
                if (cityRepository == null)
                    cityRepository = new CityRepositorySQL(db);
                return cityRepository;
            }
        }

        public IRepository<EventsOrganizers> EventsOrganizers
        {
            get
            {
                if (eventsOrganizersRepository == null)
                    eventsOrganizersRepository = new EventsOrganizersRepositorySQL(db);
                return eventsOrganizersRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepositorySQL(db);
                return categoryRepository;
            }
        }

        public IRepository<Type> Types
        {
            get
            {
                if (typeRepository == null)
                    typeRepository = new TypeRepositorySQL(db);
                return typeRepository;
            }
        }


        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
