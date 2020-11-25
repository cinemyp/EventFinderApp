using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace WpfApp1
{
    public class EventViewModel : IPageViewModel,INotifyPropertyChanged
    {
        private Event currentEvent;
        public Event CurrentEvent
        {
            get { return currentEvent; }
            set
            {
                currentEvent = value;
                OnPropertyChanged("CurrentEvent");
            }
        }

        public EventViewModel(Event ev)
        {
            CurrentEvent = ev;
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
