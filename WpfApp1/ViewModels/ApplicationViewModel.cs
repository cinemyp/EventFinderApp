using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using WpfApp1.Models;
using WpfApp1.ViewModels.Interfaces;

namespace WpfApp1
{
    
    public class ApplicationViewModel : INotifyPropertyChanged, IPageManager
    {
        TransactionManager tm;

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
        private OverviewViewModel OverviewViewModel;

        public ObservableCollection<CategoryModel> Categories { get; set; }

        private CategoryModel categoryFilter;
        public CategoryModel CategoryFilter
        {
            get { return categoryFilter; }
            set
            {
                categoryFilter = value;

                if (CurrentPageViewModel.GetType() == PageType.Event)
                    CurrentPageViewModel = OverviewViewModel;

                OverviewViewModel.FilterByCategory(categoryFilter);
                OnPropertyChanged("CategoryFilter");
                
                //функция фильтрации
                //возможно изменить интерфейс, хз как сделать обособленно
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


        public ApplicationViewModel()
        {
            tm = new TransactionManager();
            OverviewViewModel = new OverviewViewModel(tm, this);
            CurrentPageViewModel = OverviewViewModel;
            Categories = new ObservableCollection<CategoryModel>(tm.GetCategories());
        }

        public void OpenEvent(EventModel ev)
        {
            CurrentPageViewModel = new EventViewModel(ev);
            categoryFilter = null;
            OnPropertyChanged("CategoryFilter");
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
