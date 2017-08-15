using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoodVideoSystem.Models.Abstract;
using GoodVideoSystem.Models.Concrete;
using GoodVideoSystem.Models.LCUtils;

namespace GoodVideoSystem.App_Start
{
    public class IocConfig:IDependencyResolver
    {
        public IKernel Kernel { get; private set; }

        public IocConfig()
        {
            this.Kernel = new StandardKernel();
            AddBindings();
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
        public void AddBindings()
        {
            //将接口绑定到它的实现
            Kernel.Bind<IVerifyCode>().To<VerifyCode>();
            Kernel.Bind<IPaging>().To<Paging>();
            Kernel.Bind<IUploadFile>().To<UploadFile>();
            Kernel.Bind<ICreateCode>().To<CreateCode>();
            Kernel.Bind<IEncryption>().To<Encryption>();
            Kernel.Bind<IExportExcel>().To<ExportExcel>();
        }
    }
}