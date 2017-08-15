using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RefactorVideoSystem.Models;
using GoodVideoSystem.Services.filters;
using GoodVideoSystem.Models.Concrete;
using System.Web.Script.Serialization;
using System.IO;


namespace GoodVideoSystem.Controllers.Back
{
    [ManagerAuthorize]
    public class UploadController : Controller
    {
        //
        // GET: /Upload/
        private UploadFile uf;


        public UploadController(UploadFile uf)
        {
            this.uf = uf;
        }
        public ActionResult Index()
        {
            return Content("ok");
        }



        //上传图片
        [HttpPost]
        public ContentResult UploadImage()
        {
            HttpPostedFileBase file = Request.Files[0];
            string saveUrl = Server.MapPath("/") + "UploadFiles/VideoImages/";

            UploadInfo info = uf.UploadImage(file, saveUrl);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonStr = serializer.Serialize(info);


            return Content(jsonStr);
        }


        //上传视频
        [HttpPost]
        public ActionResult UploadVideo(int chunk = 0, int chunks = 0)
        {
            string saveUrl = Server.MapPath("/") + "UploadFiles/Videos/";

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            UploadInfo info = new UploadInfo();
            //string jsonStr = serializer.Serialize(info);

            //ajax请求，判断文件是否上传，文件断点续传
            if (Request.IsAjaxRequest())
            {
                //请求类型
                string type = Request.Form["type"];
                //上传整个文件之前，检查文件是否上传过，
                if (type == "init")
                {
                    //获取文件校验码
                    string guid = Request.Form["guid"];
                    //从数据库中查找文件的md5值
                    if (Directory.Exists(saveUrl + guid))   //文件夹存在或文件已上传上传
                    {
                        info.statuCode = 200;
                        info.info = "true";
                        return Content(serializer.Serialize(info));
                    }
                    //文件未上传，创建临时保存分块的文件夹
                    Directory.CreateDirectory(saveUrl + guid);

                    info.statuCode = 200;
                    info.info = "false";
                    return Content(serializer.Serialize(info));
                }
                //上传每个分块之前
                if (type == "block")
                {
                    //当前分块的大小
                    int currentBlockSize = Convert.ToInt32(Request.Form["currentBlockSize"]);

                    //判断当前分块是否已上传完成或者是否已上传
                    string[] tempDirectory = Directory.GetDirectories(saveUrl);
                    bool is_exists = System.IO.File.Exists(tempDirectory[0] + chunk);
                    FileInfo fi = new FileInfo(tempDirectory[0] + chunk);
                    if (is_exists && fi.Length == currentBlockSize)
                    {
                        info.statuCode = 200;
                        info.info = "true";
                        return Content(serializer.Serialize(info));
                    }
                    //如果分块上传了部分，则删除分块并重新上传
                    if (is_exists && fi.Length < currentBlockSize)
                    {
                        System.IO.File.Delete(tempDirectory[0] + chunk);
                    }
                    info.statuCode = 200;
                    info.info = "false";
                    return Content(serializer.Serialize(info));
                }
                //整个文件上传完成后，合并所有分块
                if (type == "merge")
                {
                    string[] tempDirectory = Directory.GetDirectories(saveUrl);
                    string[] blockFileName = Directory.GetFiles(tempDirectory[0] + "/");
                    string videoLocal = uf.CombineFile(blockFileName, saveUrl);

                    //合并完成，返回视频路径
                    info.statuCode = 200;
                    info.info = videoLocal;
                    return Content(serializer.Serialize(info));
                }
            }

            HttpPostedFileBase file = null;
            try
            {
                file = Request.Files[0];
            }
            catch (Exception)
            {
            }

            //上传文件
            info = uf.UploadVideo(file, saveUrl, chunk, chunks);
            //文件没有分块时返回保存的文件路径
            return Content(serializer.Serialize(info));
        }
    }
}
