using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class CategoryModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public CategoryModel()
        {
        }

        public CategoryModel(Category c)
        {
            ID = c.ID;
            Name = c.Name;
        }
    }
}
