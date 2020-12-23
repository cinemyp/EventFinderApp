using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepositorySQL : IRepository<User>
    {
        private EventFinderContext db;
        public UserRepositorySQL(EventFinderContext dbContext)
        {
            db = dbContext;
        }
        public void Create(User item)
        {
            db.User.Add(item);
        }

        public void Delete(int id)
        {
            User user = db.User.Find(id);
            if (user != null)
                db.User.Remove(user);
        }

        public List<User> GetAll()
        {
            return db.User.ToList();
        }

        public User GetItem(int id)
        {
            return db.User.Find(id);
        }

        public void Update(User item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
