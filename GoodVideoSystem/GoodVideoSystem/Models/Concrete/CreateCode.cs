using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodVideoSystem.Models.Abstract;
using System.Security.Cryptography;
using System.Text;
using RefactorVideoSystem.Models.Models;

namespace GoodVideoSystem.Models.Concrete
{
    public class CreateCode : ICreateCode
    {
        private Encryption encryption;

        public CreateCode(Encryption encryption)
        {
            this.encryption = encryption;
        }

        public List<string> createCode(int count, Video v)
        {
            Video video = v;
            List<string> codeList = new List<string>();
            string str = "";

            for (int i = 0; i < count; i++)
            {
                video.CodeCounts++;
                str = video.VideoName + video.vid + video.CodeCounts;
                codeList.Add(getInviteCode(str));
            }
            return codeList;
        }


        //获取邀请码
        public string getInviteCode(string str)
        {
            //md5加密
            string data = encryption.SHA1(str);
            //改变字母大小写
            byte[] dataByte = toLow(data);
            byte[] codeByte = new byte[10];
            string code = "";
            Random seekRand = new Random();

            for (int i = 0; i < 10; i++)
            {
                codeByte[i] = dataByte[seekRand.Next(32)];
            }

            code = Encoding.Default.GetString(codeByte);
            return code;
        }

        //md5序列大写字母随机转小写
        public byte[] toLow(string md5str)
        {
            byte[] data1 = Encoding.Default.GetBytes(md5str);
            Random r = new Random();
            int length = data1.Length;
            for (int i = 0; i < length; i++)
            {
                if (data1[i] >= 65 && data1[i] <= 90)
                {
                    if (r.Next(10) > 5)
                    {
                        data1[i] = (byte)(data1[i] + 32);
                    }
                }
            }
            return data1;
        }
    }
}