﻿using System;
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

        public ActionResult Index(int Page = 1)
        {
            IEnumerable<Video> videoList = videoService.getVideosById(-1);
            TempData["videoCount"] = videoList.Count();
            Manager manager = (Manager)Session["Manager"];
            ViewBag.searchAction = "/VideoManager/Index?Page=";
            ViewBag.account = manager.Account;
            ip.GetCurrentPageData(videoList, Page);
            return View(ip);
        }
        public ActionResult UploadImg(int vid, string coverImage)
        {
            Video v = videoService.getVideo(vid);
            v.coverImage = coverImage;
            if (ModelState.IsValid)
            {
                videoService.updateVideo(v);
            }
            return RedirectToAction("Index", "VideoManager");
        }
        //跳转上传视频页面
        public ActionResult UploadPage(int vid)
        {
            Video v = videoService.getVideo(vid);
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
                v.VideoName = video_name;
                v.coverImage = "null";
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
        public ActionResult DeleteVideo(int vid)
        {
            /*
            Video v = videoService.getVideo(vid);
            if (ModelState.IsValid)
            {
                videoService.deleteVideo(v);
                //删除视频文件和视频首图
                if (v.coverImage != "null")
                {
                    string imgUrl = Server.MapPath(v.coverImage);
                    FileInfo img = new FileInfo(imgUrl);
                    img.Delete();
                }

                LCUtils lc = new LCUtils();
                jsonout result = lc.deleteVideo(v.vid);

                if (result.code == "0")
                {
                    return Content("success");
                }
                else
                {
                    return Content("erro");
                }
            }*/

            return Content("erro");
        }

        public ActionResult getInviteCode(int vid = -1, int Page = 1)
        {
            if (vid != -1)
            {
                Session["vid"] = vid;
            }
            else
            {
                vid = (int)Session["vid"];
            }

            IEnumerable<Code> codeList = null;
            codeList = videoService.getInviteCodes(vid);

            ip.GetCurrentPageData(codeList, Page);

            int codeCount, codeCountNotExport, codeCountNotUsed,codeCountUsed;
            codeService.getCounts(vid, out codeCount, out codeCountNotExport, out codeCountNotUsed, out codeCountUsed);
            TempData["codeCount"] = codeCount;

            TempData["codeCountNotExport"] = codeCountNotExport;

            TempData["codeCountUsed"] = codeCountUsed;
            Manager manager = (Manager)Session["Manager"];
            ViewBag.searchAction = "/VideoManager/getInviteCode?Page=";
            ViewBag.account = manager.Account;
            return View(ip);
        }
    }
}
