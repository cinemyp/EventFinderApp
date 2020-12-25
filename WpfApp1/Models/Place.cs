using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class PlaceModel
    {
        public int ID { get; set; }

        public string Address { get; set; }

        public int CountryId { get; set; }

        public int CityId { get; set; }

        //public City City { get; set; }

        //public Country Country { get; set; }

        public PlaceModel() { }
        public PlaceModel(Place p)
        {
            ID = p.ID;
            Address = p.Address;
            CountryId = p.CountryId;
            CityId = p.CityId;
        }
    }
}
