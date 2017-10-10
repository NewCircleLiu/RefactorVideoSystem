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
            if (string.IsNullOrEmpty(deviceUniqueCode))
                return RedirectToAction("Index", "User");

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
            //0.1.异常处理，如果没有获取到地址，则跳转到欢迎页面获取设备标识
            string deviceUniqueCode = (string)Session["deviceUniqueCode"];
            if (string.IsNullOrEmpty(deviceUniqueCode))
                return RedirectToAction("Index", "User");

            //0.2.异常处理，如果传过来的邀请码为空，则跳转到获取视频页面
            if (string.IsNullOrEmpty(videoInviteCode))
                return RedirectToAction("Home", "User");

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

            //3.当前用户是否有权限，如果Session失效，跳转到Index
            string deviceUniqueCode = (string)Session["deviceUniqueCode"];
            if (string.IsNullOrEmpty(deviceUniqueCode))
                return RedirectToAction("Index", "User");

            User currentUser = userService.getUserByDevice(deviceUniqueCode);
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
            //如果用户名和手机号是空则跳转到AddUserInfo页面重新录入（客户端也有判断）
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(phone))
                return RedirectToAction("AddUserInfo","User");

            //Session失效处理，跳转到Index
            string deviceUniqueCode_ = (string)Session["deviceUniqueCode"];
            if (string.IsNullOrEmpty(deviceUniqueCode_))
                return RedirectToAction("Index", "User");

            string inviteCode_ = (string)Session["inviteCode"];
            if (string.IsNullOrEmpty(inviteCode_))
                return RedirectToAction("Index", "User");

            //根据电话号码查找用户，如果用户不存在则新建用户，否则更新邀请码
            User user = userService.getUserByPhone(phone.Trim());
            if (user == null)
            {
                user = new User() { Username = username, Phone = phone, InviteCodes = inviteCode_ };
                userService.registeUser(user);
            }

            else
            {
                if (!user.InviteCodes.Contains(inviteCode_))
                {
                    user.InviteCodes += "," + inviteCode_;
                    userService.updateUser(user);
                }
            }

            //邀请码加入当前硬件信息（如果需要的话）
            string videoInviteCode = inviteCode_.ToString();
            Code returnCode = null;
            string returnInfo = codeService.checkInviteCode(videoInviteCode, out returnCode);

            if (returnInfo.Equals("AVAILABLE")) //如果输入合法的邀请码（数据库中存在）
                codeService.updateInviteCodeInfo(returnCode, deviceUniqueCode_);

            return RedirectToAction("Home");
        }

        /*
        * @desc 意见反馈
        * @url /user/suggest
        * @method POST
        */
        [HttpPost]
        public ActionResult Suggest(string suggestText, string phone)
        {
            Suggest suggest = new Suggest();
            suggest.Text = suggestText;
            suggest.CreateTime = DateTime.Now;
            suggest.UserPhone = phone;

            suggestService.addSuggest(suggest);
            return Content("success");
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