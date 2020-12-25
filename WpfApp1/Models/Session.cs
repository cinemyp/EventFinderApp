using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class SessionModel
    {
        public int ID { get; set; }

        public int EventsOrganizersId { get; set; }

        public DateTime Date { get; set; }

        public OrganizerModel Organizer { get; set; }
        public bool IsDone { get; set; }
        public bool IsFavourite { get; set; }
        public SessionModel() { }
        public SessionModel(Session s)
        {
            ID = s.ID;
            EventsOrganizersId = s.EventsOrganizersId;
            Date = s.Date;
            Organizer = new OrganizerModel(s.EventsOrganizers.Organizer);
            IsDone = DateTime.Now > Date ? true : false;
            IsFavourite = false;
        }
    }
}
