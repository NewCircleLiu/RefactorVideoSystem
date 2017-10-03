using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace GoodVideoSystem.Models.Utility
{
    public class RequestUrl
    {
        public static string DoGetRequest(string url)
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

        public static JObject jsonGetResult(String jsonStr)
        {
            JObject json_ = (JObject)JsonConvert.DeserializeObject(jsonStr);
            return  json_;
            
            //string zone = jo["zone"].ToString();
            //string zone_en = jo["zone_en"].ToString();
        }
    }
}