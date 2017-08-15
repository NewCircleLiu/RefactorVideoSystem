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

namespace VideoSystem.Controllers.Back
{
    [ManagerAuthorize]
    public class SettingsController : Controller
    {
        private IManagerService managerService;

        //
        // GET: /Settings/
        //修改管理员信息


        public SettingsController(IManagerService managerService)
        {
            this.managerService = managerService;
        } //注入

        public ActionResult Index()
        {
            Manager manager = (Manager)Session["Manager"];
            ViewBag.account = manager.Account;
            return View(manager);
        }

        //编辑信息


        public ActionResult EditInfo(string email, string phone)
        {
            Manager manager = (Manager)Session["Manager"];
            manager.Email = email;
            manager.Phone = phone;

            managerService.editManager(manager);
            return RedirectToAction("", "Settings");
        }

        //跳转到修改密码页面
        public ActionResult ModifyPassPage()
        {
            Manager manager = (Manager)Session["Manager"];
            ViewBag.account = manager.Account;
            return View();
        }

        //修改密码
        public ActionResult ModifyPass(string oldPass, string newPass)
        {
            Manager manager = (Manager)Session["Manager"];

            if (oldPass != manager.Password)
            {
                TempData["erroInfo"] = "原始密码不正确";
                return RedirectToAction("ModifyPassPage", "Settings");
            }
            else
            {
                manager.Password = newPass;
                managerService.editManager(manager);
                FormsAuthentication.SignOut();
                Response.Cookies.Clear();
                return RedirectToAction("", "Qhgypacz");
            }
        }
    }
}
