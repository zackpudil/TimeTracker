using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Infrastructure;
using Ilm.CodeAudition.Data.Storage.Context;
using System.Data.Entity;

namespace Ilm.CodeAudition.Data.Infrastructure
{
    public class AppStart
    {
        public static void RegisterAssemblies(IKernel kernel)
        {
            if (kernel == null)
                throw new ArgumentException("can not be null", "kernel");

            kernel.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses()
                .BindAllInterfaces()
                .Configure(b => b.InTransientScope()));

            RegisterDatabaseInitializer();
        }

        public static void RegisterDatabaseInitializer()
        {
            //used for active development. Deletes database on every model change!
            Database.SetInitializer<TimeTrackerContext>(new TimeTrackerInitializer());
        }
    }
}
