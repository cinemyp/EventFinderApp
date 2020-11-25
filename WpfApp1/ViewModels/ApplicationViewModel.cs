using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Interfaces;

namespace WpfApp1
{
    public class ApplicationViewModel : INotifyPropertyChanged, IPageManager
    {
        
        private IPageViewModel _currentPageViewModel;
        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }


        private RelayCommand signIn;
        public RelayCommand SignIn
        {
            get
            {
                return signIn ??
                    (signIn = new RelayCommand(obj =>
                    {
                        Login login = new Login();
                        login.Show();
                        //TODO: брать отсюда данные и проверять на вход
                    }
                ));
            }
        }

        private RelayCommand filterEvents;
        public RelayCommand FilterEvents
        {
            get
            {
                return filterEvents ??
                    (filterEvents = new RelayCommand(obj =>
                    {
                        
                    }
                ));
            }
        }

        public ApplicationViewModel()
        {
            CurrentPageViewModel = new OverviewViewModel(this);
        }
        public void OpenEvent(Event ev)
        {
            CurrentPageViewModel = new EventViewModel(ev);
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
