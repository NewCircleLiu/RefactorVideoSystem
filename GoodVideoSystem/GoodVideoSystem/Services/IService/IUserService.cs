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
        //根据设备信息处理用户
        void updateUserInfo(Code inviteCode, string deviceUniqueCode);

        //用户注册
        void registeUser(User user);

        //更新用户信息
        void updateUser(User user);
    }
}