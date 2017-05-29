using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodVideoSystem.Repositories.IRepository
{
    public interface IUserRepository
    {
        //获取用户列表
        IEnumerable<User> getUsers(int pageIndex,int pageSize,out int recordCount);

        //根据电话获取用户
        User getUserByPhone(string phone);

        //根据邀请码获取用户
        User getUserByInviteCode(string inviteCode);

        //添加用户
        void addUser(User user);

        //更新用户
        void updateUser(User user);
    }
}
