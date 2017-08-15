using GoodVideoSystem.Services.filters;
using GoodVideoSystem.Services.IService;
using GoodVideoSystem.Services.Service;
using RefactorVideoSystem.Models.Models;
using GoodVideoSystem.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GoodVideoSystem.Controllers.Back
{
    public class QhgypaczController : Controller
    {
        //
        // GET: /Qhgypacz/

        private IManagerService managerService;


        //通过构造方法注入服务层
        public QhgypaczController(IManagerService managerService)
        {
            this.managerService = managerService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [ManagerAuthorize]
        public ActionResult BackMain()
        {
            Manager manager = (Manager)Session["Manager"];
            ViewBag.account = manager.Account;
            return View();

        }
        [HttpPost]
        public ActionResult BackLogin(string account, string password)
        {
            Manager currentManager = null;
            string returnInfo = managerService.checkManager(account,password,out currentManager);

            if (currentManager==null)
            {
                return Content("error");
            }
            else
            {
                Session["Manager"] = currentManager;
                return Content("ok");
            }
        }
        //退出登录
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("", "Qhgypacz");
        }
    }
}
