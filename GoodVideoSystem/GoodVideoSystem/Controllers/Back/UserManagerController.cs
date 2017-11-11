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
using GoodVideoSystem.Models.Abstract;


namespace GoodVideoSystem.Controllers.Back
{
    [ManagerAuthorize]
    public class UserManagerController : Controller
    {
        private IUserService userService;
        private ISuggestService suggestService;
        private ICodeService codeService;
        private IPaging ip;
        public UserManagerController(IUserService userService, ISuggestService suggestService, IPaging ip, ICodeService codeService)
        {
            this.suggestService = suggestService;
            this.userService = userService;
            this.ip = ip;
            this.codeService = codeService;
        }
        public ActionResult Index(int Page = 1)
        {
            int recordCount;
            IEnumerable<User> userList = userService.getUsers(out recordCount);
            ip.GetCurrentPageData(userList, Page);
            Manager manager = (Manager)Session["Manager"];
            ViewBag.searchAction = "/UserManager/Index?Page=";
            ViewBag.account = manager.Account;
            return View(ip);
        }

        //查看留言
        public ActionResult UserSuggestsPage(int Page = 1)
        {
            int recordcount;
            IEnumerable<Suggest> suggestList = suggestService.getSuggests(out recordcount);
            suggestList = suggestList.OrderByDescending(s => s.CreateTime);

            ip.GetCurrentPageData(suggestList, Page);
            Manager manager = (Manager)Session["Manager"];
            ViewBag.account = manager.Account;
            ViewBag.searchAction = "/UserManager/UserSuggestPage?Page=";
            return View(ip);
        }

        //查看用户视频
        public ActionResult GetUserVideo(int userID)
        {
            string userid = Convert.ToString(userID);
            User user = userService.getUserById(userID);
            string[] codes = user.InviteCodes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<Code> codeList = new List<Code>();
            //List<Code> temp = new List<Code>();
            foreach (string inviteCode in codes)
            {
                //temp=codeService.getInviteCodesContainsCode(inviteCode, -1).ToList();
                codeList.AddRange(codeService.getInviteCodesContainsCode(inviteCode, -1).ToList());
            }

            //codeArray = getInviteCode.getInviteCodeArray(codeArray, userid).ToArray();

            return View(codeList.ToArray());
        }

        //查看用户视频
        public ActionResult DeleteUser(int userID)
        {
            bool ret = userService.deleteUser(userID);
            if (ret)
                return Content("success");
            else
                return Content("error");
        }
    }
}