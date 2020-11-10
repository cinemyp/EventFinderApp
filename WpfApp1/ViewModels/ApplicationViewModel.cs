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
        private IPageViewModel _previousPageViewModel;
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
