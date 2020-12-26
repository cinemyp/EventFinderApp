using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ReportData
    {
        public int CountFavouriteEvents { get; set; } 
        public Category FavouriteCategory { get; set; }
        public Type FavouriteType { get; set; }
        public List<Event> FavouriteEvents { get; set; }
    }
}
