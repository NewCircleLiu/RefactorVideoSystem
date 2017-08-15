using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;


namespace GoodVideoSystem.Models.Abstract
{
    public interface IVerifyCode
    {

        //生成验证码
        string CreateValidateCode(int codeLength);
        //创建验证码的图片
        byte[] CreateValidateGraphic(string validateCode);



    }
}