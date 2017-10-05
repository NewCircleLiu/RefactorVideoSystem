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

        //根据id或许用户


        //用户注册
        void registeUser(User user);

        //更新用户信息
        void updateUser(User user);

        //IEnumerable<User> getUsers(int page_id, int pageSize, out int recordCount);
        IEnumerable<User> getUsersByPhone(string phone, out int recordCount);
        IEnumerable<User> getUsers(out int recordCount);

        //获取当前用户
        User getUserByDevice(string deviceUniqueCode);

        //根据id获取用户
        User getUserById(int UserId);

        //根据设备标识和邀请码判断当前用户是否存在
        bool IsCurrentUserExist(string deviceUniqueCode, Code inputCode);

        //根据邀请码获取用户
        User getUserByInviteCode(Code inviteCode);
    }
}