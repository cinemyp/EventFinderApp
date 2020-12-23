using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using WpfApp1.Models;
using WpfApp1.ViewModels.Interfaces;
using System.Linq;
using System.Reflection;
using System.Windows;

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

                dateFilter = Dates[0];
                OnPropertyChanged("DateFilter");
                typeFilter = Types[0];
                OnPropertyChanged("TypeFilter");

                HandleFilter(true);
            }
        }

        private Date dateFilter;
        public Date DateFilter
        {
            get { return dateFilter; }
            set
            {
                dateFilter = value;

                OverviewViewModel.FilterByDate(categoryFilter.ID, dateFilter);

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
                OverviewViewModel.FilterByType(categoryFilter.ID, dateFilter, typeFilter.ID);
                OnPropertyChanged("TypeFilter");
            }
        }
        #endregion


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

        private Visibility isLoggedVisibility;
        public Visibility IsLoggedVisibility
        {
            get { return isLoggedVisibility; }
            set { isLoggedVisibility = value; OnPropertyChanged("IsLoggedVisibility"); }
        }

        public ApplicationViewModel()
        {
            tm = new TransactionManager();
            Categories = new ObservableCollection<CategoryModel>(tm.GetCategories());
            InitDateFilterContent();
            Types = new ObservableCollection<TypeModel>(tm.GetTypes());
            OverviewViewModel = new OverviewViewModel(tm, this);
            CurrentPageViewModel = OverviewViewModel;
            InitContent();

            HandleFilter(true);
        }

        public void OpenEvent(EventModel ev)
        {
            CurrentPageViewModel = new EventViewModel(ev);

            categoryFilter = null;
            OnPropertyChanged("CategoryFilter");

            HandleFilter(false);
        }

        private void InitContent()
        {
            CategoryFilter = Categories[0];
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

        private void HandleFilter(bool isVisible)
        {
            if (isVisible)
                FilterHeight = 30;
            else
                FilterHeight = 0;
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
