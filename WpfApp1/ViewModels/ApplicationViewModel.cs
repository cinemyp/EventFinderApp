using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using WpfApp1.Models;
using WpfApp1.ViewModels.Interfaces;
using DAL.Interfaces;
using WpfApp1.ViewModels;

namespace WpfApp1
{
    public class ApplicationViewModel : INotifyPropertyChanged, IMainViewModel
    {
        IDbCrud tm;

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

        private RelayCommand openFavouritePage;
        public RelayCommand OpenFavouritePage
        {
            get
            {
                return openFavouritePage ??
                    (openFavouritePage = new RelayCommand(obj =>
                    {
                        ClearFilters();
                        CurrentPageViewModel = new FavouriteViewModel(tm, this, LoggedUser.ID);
                        DisplayFilters(false);
                    }));
            }
        }

        private RelayCommand openReportPage;
        public RelayCommand OpenReportPage
        {
            get
            {
                return openReportPage ??
                    (openReportPage = new RelayCommand(obj =>
                    {
                        ClearFilters();
                        CurrentPageViewModel = new ReportViewModel(tm, LoggedUser.ID);
                        DisplayFilters(false);
                    }));
            }
        }

        #region Фильтры
        private int filterHeight;
        public int FilterHeight
        {
            get { return filterHeight; }
            set
            {
                filterHeight = value;
                OnPropertyChanged("FilterHeight");
            }
        }
        public ObservableCollection<CategoryModel> Categories { get; set; }
        public ObservableCollection<Date> Dates { get; set; }
        public ObservableCollection<TypeModel> Types { get; set; }
        public ObservableCollection<CityModel> Cities { get; set; }

        private CityModel cityFilter;
        public CityModel CityFilter
        {
            get { return cityFilter; }
            set
            {
                cityFilter = value;

                if (CurrentPageViewModel.GetType() != PageType.Overview)
                    CurrentPageViewModel = OverviewViewModel;
                if(categoryFilter == null)
                {
                    categoryFilter = Categories[0];
                    OnPropertyChanged("CategoryFilter");
                }    
                OverviewViewModel.GetAllEvents(cityFilter.ID, categoryFilter.ID);
                OnPropertyChanged("CityFilter");

                dateFilter = Dates[0];
                OnPropertyChanged("DateFilter");
                typeFilter = Types[0];
                OnPropertyChanged("TypeFilter");

                DisplayFilters(true);
            }
        }
        private CategoryModel categoryFilter;
        public CategoryModel CategoryFilter
        {
            get { return categoryFilter; }
            set
            {
                categoryFilter = value;

                if (CurrentPageViewModel.GetType() != PageType.Overview)
                    CurrentPageViewModel = OverviewViewModel;

                OverviewViewModel.GetAllEvents(cityFilter.ID, categoryFilter.ID);
                OnPropertyChanged("CategoryFilter");

                dateFilter = Dates[0];
                OnPropertyChanged("DateFilter");
                typeFilter = Types[0];
                OnPropertyChanged("TypeFilter");

                DisplayFilters(true);
            }
        }

        private Date dateFilter;
        public Date DateFilter
        {
            get { return dateFilter; }
            set
            {
                dateFilter = value;

                OverviewViewModel.FilterByDate(cityFilter.ID, categoryFilter.ID, dateFilter);

                OnPropertyChanged("DateFilter");
                typeFilter = Types[0];
                OnPropertyChanged("TypeFilter");
            }
        }

        private TypeModel typeFilter;
        public TypeModel TypeFilter
        {
            get { return typeFilter; }
            set
            {
                typeFilter = value;
                OverviewViewModel.FilterByType(cityFilter.ID, categoryFilter.ID, dateFilter, typeFilter.ID);
                OnPropertyChanged("TypeFilter");
            }
        }
        #endregion

        #region Логин
        private UserModel LoggedUser;
        private RelayCommand signIn;
        public RelayCommand SignIn
        {
            get
            {
                return signIn ??
                    (signIn = new RelayCommand(obj =>
                    {
                        Login login = new Login(tm);
                        bool? result = login.ShowDialog();
                        if(result == true)
                        {
                            IsLogged = true;
                            LoggedUser = tm.GetUser(login.GetLoggedUser().ID);
                            InitContent();
                            NotificateUser();
                        }
                        //TODO: брать отсюда данные и проверять на вход
                    }
                ));
            }
        }
        private RelayCommand signOut;
        public RelayCommand SignOut
        {
            get
            {
                return signOut ??
                    (signOut = new RelayCommand(obj =>
                    {
                        IsLogged = false;
                        LoggedUser = null;
                        if (CurrentPageViewModel.GetType() != PageType.Overview)
                            CurrentPageViewModel = OverviewViewModel;
                    }
                ));
            }
        }

        private bool isLogged;
        public bool IsLogged
        {
            get { return isLogged; }
            set { isLogged = value;  ; OnPropertyChanged("IsLogged"); }
        }
        #endregion
        public ApplicationViewModel(IDbCrud crudServ, IReportRepository report)
        {
            tm = crudServ;
            InitDateFilterContent();
            Cities = new ObservableCollection<CityModel>(tm.GetCities());
            Categories = new ObservableCollection<CategoryModel>(tm.GetCategories());
            Types = new ObservableCollection<TypeModel>(tm.GetTypes());
            OverviewViewModel = new OverviewViewModel(crudServ, this);
            CurrentPageViewModel = OverviewViewModel;
            InitContent();

            DisplayFilters(true);
        }

        public void OpenEvent(EventModel ev)
        {
            CurrentPageViewModel = new EventViewModel(ev, tm, this);

            ClearFilters();

            DisplayFilters(false);
        }

        public int GetLoggedUserId()
        {
            return LoggedUser == null ? -1 : LoggedUser.ID;
        }

        private void ClearFilters()
        {
            categoryFilter = null;
            OnPropertyChanged("CategoryFilter");
        }

        private void InitContent()
        {
            cityFilter = Cities[0];
            categoryFilter = Categories[0];
            OnPropertyChanged("CityFilter");
            OnPropertyChanged("CategoryFilter");
            DateFilter = Dates[0];
        }

        private void InitDateFilterContent()
        {
            Dates = new ObservableCollection<Date>();
            for(int i = 0; i < 7; i++)
            {
                Dates.Add(new Date((DateValue)i));
            }
        }

        private void DisplayFilters(bool isVisible)
        {
            if (isVisible)
                FilterHeight = 30;
            else
                FilterHeight = 0;
        }

        private void NotificateUser()
        {
            List<EventModel> todaysEvents = tm.GetTodaysEvent(LoggedUser.ID);
            if (todaysEvents.Count == 0)
                return;

            NotificationWindow w = new NotificationWindow(todaysEvents);
            w.Show();
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
