using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class TypeModel
    {
        

        public int ID { get; set; }

        public string Name { get; set; }
        public TypeModel()
        {
        }
        public TypeModel(DAL.Type t)
        {
            ID = t.ID;
            Name = t.Name;
        }
    }
}
