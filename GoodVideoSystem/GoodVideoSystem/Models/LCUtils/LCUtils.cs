using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Script.Serialization;

namespace GoodVideoSystem.Models.LCUtils
{

    public struct jsonout
    {
        public string code { get; set; }
        public string message { get; set; }
        public string total { get; set; }
        public Object data;
    }

    public class LCUtils
    {
        private string userUnique = "ievel39qfn";
        private string secretKey = "400c0826066c64f8a3b8c64d55342ea1";

        private string apiUrl = "http://api.letvcloud.com/open.php";
        public string format = "json";
        public string apiVersion = "2.0";

        //封装基本的参数
        public string baseParam(Dictionary<string,string> args)
        {
            //生成时间戳
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970,1,1,0,0,0,0);
            string timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();

            args.Add("timestamp", timestamp);
            args.Add("user_unique", userUnique);
            args.Add("ver", apiVersion);
            args.Add("format", format);

            //生成sign
            List<string> keyList = new List<string>(args.Keys);
            keyList.Sort();

            string urlParam = "";
            string keyStr = "";
            for (int i = 0; i < keyList.Count; i++ )
            {
                string key = keyList[i];
                urlParam += (String.IsNullOrEmpty(urlParam) ? "?" : "&") + key + "=" + HttpUtility.UrlEncode(args[key]);
                keyStr += key + args[key];
            }
            keyStr += secretKey;

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(keyStr));
            string signStr = BitConverter.ToString(output).Replace("-", String.Empty).ToLower();

            urlParam += "&sign=" + signStr;
            String resUrl = apiUrl + urlParam;

            return resUrl;
        }

        //发送请求
        public string doRequest(String url)
        {
            Uri uri = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var mstream = new MemoryStream())
            {
                responseStream.CopyTo(mstream);
                return System.Text.Encoding.UTF8.GetString(mstream.ToArray());
            }
        }

        //
        private jsonout jsonGetResult(String src)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            jsonout json = serializer.Deserialize<jsonout>(src);
            return json;
        }

        //删除视频 video_id fileID
        public jsonout deleteVideo(string video_id)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("api", "video.del");
            args.Add("video_id", video_id+"");

            string url = baseParam(args);
            string result = doRequest(url);

            jsonout re = jsonGetResult(result);

            return re;
        }

    }
}