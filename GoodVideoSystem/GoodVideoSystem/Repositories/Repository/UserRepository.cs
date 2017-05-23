using GoodVideoSystem.Repositories.IRepository;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Models.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BaseDbContext context) : base(context) { }

        //获取用户列表
        public IEnumerable<User> getUsers(int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = this.DbSet.Count();
            return this.Get(p=>true,pageIndex,pageSize,p=>p.ModifyTime,true);
        }

        //根据电话获取用户
        public User getUserByPhone(string phone)
        {
            return this.Get(p => p.Phone == phone).FirstOrDefault();
        }

        //根据账号获取用户
        public User getUserByAccount(string account)
        {
            return this.Get(p => p.UserUniqueCode == account).FirstOrDefault();
        }

        //添加用户
        public void addUser(User user)
        {
            this.Add(user);
        }
    }
}