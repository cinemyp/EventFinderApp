using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepositorySQL : IRepository<Category>
    {
        private EventFinderContext db;
        public CategoryRepositorySQL(EventFinderContext dbContext)
        {
            db = dbContext;
        }
        public void Create(Category item)
        {
            db.Category.Add(item);
        }

        public void Delete(int id)
        {
            Category category = db.Category.Find(id);
            if (category != null)
                db.Category.Remove(category);
        }

        public List<Category> GetAll()
        {
            return db.Category.ToList();
        }

        public Category GetItem(int id)
        {
            return db.Category.Find(id);
        }

        public void Update(Category item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
