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
        private List<IPageViewModel> _pageViewModels;
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

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
            PageViewModels.Add(new OverviewViewModel(this));
            PageViewModels.Add(new EventViewModel());

            CurrentPageViewModel = PageViewModels[0];
            
        }
        public void ChangePage()
        {
            CurrentPageViewModel = PageViewModels[1];
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
