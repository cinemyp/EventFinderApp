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

        public SessionModel() { }
        public SessionModel(Session s)
        {
            ID = s.ID;
            EventsOrganizersId = s.EventsOrganizersId;
            Date = s.Date;
        }
    }
}
