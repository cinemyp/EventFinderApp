﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WpfApp1.ViewModels.Interfaces;
using WpfApp1.Models;
using System;

namespace WpfApp1
{
    public class OverviewViewModel : INotifyPropertyChanged, IPageViewModel
    {
        TransactionManager tm;
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
        public OverviewViewModel(TransactionManager tm, IPageManager pm)
        {
            this.tm = tm;
            Events = new ObservableCollection<EventModel>(tm.GetEvents());
            this.pm = pm;
        }

        public void FilterByCategory(CategoryModel c)
        {
            int category = c.ID;
            switch(category)
            {
                case 1: //если мы выбрали категорию "Все", тк ни одно событие не имеет категорию "Все"
                    Events = new ObservableCollection<EventModel>(tm.GetEvents());
                    break;
                default:
                    Events = new ObservableCollection<EventModel>(tm.GetEvents().Where(e => e.CategoryId == category));
                    break;
            }
        }
        public void FilterByDate(Date d)
        {
            Events = new ObservableCollection<EventModel>(tm.GetEvents(d));
        }

        PageType IPageViewModel.GetType()
        {
            return PageType.Overview;
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
