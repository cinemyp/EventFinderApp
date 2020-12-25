using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class NotificationViewModel 
    {
        public List<EventModel> TodaysEvents { get; set; }

        public NotificationViewModel(List<EventModel> ev)
        {
            TodaysEvents = ev;
        }

    }
}
