using GoodVideoSystem.App_Start;
using GoodVideoSystem.Models.Repository;
using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Repositories.Repository;
using GoodVideoSystem.Services.IService;
using GoodVideoSystem.Services.Service;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GoodVideoSystem
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer<BaseDbContext>(new DropCreateDatabaseIfModelChanges<BaseDbContext>()); 

            registerService();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterView();
        }

        protected void RegisterView()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new MyViewEngine());
        }

        protected void registerService()
        {
            IocConfig Ioc = new IocConfig();
            //register repository
            Ioc.Register<IActionLogRepository, ActionLogRepository>();
            Ioc.Register<ICodeRepository, CodeRepository>();
            Ioc.Register<IExceptionLogRepository, ExceptionLogRepository>();
            Ioc.Register<IManagerRepository, ManagerRepository>();
            Ioc.Register<IProductRepository, ProductRepository>();
            Ioc.Register<ISuggestRepository, SuggestRepository>();
            Ioc.Register<IUserRepository, UserRepository>();
            Ioc.Register<IVideoRepository, VideoRepository>();

            //register service
            Ioc.Register<IActionLogService,ActionLogService>();
            Ioc.Register<ICodeService,CodeService>();
            Ioc.Register<IExceptionLogServcei,ExceptionLogServcei>();
            Ioc.Register<IManagerService,ManagerService>();
            Ioc.Register<IProductService,ProductService>();
            Ioc.Register<ISuggestService,SuggestService>();
            Ioc.Register<IUserService,UserService>();
            Ioc.Register<IVideoService, VideoService>();
            DependencyResolver.SetResolver(Ioc);
        }
    }
}