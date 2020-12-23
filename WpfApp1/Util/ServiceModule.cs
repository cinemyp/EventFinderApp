using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Util
{
    public class ServiceModule : NinjectModule
    {
        public string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IDbRepository>().To<DbRepositorySQL>().InSingletonScope().WithConstructorArgument(connectionString);
            
        }
    }
}
