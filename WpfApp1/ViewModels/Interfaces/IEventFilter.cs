using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.ViewModels.Interfaces
{
    public interface IEventFilter
    {
        void FilterByCategory(CategoryModel category);
        void FilterByType(TypeModel type);
        void FilterByTime(DateTime time);
    }
}
