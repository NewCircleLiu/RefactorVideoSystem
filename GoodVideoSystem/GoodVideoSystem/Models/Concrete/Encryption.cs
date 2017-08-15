using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodVideoSystem.Models.Abstract;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace GoodVideoSystem.Models.Concrete
{
    public class Encryption : IEncryption
    {
        //MD5加密
        public string MyMD5(string data)
        {
            MD5 md5 = MD5.Create();
            byte[] dataByte = Encoding.Default.GetBytes(data);
            byte[] md5Byte = md5.ComputeHash(dataByte);
            string md5Data = byteToString(md5Byte);
            return md5Data;
        }

        //SHA1加密
        public string SHA1(string data)
        {
            HMACSHA1 aha1 = new HMACSHA1();
            byte[] dataByte = Encoding.Default.GetBytes(data);
            byte[] sha1DataByte = aha1.ComputeHash(dataByte);
            string sha1Data = byteToString(sha1DataByte);
            return sha1Data;
        }

        //SHA256
        public string SHA256(string data)
        {
            SHA256 sha256 = new SHA256Managed();
            byte[] dataByte = Encoding.Default.GetBytes(data);
            byte[] sha256Byte = sha256.ComputeHash(dataByte);
            string sha256Data = byteToString(sha256Byte);
            return sha256Data;
        }

        //SHA348
        public string SHA348(string data)
        {
            SHA384 sha348 = new SHA384Managed();
            byte[] dataByte = Encoding.Default.GetBytes(data);
            byte[] sha348Byte = sha348.ComputeHash(dataByte);
            string sha348Data = byteToString(sha348Byte);
            return sha348Data;
        }

        //SHA512
        public string SHA512(string data)
        {
            SHA512 sha512 = new SHA512Managed();
            byte[] dataByte = Encoding.Default.GetBytes(data);
            byte[] sha512Byte = sha512.ComputeHash(dataByte);
            string sha512Data = byteToString(sha512Byte);
            return sha512Data;
        }

        //byte to string
        public string byteToString(byte[] byteData)
        {
            string data = "";
            int length = byteData.Length;
            for (int i = 0; i < length; i++)
            {
                data += byteData[i].ToString("X2");
            }
            return data;
        }
    }
}