using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GoodVideoSystem.Services.filters;
using RefactorVideoSystem.Models.Models;
using GoodVideoSystem.Services.IService;
using GoodVideoSystem.Models.LCUtils;
using GoodVideoSystem.Models.Abstract;

namespace GoodVideoSystem.Controllers.Back
{
    [ManagerAuthorize]
    public class VideoManagerController : Controller
    {
        //
        // GET: /VideoManager/
        private IVideoService videoService;
        private IPaging ip;
        private ICodeService codeService;



        public VideoManagerController(IVideoService videoService, IPaging ip, ICodeService codeService)
        {
            this.videoService = videoService;
            this.ip = ip;
            this.codeService = codeService;
        }

        public ActionResult Index(int page_id = 1)
        {
            IEnumerable<Video> videoList = videoService.getVideosById(-1);
            TempData["videoCount"] = videoList.Count();
            Manager manager = (Manager)Session["Manager"];
            ViewBag.searchAction = "/VideoManager/Index/Page";
            ViewBag.account = manager.Account;
            ip.GetCurrentPageData(videoList, page_id);
            return View(ip);
        }
        public ActionResult UploadImg(int videoID, string VideoImageLocal)
        {
            Video v = videoService.getVideo(videoID);
            v.VideoImageLocal = VideoImageLocal;
            if (ModelState.IsValid)
            {
                videoService.updateVideo(v);
            }
            return RedirectToAction("Index", "VideoManager");
        }
        //跳转上传视频页面
        public ActionResult UploadPage(int VideoID)
        {
            Video v = videoService.getVideo(VideoID);
            Manager manager = (Manager)Session["Manager"];
            ViewBag.account = manager.Account;

            return View(v);
        }

        //上传接收来自客户端的视频信息
        [AllowAnonymous]
        public ActionResult UploadVideo(string videoInfo, string token)
        {
            if (token != null & token == "123456789")
            {
                string[] info = videoInfo.Split('_');
                string video_id = info[0];
                string video_uuid = info[1];
                string video_name = info[2];

                Video v = new Video();
                v.CodeCounts = 0;
                v.CodeNotUsed = 0;
                v.CodeUsed = 0;
                v.fileID = "001";
                v.VideoName = video_name;
                v.VideoImageLocal = "null";
                v.CreateTime = DateTime.Now;

                if (ModelState.IsValid)
                {
                    videoService.addVideo(v);
                    return Content("ok");
                }
            }
            return Content("error");
        }

        //删除视频
        public ActionResult DeleteVideo(int VideoID)
        {
            Video v = videoService.getVideo(VideoID);
            if (ModelState.IsValid)
            {
                videoService.deleteVideo(v);
                //删除视频文件和视频首图
                if (v.VideoImageLocal != "null")
                {
                    string imgUrl = Server.MapPath(v.VideoImageLocal);
                    FileInfo img = new FileInfo(imgUrl);
                    img.Delete();
                }

                LCUtils lc = new LCUtils();
                jsonout result = lc.deleteVideo(v.fileID);

                if (result.code == "0")
                {
                    return Content("success");
                }
                else
                {
                    return Content("erro");
                }
            }

            return Content("erro");
        }

        public ActionResult getInviteCode(int VideoID = -1, int page_id = 1)
        {
            if (VideoID != -1)
            {
                Session["VideoID"] = VideoID;
            }
            else
            {
                VideoID = (int)Session["VideoID"];
            }

            IEnumerable<Code> codeList = null;
            codeList = videoService.getInviteCodes(VideoID);

            ip.GetCurrentPageData(codeList, page_id);

            int codeCount, codeCountNotExport, codeCountNotUsed,codeCountUsed;
            codeService.getCounts(VideoID, out codeCount, out codeCountNotExport, out codeCountNotUsed, out codeCountUsed);
            TempData["codeCount"] = codeCount;

            TempData["codeCountNotExport"] = codeCountNotExport;

            TempData["codeCountUsed"] = codeCountUsed;
            Manager manager = (Manager)Session["Manager"];
            ViewBag.searchAction = "/VideoManager/getInviteCode/Page";
            ViewBag.account = manager.Account;
            return View(ip);
        }
    }
}
