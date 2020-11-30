using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public enum DateValue { Soon, Today, Tomorrow, Weekend, Week, Month, AnotherDay }
    public class Date
    {
        public string Name { 
            get 
            {
                switch(date)
                {
                    case DateValue.Soon:
                        return "ближайшее время";
                    case DateValue.Today:
                        return "сегодня";
                    case DateValue.Tomorrow:
                        return "завтра";
                    case DateValue.Weekend:
                        return "выходные";
                    case DateValue.Week:
                        return "неделя";
                    case DateValue.Month:
                        return "месяц";
                    case DateValue.AnotherDay:
                        return "выбрать день";
                    default:
                        return string.Empty;
                }
            }
        }
        private DateValue date;
        public DateValue Value { get { return date; } }

        public Date(DateValue date)
        {
            this.date = date;
        }
    }
}
