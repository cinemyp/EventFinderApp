using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1.Models;

namespace WpfApp1
{
    public class EventViewModel : IPageViewModel,INotifyPropertyChanged
    {
        private EventModel currentEvent;
        public EventModel CurrentEvent
        {
            get { return currentEvent; }
            set
            {
                currentEvent = value;
                OnPropertyChanged("CurrentEvent");
            }
        }
        
        public EventViewModel(EventModel ev)
        {
            CurrentEvent = ev;
        }

        PageType IPageViewModel.GetType()
        {
            return PageType.Event;
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
