using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class EventModel
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Site { get; set; }

        public byte[] Poster { get; set; }

        public int TypeId { get; set; }

        public int CategoryId { get; set; }

        public int RestrictionId { get; set; }

        public EventModel() { }
        public EventModel(Event e)
        {
            ID = e.ID;
            Title = e.Title;
            Description = e.Description;
            Site = e.Site;
            Poster = e.Poster;
            TypeId = e.TypeId;
            CategoryId = e.CategoryId;
            RestrictionId = e.RestrictionId;
        }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<EventsOrganizers> EventsOrganizers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<User> User { get; set; }
    }
}
