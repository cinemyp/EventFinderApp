﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class ReportData
    {
        public int CountFavouriteEvents { get; set; }
        public string FavouriteCategory { get; set; }
        public string FavouriteType { get; set; }
        public List<EventModel> FavouriteEvents { get; set; }
    }
}
