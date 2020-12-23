﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public enum PageType { Overview, Event, Favourite }
    public interface IPageViewModel
    {
        PageType GetType();
    }
}
