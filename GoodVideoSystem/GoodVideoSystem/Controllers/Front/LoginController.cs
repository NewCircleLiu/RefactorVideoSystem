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
        public ActionResult SavedeviceUniqueCode(string deviceUniqueCode = null)
        {
           Session["deviceUniqueCode"] = deviceUniqueCode;         
           return Content("success");
        }
        //判断当前用户使用的这个浏览器是否是第一次是第一次登录

        public ActionResult GetUserInfoFromCookie()
        {
            if (Request.Cookies["User"] != null)//是第一次登录
            {
                int UserId = int.Parse(Request.Cookies["User"]["UserId"]);
                Session["UserId"] = UserId.ToString();
                return Content("success");
            }
            else
            {
                return Content("failure");
            }
        }
    }
}