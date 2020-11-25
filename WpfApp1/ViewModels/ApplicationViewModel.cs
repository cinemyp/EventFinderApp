using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.Interfaces;
using DAL;
using System.Collections.ObjectModel;

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

        public ObservableCollection<Category> Categories { get; set; }

        private Category categoryFilter;
        public Category CategoryFilter
        {
            get { return categoryFilter; }
            set
            {
                categoryFilter = value;

                if (CurrentPageViewModel.GetType() == PageType.Event)
                    CurrentPageViewModel = new OverviewViewModel(tm, this);

                OverviewViewModel.FilterByCategory(categoryFilter);

                //TODO: Фильтрация
                //если текущая вьюмодель эта, то создаем новую ивент вью модель 
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
            Categories = new ObservableCollection<Category>(tm.GetCategories());
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
