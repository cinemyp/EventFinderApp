using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels.Interfaces
{
    public interface IEventFilter
    {
        void FilterByCategory(Category category);
        void FilterByType(DAL.Type type);
        void FilterByTime(DateTime time);
    }
}
