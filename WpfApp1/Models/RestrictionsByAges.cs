using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class RestrictionsByAgesModel
    {
        public int ID { get; set; }

        public int Age { get; set; }

        public RestrictionsByAgesModel()
        {
        }
        public RestrictionsByAgesModel(RestrictionsByAges r)
        {
            ID = r.ID;
            Age = r.Age;
        }
        
    }
}
