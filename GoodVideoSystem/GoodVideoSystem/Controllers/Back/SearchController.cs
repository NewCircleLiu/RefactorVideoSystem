using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoodVideoSystem.Services.filters;
using GoodVideoSystem.Services.IService;
using GoodVideoSystem.Services.Service;
using RefactorVideoSystem.Models.Models;
using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Models.Abstract;

namespace GoodVideoSystem.Controllers.Back
{
    [ManagerAuthorize]
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        private ICodeService codeService;
        private ISuggestService suggestService;
        private IUserService userService;
        private IVideoService videoService;
        private IPaging ip;

        public SearchController(ICodeService codeService, ISuggestService suggestService, IUserService userService, IVideoService videoService, IPaging ip)
        {
            this.codeService = codeService;
            this.suggestService = suggestService;
            this.userService = userService;
            this.videoService = videoService;
            this.ip = ip;
        }
        public ActionResult Index(string model, string searchType, string searchValue, int page_id = 1)
        {
            if (string.IsNullOrEmpty(model) || string.IsNullOrEmpty(searchType) || string.IsNullOrEmpty(searchValue))
            {
                model = (string)Session["search_model"];
                searchType = (string)Session["searchType"];
                searchValue = (string)Session["searchValue"];
            }
            else
            {
                Session["search_model"] = model.Trim();
                Session["searchType"] = searchType.Trim();
                Session["searchValue"] = searchValue.Trim();
            }


            //如果是查找某视频的所有邀请码
            if (model == "VideoCode")
            {
                int vid = (int)Session["vid"];

                IEnumerable<Code> codeList = null;

                //按邀请码查询
                if (searchType == "code")
                {
                    codeList = codeService.getInviteCodesContainsCode(searchValue, vid);
                }
                //按状态查询
                if (searchType == "status")
                {
                    int statusInt = Convert.ToInt32(searchValue);
                    codeList = codeService.getInviteCodesByStatus(statusInt, vid);
                }
                int codeCount, codeCountNotExport, codeCountUsed, codeNotUsed;
                codeService.getCounts(vid, out codeCount, out codeCountNotExport, out codeNotUsed, out codeCountUsed);
                ip.GetCurrentPageData(codeList, page_id);
                TempData["codeCount"] = codeCount;

                TempData["codeCountNotExport"] = codeCountNotExport;

                TempData["codeCountUsed"] = codeCountUsed;

                ViewBag.searchAction = "/Search/Index/Page";
                return View("/Views/Back/VideoManager/getInviteCode.cshtml", ip);
            }
            //查询视频
            if (model == "Video")
            {
                IEnumerable<Video> videoList = null;

                //按编号
                if (searchType == "vid")
                {
                    int vid = Convert.ToInt32(searchValue);
                    videoList = videoService.getVideosById(vid);
                }
                //按名称
                if (searchType == "videoName")
                {
                    videoList = videoService.getVideosByName(searchValue);
                }

                TempData["videoCount"] = videoService.getVideoCount();

                ViewBag.searchAction = "/Search/Index/Page";
                ip.GetCurrentPageData(videoList, page_id);
                return View("/Views/Back/VideoManager/Index.cshtml", ip);
            }
            //查询建议
            if (model == "Suggest")
            {
                IEnumerable<Suggest> suggestList = null;
                int recordcount;
                
                //内容
                if (searchType == "suggestValue")
                {
                    suggestList = suggestService.getSuggestsByText(searchValue, out recordcount);
                }

                //电话
                if (searchType == "phone")
                {
                    suggestList = suggestService.getSuggestsbByPhone(searchValue, out recordcount);
                }


                ip.GetCurrentPageData(suggestList, page_id);
                ViewBag.searchAction = "/Search/Index/Page";
                return View("/Views/Back/UserManager/UserSuggestsPage.cshtml", ip);
            }
            //查询用户
            if (model == "User")
            {
                IEnumerable<User> userList = null;
                int recordcount;
                //电话
                if (searchType == "phone")
                {
                    userList = userService.getUsersByPhone(searchValue, out recordcount);
                }
                ip.GetCurrentPageData(userList, page_id);
                ViewBag.searchAction = "/Search/Index/Page";
                return View("/Views/Back/UserManager/Index.cshtml", ip);
            }
            return null;
        }
    }
}
