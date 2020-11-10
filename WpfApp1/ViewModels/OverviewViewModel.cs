using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp1.ViewModels.Interfaces;

namespace WpfApp1
{
    public class OverviewViewModel : INotifyPropertyChanged, IPageViewModel
    {
        EventFinderContext db;
        IPageManager pm;
        public ObservableCollection<Event> Events { get; set; }
        private Event currentEvent;
        public Event CurrentEvent
        {
            get { return currentEvent; }
            set
            {
                currentEvent = value;
            }
        }

        private RelayCommand openEvent;
        public RelayCommand OpenEvent
        {
            get
            {
                return openEvent ??
                    (openEvent = new RelayCommand(obj =>
                    {
                        string title = obj.ToString();
                        Event ev = Events.ToList().Find(e => e.Title == title);
                        if (ev != null)
                        {
                            CurrentEvent = ev;
                            pm.ChangePage();
                        }
                    }
                ));
            }
        }
        public OverviewViewModel(IPageManager pm)
        {
            db = new EventFinderContext();
            Events = new ObservableCollection<Event>(db.Event.ToList());
            this.pm = pm;
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
