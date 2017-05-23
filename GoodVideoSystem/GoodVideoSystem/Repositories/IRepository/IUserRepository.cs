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

        //根据账号获取用户
        User getUserByAccount(string account);

        //添加用户
        void addUser(User user);
    }
}
