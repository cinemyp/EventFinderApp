using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Util
{
    public class LoginModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoginWindow>().To<Login>().InSingletonScope();
        }
    }
}
