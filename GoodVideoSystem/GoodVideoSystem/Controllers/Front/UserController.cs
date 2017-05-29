﻿using GoodVideoSystem.Services.filters;
using GoodVideoSystem.Services.IService;
using GoodVideoSystem.Services.Service;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoodVideoSystem.Controllers.Front
{
    public class UserController : Controller
    {
        private IUserService userService;
        private ICodeService codeService;
        private IProductService productService;


        //通过构造方法注入服务层
        public UserController(ICodeService codeService, IProductService productService, IUserService userService)
        {
            this.codeService = codeService;
            this.productService = productService;
            this.userService = userService;
        }

        /*
        * @desc 欢迎首页
        * @url /
        * @method GET
        */
        public ActionResult Index()
        {
            return View();
        }

        /*
        * @desc 用户主页
        * @url /user/home
        * @method GET
        */
        public ActionResult Home()
        {
            string deviceUniqueCode = (string)Session["deviceUniqueCode"];
            Code[] codes = codeService.getCodes(deviceUniqueCode).ToArray();
            return View(codes);
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
            return View(productService.getProducts().ToArray());
        }

        /*
        * @desc 播放视频页面
        * @url /user/play
        * @method GET
        */
        [UserAuthorise]
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
        public ActionResult GetVideo(string videoInviteCode)
        {
            Code returnCode = null;
            string returnInfo = codeService.checkCode(videoInviteCode, out returnCode);
            if (returnInfo.Equals("AVAILABLE"))
            {
                userService.updateUserInfo(returnCode, (string)Session["deviceUniqueCode"]);
                codeService.updateCodeInfo(returnCode, (string)Session["deviceUniqueCode"]);
            }
            return Content(returnInfo);
        }
    }
}