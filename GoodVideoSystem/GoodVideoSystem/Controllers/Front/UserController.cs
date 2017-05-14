using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoodVideoSystem.Controllers.Front
{
    public class UserController : Controller
    {
        /*
        * @desc 欢迎首页
        * @url /
        * @method GET
        */
        public ActionResult Index()
        {
            return Content("index");
        }

        /*
        * @desc 用户主页
        * @url /user/home
        * @method GET
        */
        public ActionResult Home()
        {
            return Content("home");
        }

        /*
        * @desc 关于我们页面
        * @url /user/about
        * @method GET
        */
        public ActionResult About()
        {
            return View();
        }

        /*
       * @desc 联系我们页面
       * @url /user/contact
       * @method GET
       */
        public ActionResult Contact()
        {
            return View();
        }

        /*
         * @desc 产品展示页面
         * @url /user/product
         * @method GET
         */
        public ActionResult Product()
        {
            return Content("product");
        }

        /*
        * @desc 播放视频页面
        * @url /user/play
        * @method GET
        */
        public ActionResult Play()
        {
            return Content("play");
        }

        /*
        * @desc 意见反馈
        * @url /user/suggest
        * @method POST
        */
        [HttpPost]
        public ActionResult Suggest()
        {
            return Content("suggest");
        }

        /*
        * @desc 获取视频
        * @url /user/GetVideo
        * @method POST
        */
        [HttpPost]
        public ActionResult GetVideo()
        {
            return Content("get video");
        }
    }
}
