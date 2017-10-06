using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GoodVideoSystem.Models.Utility;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RefactorVideoSystem.Models.Models;
using GoodVideoSystem.Services.IService;

namespace GoodVideoSystem.Controllers.Back
{
    public class PolyProcessController : Controller
    {
        private string secretkey = "QH8IHKX6JR";
        private string userid = "212b30914a";
        private DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));

        private IVideoService videoService;


        public PolyProcessController(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        public EmptyResult Callback()
        {
            string sign = Request.QueryString["sign"];
            string vid = Request.QueryString["vid"];
            string type = Request.QueryString["type"];
            string df = Request.QueryString["df"];
            string type_vid_sec = type + vid + secretkey;

            if (type == "pass")
            {
                string verifySign = Encypt.GetMD5(type_vid_sec);
                if (verifySign == sign)
                {
                    Video video = GetSingleVideo(vid);
                    videoService.addVideo(video);
                }
            }

            /*
            string vid = "212b30914ad7f00138158897c941f5dc_2";
            Video video = GetSingleVideo(vid);
            videoService.addVideo(video);
            */

            return null;
        }

        //vidstr:Code.vid, polyVid:Code.polyVid;
        public ActionResult DeleteVideo(string polyVid)
        {
            //在本站删除视频信息
            Video v = videoService.getVideoByPolyvid(polyVid);
            if (v != null)
                videoService.deleteVideo(v);
            else
                return Content("error");

            //保利威视端删除视频
            long timestamp = (DateTime.Now.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            string ptime = timestamp.ToString();

            string str = "ptime=" + ptime + "&vid=" + polyVid + secretkey;
            string hash = Encypt.GetSHA1(str, Encoding.UTF8);

            string url = "http://api.polyv.net/v2/video/" + userid + "/del-video?";
            url += "vid=" + polyVid;
            url += "&ptime=" + ptime;
            url += "&sign=" + hash;

            string jsonStr = RequestUrl.DoGetRequest(url);
            JObject json_ = (JObject)JsonConvert.DeserializeObject(jsonStr);

            string polyDeInfo = json_["status"].ToString().Trim();
            return Content(polyDeInfo);
        }


        public Video GetSingleVideo(string vid)
        {
            string format = "json";

            long timestamp = (DateTime.Now.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            string ptime = timestamp.ToString();

            string str = "format=" + format + "&ptime=" + ptime + "&vid=" + vid + secretkey;
            string hash = Encypt.GetSHA1(str, Encoding.UTF8);

            string url = "http://api.polyv.net/v2/video/" + userid + "/get-video-msg?";
            url += "format=" + format;
            url += "&ptime=" + ptime;
            url += "&vid=" + vid;
            url += "&sign=" + hash;

            string jsonStr = RequestUrl.DoGetRequest(url);
            JObject json_ = (JObject)JsonConvert.DeserializeObject(jsonStr);

            Video video_ = new Video();
            string s = json_["status"].ToString();
            video_.polyVid = vid;
            video_.VideoName = json_["data"][0]["title"].ToString();
            video_.coverImage = "http://img.videocc.net/uimage/2/" + json_["data"][0]["images"][0].ToString();

            return video_;
        }
    }
}