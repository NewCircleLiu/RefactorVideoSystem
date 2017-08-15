using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using GoodVideoSystem.Models.Abstract;


namespace GoodVideoSystem.Models.Concrete
{
    public class UploadFile : IUploadFile
    {
        public UploadInfo UploadImage(HttpPostedFileBase file, string saveLocal)
        {
            UploadInfo info = new UploadInfo();

            //文件保存路径
            string saveUrl = saveLocal;
            //文件名称
            string imageName = "";
            //文件后缀
            string imageExt = "";
            //文件类型
            string[] fileTypes = { "jpeg", "jpg", "gif", "png", "bmp" };
            //文件大小限制5M
            int fileSize = 3 * 1024 * 1024;

            //判断文件是否为空
            if (file == null)
            {
                info.statuCode = 400;
                info.info = "文件为空";
                return info;
            }

            //判断文件类型
            imageExt = getFileSuffix(file.FileName);
            if (!fileTypes.Contains(imageExt))
            {
                info.statuCode = 401;
                info.info = "文件格式不支持";
                return info;
            }

            //判断文件大小
            if (file.ContentLength > fileSize)
            {
                info.statuCode = 403;
                info.info = "文件大小超过限制";
                return info;
            }

            //生成文件名称
            imageName = createImageName();

            //判断路径是否存在
            if (!Directory.Exists(saveUrl))
            {
                //如果不存在则生成目录
                Directory.CreateDirectory(saveUrl);
            }
            //保存文件
            file.SaveAs(saveUrl + imageName + "." + imageExt);
            info.statuCode = 200;
            info.info = "/UploadFiles/VideoImages/" + imageName + "." + imageExt;
            return info;
        }

        //上传视频
        public UploadInfo UploadVideo(HttpPostedFileBase file, string saveLocal, int chunk, int chunks)
        {
            UploadInfo info = new UploadInfo();
            string videoName = null;
            string fileSuffix = null;

            try
            {
                fileSuffix = getFileSuffix(file.FileName);

                //文件没有分块，直接保存
                if (chunk == 0 && chunks == 0)
                {
                    videoName = createImageName();
                    file.SaveAs(saveLocal + videoName + "." + fileSuffix);
                    info.statuCode = 200;
                    info.info = "/UploadFiles/Videos/" + videoName + "." + fileSuffix;
                }
                else
                {//文件分块了
                    string[] directory = Directory.GetDirectories(saveLocal);
                    file.SaveAs(directory[0] + "/" + chunk + "." + fileSuffix);
                    info.statuCode = 200;
                    info.info = "";
                }

                file.InputStream.Flush();
                file.InputStream.Close();
                file.InputStream.Dispose();
            }
            catch (Exception)
            {
            }
            return info;
        }

        //合并文件
        public string CombineFile(string[] blockFileName, string saveLocal)
        {
            string videoName = createImageName();
            string fileSuffix = getFileSuffix(blockFileName[0]);

            FileStream addFile = new FileStream(saveLocal + videoName + "." + fileSuffix, FileMode.Append, FileAccess.Write);
            BinaryWriter addWriter = new BinaryWriter(addFile);
            FileStream fs = null;

            int blockNum = blockFileName.Length;
            for (int i = 0; i < blockNum; i++)
            {
                //读取分块文件字节流
                fs = new FileStream(blockFileName[i], FileMode.Open, FileAccess.Read);
                byte[] infbytes = new byte[(int)fs.Length];
                fs.Read(infbytes, 0, infbytes.Length);

                //合并分块文件
                addWriter.Write(infbytes);
                addWriter.Flush();
                fs.Flush();
                fs.Close();
                fs.Dispose();
            }

            addWriter.Flush();
            addFile.Flush();

            addWriter.Close();
            addFile.Close();

            addWriter.Dispose();
            addFile.Dispose();

            return "/UploadFiles/Videos/" + videoName + "." + fileSuffix;
        }

        //获取文件后缀
        private string getFileSuffix(string fileName)
        {
            string suffix = null;
            if (fileName.IndexOf('.') > 0)
            {
                string[] fs = fileName.Split('.');
                suffix = fs[fs.Length - 1];
            }
            return suffix;
        }

        //生成图片名称
        private string createImageName()
        {
            string imageName = "";
            string timeTicks = DateTime.Now.ToString("yyMMddHHmmssfff");
            Random seekRand = new Random();
            imageName += timeTicks;
            imageName += seekRand.Next(10000, 99999);
            return imageName;
        }
    }

    public class UploadInfo
    {
        public int statuCode;
        public string info;
    }
}