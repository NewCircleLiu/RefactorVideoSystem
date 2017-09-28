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
        //
        // GET: /VideoCode/
        private ICodeService codeService;
        private ICreateCode createCode;
        private IVideoService videoService;
        private IExportExcel exportExcel;

        public VideoCodeController(ICodeService codeService, ICreateCode createCode, IVideoService videoService, IExportExcel exportExcel)
        {
            this.codeService = codeService;
            this.createCode = createCode;
            this.videoService = videoService;
            this.exportExcel = exportExcel;
        }

        //设置邀请码状态为已打印
        public ActionResult setCodeStatus(int codeID)
        {
            Code code = codeService.getInviteCodeById(codeID);
            code.CodeStatus = 1;

            if (ModelState.IsValid)
            {
                codeService.updateInviteCode(code);
            }

            return Content("error");
        }

        //删除邀请码
        public ActionResult deleteInviteCode(int codeID)
        {
            Code code = codeService.getInviteCodeById(codeID);
            if (ModelState.IsValid)
            {
                codeService.deleteInviteCode(code);

                //修改视频邀请码数量
                Video video = videoService.getVideo(code.VideoID);
                int codeCounts;
                int codesNotUsed;
                int codesNotExport;
                int codesUsed;
                codeService.getCounts(video.VideoID, out codeCounts, out codesNotExport, out codesNotUsed, out codesUsed);
                video.CodeNotUsed = codesNotUsed;
                video.CodeUsed = codesUsed;
                video.CodeCounts = codeCounts;

                if (ModelState.IsValid)
                {
                    videoService.updateVideo(video);
                    return Content("success");
                }
            }

            return Content("error");
        }

        //生成邀请码
        [HttpPost]
        public ActionResult CreateCode(int codeCounts, int videoID)
        {
            if (videoID != -1)
            {
                Video video = videoService.getVideo(videoID);

                List<string> codeList = createCode.createCode(codeCounts, video);

                foreach (string code in codeList)
                {
                    Code c = new Code() { CodeStatus = 0, CodeValue = code, VideoID = video.VideoID, UserID = -1 };
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



        // GET: /VideoCode/ExportExcel
        //导出邀请码
        public ActionResult ExportExcel(int num = 0, int videoID = -1)
        {
            Code[] codeArray = null;
            string fileName = null;

            if (Request.IsAjaxRequest())
            {
                int count;
                int t1, t2, t3;
                codeService.getCounts(videoID, out t1, out count, out t2, out t3);
                //请求的数量大于已有的数量
                if (num > count)
                {
                    return Content("error");
                }
                return Content("success");
            }
            else
            {
                //导出单个视频的邀请码
                if (videoID != -1)
                {
                    if (num == 0) //表示导出改视频的所有邀请码
                    {
                        codeArray = videoService.getInviteCodes(videoID).ToArray();
                        //codeArray = vsc.Codes.Where(c => c.VideoID == videoID && c.CodeStatus == 0).ToArray();
                    }
                    else
                    {
                        int count;
                        int t1, t2, t3;
                        codeService.getCounts(videoID, out t1, out count, out t2, out t3);

                        //请求的数量大于已有的数量
                        if (num > count)
                        {
                            return RedirectToAction("", "VideoManager");
                        }

                        codeArray = videoService.getInviteCodes(videoID).ToArray();

                    }
                    fileName = codeArray[0].Video.VideoName;
                }
                //导出全部视频的邀请码
                else
                {

                    int count = codeService.getInviteCodesByStatus(0,-1).Count();
                    //没有可用的邀请码
                    if (count <= 0)
                    {
                        return RedirectToAction("", "VideoManager");
                    }

                    codeArray = codeService.getInviteCodesByStatus(0, -1).ToArray();
                    fileName = "全部视频";
                }

                //修改邀请码状态
                foreach (Code c in codeArray)
                {
                    c.CodeStatus = 1;
                    if (ModelState.IsValid)
                    {
                        codeService.updateInviteCode(c);
                    }
                }

                //修改视频邀请码数量
                //Video[] videoArray = vsc.Videos.ToArray();
                Video[] videoArray = videoService.getVideos().ToArray();
                foreach (Video v in videoArray)
                {
                    int codesNotUsed;
                    //该视频中没用的数量
                    int codesUsed;

                    int codesNotExport;

                    int codesCount;
                    codeService.getCounts(v.VideoID,out codesCount,out codesNotExport,out codesNotUsed,out codesUsed);
                    //该视频种用了的数量

                    v.CodeNotUsed = codesNotUsed;
                    v.CodeUsed = codesUsed;

                    if (ModelState.IsValid)
                    {
                        videoService.updateVideo(v);
                    }
                }
                byte[] bytes = exportExcel.WriteExcel(codeArray);
                return File(bytes, "application/vnd.ms-excel", fileName + ".xls");
            }
        }
    }
}
