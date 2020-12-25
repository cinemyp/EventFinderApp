using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1.Models;
using WpfApp1.ViewModels.Interfaces;
using System.Windows.Media;

namespace WpfApp1
{
    public class EventViewModel : IPageViewModel,INotifyPropertyChanged
    {
        IDbCrud tm;
        IMainViewModel mvm;
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
        private int loggedUserId;
        public bool IsLogged { get; set; }


        private RelayCommand clickFavourite;
        public RelayCommand ClickFavourite
        {
            get
            {
                return clickFavourite ??
                    (clickFavourite = new RelayCommand(obj =>
                    {
                        
                        int sessionId = (int)obj;
                        if (IsLogged == false)
                            return;

                        bool result = tm.MakeFavourite(loggedUserId, sessionId);
                        CheckFavoriteSessions();
                    }
                ));
            }
        }

        public EventViewModel(EventModel ev, IDbCrud tm, IMainViewModel mvm)
        {
            CurrentEvent = ev;
            this.tm = tm;
            this.mvm = mvm;
            loggedUserId = mvm.GetLoggedUserId();
            IsLogged = loggedUserId == -1 ? false : true;
            if (IsLogged)
                CheckFavoriteSessions();
        }

        private void CheckFavoriteSessions()
        {
            EventModel ev = CurrentEvent;
            foreach(SessionModel s in ev.Sessions)
            {
                if (tm.GetUser(loggedUserId).Session.Select(i => i.ID == s.ID).Count() > 0)
                    s.IsFavorite = true;
                else
                    s.IsFavorite = false;
            }
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
