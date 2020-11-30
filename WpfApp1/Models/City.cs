using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class CityModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public CityModel()
        {
        }

        public CityModel(City c)
        {
            ID = c.Id;
            Name = c.Name;
        }
    }
}
