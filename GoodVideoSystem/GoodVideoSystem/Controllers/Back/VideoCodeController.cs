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
    public class VideoCodeController : Controller
    {
        public readonly int UNACTIVE_ = 0;
        public readonly int ACTIVE_ = 1;
        public readonly int USED_ = 2;

        // GET: /VideoCode/
        private ICodeService codeService;
        private IUserService userService;
        private ICreateCode createCode;
        private IVideoService videoService;
        private IExportExcel exportExcel;

        public VideoCodeController(ICodeService codeService, IUserService userService, ICreateCode createCode, IVideoService videoService, IExportExcel exportExcel)
        {
            this.codeService = codeService;
            this.userService = userService;
            this.createCode = createCode;
            this.videoService = videoService;
            this.exportExcel = exportExcel;
        }

        //设置邀请码状态为已打印
        public ActionResult setCodeStatus(int codeID)
        {
            Code code = codeService.getInviteCodeById(codeID);
            code.CodeStatus = ACTIVE_;

            if (ModelState.IsValid)
            {
                codeService.updateInviteCode(code);
            }

            return Content("error");
        }

        //删除邀请码
        public ActionResult deleteInviteCode(int codeID)
        {
            //1.从邀请码表中删除邀请码
            Code code = codeService.getInviteCodeById(codeID);
            codeService.deleteInviteCode(code);

            //2.从用户表中删除该邀请码
            userService.deleteInviteCode(code);

            /*
            if (ModelState.IsValid)
            {
                codeService.deleteInviteCode(code);

                //修改视频邀请码数量
                Video video = videoService.getVideo(code.vid);
                int codeCounts;
                int codesNotUsed;
                int codesNotExport;
                int codesUsed;
                codeService.getCounts(video.vid, out codeCounts, out codesNotExport, out codesNotUsed, out codesUsed);
                video.CodeNotUsed = codesNotUsed;
                video.CodeUsed = codesUsed;
                video.CodeCounts = codeCounts;

                if (ModelState.IsValid)
                    videoService.updateVideo(video);
                else
                    return Content("error");
            }*/
            return Content("success");
        }

        //生成邀请码
        [HttpPost]
        public ActionResult CreateCode(int codeCounts, int vid)
        {
            if (vid != -1)
            {
                Video video = videoService.getVideo(vid);

                List<string> codeList = createCode.createCode(codeCounts, video);

                foreach (string code in codeList)
                {
                    Code c = new Code() { CodeStatus = UNACTIVE_, CodeValue = code, vid = video.vid, UserID = -1 };
                    if (ModelState.IsValid)
                    {
                        codeService.addInviteCode(c);
                    }
                }

                video.CodeNotUsed += codeCounts;
                if (ModelState.IsValid)
                {
                    videoService.updateVideo(video);
                }
                return Content("生成成功");
            }
            return Content("生成失败");
        }

        public ActionResult ExportExcel(int vid = -1)
        {
            string fileName = "全部视频-邀请码";
            
             List<Code> codeList = codeService.getAllInviteCodes().ToList();
             if(codeList == null)
                 return RedirectToAction("", "VideoManager");
             if (codeList.Count() == 0)
                 return RedirectToAction("", "VideoManager");

             if (vid != -1)
            {
                for (int i = codeList.Count() - 1; i >= 0; i--)
                {
                    if (codeList[i].vid != vid)
                        codeList.Remove(codeList[i]);
                }

                if (codeList == null)
                    return RedirectToAction("", "VideoManager");
                if (codeList.Count() == 0)
                    return RedirectToAction("", "VideoManager");

                Video video = videoService.getVideo(vid);
                fileName = video.VideoName + "-邀请码";
            }

             if (codeList.Count() <= 0)
                return RedirectToAction("", "VideoManager");

            //设置为已打印=======================================
            for (int i = 0; i < codeList.Count(); i++)
            {
                if (codeList[i].CodeStatus == UNACTIVE_)
                {
                    codeList[i].CodeStatus = ACTIVE_;
                    codeService.updateInviteCode(codeList[i]);
                }
            }
            //===================================================

            byte[] bytes = exportExcel.WriteExcel(codeList.ToArray());
            return File(bytes, "application/vnd.ms-excel", fileName + ".xls");
        }
    }
}