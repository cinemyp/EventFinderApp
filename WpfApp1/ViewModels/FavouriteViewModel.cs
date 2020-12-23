using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.ViewModels.Interfaces;

namespace WpfApp1
{
    public class FavouriteViewModel : INotifyPropertyChanged, IPageViewModel
    {
        IDbCrud tm;
        IPageManager pm;

        private ObservableCollection<EventModel> events;
        public ObservableCollection<EventModel> Events
        {
            get { return events; }
            set
            {
                events = value;
                OnPropertyChanged("Events");
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
                        EventModel ev = Events.ToList().Find(e => e.Title == title);
                        if (ev != null)
                            pm.OpenEvent(ev);
                    }
                ));
            }
        }
        public FavouriteViewModel(IDbCrud tm, IPageManager pm)
        {
            this.tm = tm;
            this.pm = pm;
        }

        PageType IPageViewModel.GetType()
        {
            return PageType.Favourite;
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
