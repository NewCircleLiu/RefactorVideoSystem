using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoodVideoSystem.App_Start
{
    public class IocConfig:IDependencyResolver
    {
        public IKernel Kernel { get; private set; }

        public IocConfig()
        {
            this.Kernel = new StandardKernel();
        }

        public void Register<TFrom, TTo>() where TTo : TFrom
        { 
            this.Kernel.Bind<TFrom>().To<TTo>();
        }

        public object GetService(Type serviceType)
        {
            return this.Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.Kernel.GetAll(serviceType);
        }
    }
}