﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WpfApp1.ViewModels.Interfaces;
using WpfApp1.Models;
using System;
using DAL;

namespace WpfApp1
{
    public class OverviewViewModel : INotifyPropertyChanged, IPageViewModel
    {
        IDbCrud tm;
        IMainViewModel mvm;

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
                            mvm.OpenEvent(ev);
                    }
                ));
            }
        }
        public OverviewViewModel(IDbCrud tm, IMainViewModel mvm)
        {
            this.tm = tm;
            //Events = new ObservableCollection<EventModel>(tm.GetEvents());
            this.mvm = mvm;
        }

        public void GetAllEvents(int cityId, int categoryId)
        {
            switch(categoryId)
            {
                case 1: //если мы выбрали категорию "Все", тк ни одно событие не имеет категорию "Все"
                    Events = new ObservableCollection<EventModel>(tm.GetEvents(cityId));
                    break;
                default:
                    Events = new ObservableCollection<EventModel>(tm.GetEvents(cityId).Where(e => e.CategoryId == categoryId));
                    break;
            }
        }
        public void FilterByDate(int cityId, int categoryId, Date d)
        {
            Events = new ObservableCollection<EventModel>(tm.GetEvents(cityId, d));
            
            if(categoryId > 1)
                Events = new ObservableCollection<EventModel>(Events.Where(e => e.CategoryId == categoryId));
        }

        public void FilterByType(int cityId, int categoryId, Date d, int typeId)
        {
            Events = new ObservableCollection<EventModel>(tm.GetEvents(cityId, d));

            if (categoryId > 1)
                Events = new ObservableCollection<EventModel>(Events.Where(e => e.CategoryId == categoryId));
            if (typeId > 1)
                Events = new ObservableCollection<EventModel>(Events.Where(e => e.TypeId == typeId));
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
