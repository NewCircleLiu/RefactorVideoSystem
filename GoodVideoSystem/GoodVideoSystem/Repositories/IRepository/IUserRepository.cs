using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace GoodVideoSystem.Repositories.IRepository
{
    public interface IUserRepository
    {
        //获取用户列表
        IEnumerable<User> getUsers(Expression<Func<User, bool>> filter, out int recordCount);

        //根据电话获取用户
        User getUserByPhone(string phone);

        //根据邀请码获取用户
        User getUserByInviteCode(string inviteCode);

        //根据id获取用户
        User getUserById(int UserId);

        //添加用户
        void addUser(User user);

        //更新用户
        void updateUser(User user);
    }
}
