using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        EventFinderContext db;
        IEnumerable<Event> Events { get; set; }

        public ApplicationViewModel()
        {
            db = new EventFinderContext();
            db.Event.Load();
            Events = db.Event.Local.ToBindingList();
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
