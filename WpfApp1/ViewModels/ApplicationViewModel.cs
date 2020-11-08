using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        EventFinderContext db;
        public ObservableCollection<Event> Events { get; set; }
        private Event openedEvent;
        public Event OpenedEvent
        {
            get { return openedEvent; }
            set
            {
                OpenedEvent = value;
                OnPropertyChanged("OpenedEvent");
            }
        }
        public ApplicationViewModel()
        {
            db = new EventFinderContext();
            db.Event.Load();
            Events = new ObservableCollection<Event>(db.Event.Local.ToList());
        }

        private RelayCommand openEventCommand;
        public RelayCommand OpenEventCommand
        {
            get
            {
                return openEventCommand ??
                    (openEventCommand = new RelayCommand(obj =>
                    {
                        string title = obj.ToString();
                        if(title.Length > 0)
                        {
                            Event ev = db.Event.Local.ToList().Find(e => e.Title == title);
                            EventWindow eventWindow = new EventWindow();
                        }
                    }));
            }
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
