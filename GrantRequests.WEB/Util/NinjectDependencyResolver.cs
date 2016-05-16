using System;
using System.Collections.Generic;
using System.Web.Mvc;
using GrantRequests.DAL.Repositories;
using GrantRequests.WEB.Services;
using Ninject;
using Ninject.Web.Common;
using GrantRequests.Common;

namespace GrantRequests.WEB.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel ninjectKernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            ninjectKernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<UnitOfWork>().To<UnitOfWork>().InRequestScope().WithConstructorArgument("connectionString", "name=GrantRequestsContext");
            ninjectKernel.Bind<RequestService>().To<RequestService>().InRequestScope();
            ninjectKernel.Bind<AccountService>().To<AccountService>().InRequestScope();
            ninjectKernel.Bind<ViewDataService>().To<ViewDataService>().InRequestScope();
        }

        public object GetService(Type serviceType)
        {
            return ninjectKernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ninjectKernel.GetAll(serviceType);
        }
    }
}