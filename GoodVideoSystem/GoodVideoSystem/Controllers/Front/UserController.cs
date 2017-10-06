using GoodVideoSystem.Services.filters;
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
        private IVideoService videoService;
        private ISuggestService suggestService;

        //通过构造方法注入服务层
        public UserController(ICodeService codeService,
            IProductService productService,
            IUserService userService,
            IVideoService videoService,
            ISuggestService suggestService)
        {
            this.codeService = codeService;
            this.productService = productService;
            this.userService = userService;
            this.videoService = videoService;
            this.suggestService = suggestService;
        }

        /*
        * @desc 欢迎首页
        * @url /user/index
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
            Code[] inviteCodes = codeService.getInviteCodes(deviceUniqueCode).ToArray();
            User user = userService.getUserByDevice(deviceUniqueCode);
            Session["CurrentUser"] = user;
            return View(inviteCodes);
        }

        /*
        * @desc 获取视频
        * @url /user/GetVideo
        * @method POST
        */

        public ActionResult GetVideo(string videoInviteCode)
        {
            string deviceUniqueCode = (string)Session["deviceUniqueCode"];
            Session["inviteCode"] = videoInviteCode.Trim();

            Code returnCode = null;
            string returnInfo = codeService.checkInviteCode(videoInviteCode, out returnCode);

            if (!returnInfo.Equals("INVALID")) //如果输入合法的邀请码（数据库中存在）
            {
                //如果是新的邀请码输入新的设备，那么新建用户并且绑定当前邀请码
                if (!userService.IsCurrentUserExist(deviceUniqueCode, returnCode))
                    return Content("ADDUSERPAGE");

                if (returnInfo.Equals("AVAILABLE"))
                {
                    //若当前设备已经请求了当前邀请码所指示的视频则直接返回（避免重复请求）
                    if (returnCode.DeviceUniqueCode != null && returnCode.DeviceUniqueCode.Contains(deviceUniqueCode))
                        return Content("AVAILABLE");

                    //若当前邀请码所指示的视频没有在当前设备请求过
                    else
                    {
                        userService.updateUserInfo(returnCode, deviceUniqueCode);
                        codeService.updateInviteCodeInfo(returnCode, deviceUniqueCode);
                    }
                }
            }
            return Content(returnInfo);
        }

        /*
       * @desc 播放视频页面
       * @url /user/play
       * @method GET
       */
        //[UserAuthorise]
        public ActionResult Play(int vid = 0)
        {
            //1.视频是否存在
            Video video = videoService.getVideo(vid);
            if (video == null)
                return RedirectToAction("Error");

            //2.视频是否分发给用户
            Code[] codes = video.Code.ToArray();
            if (codes == null || codes.Count() == 0)
                return RedirectToAction("Error");

            //3.当前用户是否有权限
            User currentUser = userService.getUserByDevice((string)Session["deviceUniqueCode"]);
            for (int i = 0; i < codes.Count(); i++)
            {
                if (currentUser.InviteCodes.Contains(codes[i].CodeValue))
                    return View(video);
            }
            return RedirectToAction("Error");
        }

        public ActionResult AddUserInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(string username, string phone)
        {
            User user = new User() { Username = username, Phone = phone, InviteCodes = (string)Session["inviteCode"] };
            userService.registeUser(user);

            string deviceUniqueCode = (string)Session["deviceUniqueCode"];
            string videoInviteCode = Session["inviteCode"].ToString();

            Code returnCode = null;
            string returnInfo = codeService.checkInviteCode(videoInviteCode, out returnCode);

            if (returnInfo.Equals("AVAILABLE")) //如果输入合法的邀请码（数据库中存在）
                codeService.updateInviteCodeInfo(returnCode, deviceUniqueCode);

            return RedirectToAction("Home");
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
            int record;
            return View(productService.getProducts(out record).ToArray());
        }

        /*
        * @desc 意见反馈
        * @url /user/suggest
        * @method POST
        */
        [HttpPost]
        public ActionResult Suggest(string suggestText, string phone, string UserBrowser)
        {
            Suggest suggest = new Suggest();
            suggest.Text = suggestText;
            suggest.CreateTime = DateTime.Now;
            suggest.UserPhone = phone;
            User user = (User)Session["CurrentUser"];

            if (user != null)
            {
                suggest.user = user;
                suggestService.addSuggest(suggest);
                return Content("success");
            }
            else
            {
                return Content("error");
            }

        }
        /*
        * @desc 404
        * @url /user/Error
        * @method GET
        */
        public ActionResult Error()
        {
            return View();
        }
    }
}