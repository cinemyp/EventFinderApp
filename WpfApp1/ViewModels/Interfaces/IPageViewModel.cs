using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public enum PageType { Overview, Event, Favourite, Report }
    public interface IPageViewModel
    {
        PageType GetType();
    }
}
