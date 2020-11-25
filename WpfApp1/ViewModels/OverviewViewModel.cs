﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp1.ViewModels.Interfaces;
using DAL;
using System.Windows.Documents;
using System.Collections.Generic;

namespace WpfApp1
{
    public class OverviewViewModel : INotifyPropertyChanged, IPageViewModel
    {
        TransactionManager tm;
        IPageManager pm;
        private List<Event> AllEvents;
        private ObservableCollection<Event> events;
        public ObservableCollection<Event> Events
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
                        Event ev = Events.ToList().Find(e => e.Title == title);
                        if (ev != null)
                            pm.OpenEvent(ev);
                    }
                ));
            }
        }
        public OverviewViewModel(TransactionManager tm, IPageManager pm)
        {
            this.tm = tm;
            AllEvents = tm.GetEvents();
            Events = new ObservableCollection<Event>(AllEvents);
            this.pm = pm;
        }

        public void FilterByCategory(Category c)
        {
            int category = c.ID;
            switch(category)
            {
                case 1: //если мы выбрали категорию "Все", тк ни одно событие не имеет категорию "Все"
                    Events = new ObservableCollection<Event>(AllEvents);
                    break;
                default:
                    Events = new ObservableCollection<Event>(AllEvents.Where(e => e.CategoryId == category));
                    break;
            }
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
