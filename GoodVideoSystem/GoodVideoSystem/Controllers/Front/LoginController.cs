using GoodVideoSystem.Services.filters;
using GoodVideoSystem.Services.IService;
using GoodVideoSystem.Services.Service;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoodVideoSystem.Controllers.Front
{
    public class LoginController : Controller
    {
        private IUserService userService;

        //通过构造方法注入服务层
        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        /*
        * @desc 检查当前登录用户是否已经注册
        * @url /
        * @method GET
        */
        public ActionResult SavedeviceUniqueCode(string deviceCode = null)
        {
           Session["deviceUniqueCode"] = deviceCode;         
           return Content("success");
        }
    }
}