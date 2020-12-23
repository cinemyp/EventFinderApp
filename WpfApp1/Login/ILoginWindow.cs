using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public interface ILoginWindow
    {
        void Close(WpfApp1.Models.UserModel user = null, bool? dialogResult = false);
        string GetPassword();
    }
}
