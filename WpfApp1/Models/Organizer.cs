using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class OrganizerModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Site { get; set; }

        public int PlaceId { get; set; }
        public PlaceModel Place { get; set; }

        public OrganizerModel() { }
        public OrganizerModel(Organizer o)
        {
            ID = o.ID;
            Name = o.Name;
            Site = o.Site;
            PlaceId = o.PlaceId;
            Place = new PlaceModel(o.Place);
        }
    }
}
