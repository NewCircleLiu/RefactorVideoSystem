using GoodVideoSystem.Models.VO;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodVideoSystem.Services.Service
{
    public interface IUserService
    {
        //用户登录
        User userLogin(User user);

        //用户注册
        User userRregister(User user);
    }
}
