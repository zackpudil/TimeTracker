using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Infrastructure;
using AutoMapperAssist;
using Ilm.CodeAudition.Service.Mappers;

namespace Ilm.CodeAudition.Service.Infrastructure
{
    public class AppStart
    {
        public static void RegisterAssemblies(IKernel kernel)
        {
            if (kernel == null)
                throw new ArgumentException("can not be null", "kernel");

            kernel.Bind<IViewModelToTimesheetMapper>().To<ViewModelToTimesheetMapper>();

            kernel.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses()
                .InNamespaces(new string[] { "Ilm.CodeAudition.Service.Mappers", "Ilm.CodeAudition.Service.Validators" })
                .BindAllInterfaces()
                .Configure(b => b.InSingletonScope()));

            kernel.Bind(x => x
                .FromThisAssembly()
                .SelectAllClasses()
                .InNamespaces(new string[] { "Ilm.CodeAudition.Service.Services"})
                .BindAllInterfaces()
                .Configure(b => b.InTransientScope()));
        }
    }
}
