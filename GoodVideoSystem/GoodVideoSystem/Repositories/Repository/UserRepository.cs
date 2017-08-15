using GoodVideoSystem.Repositories.IRepository;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace GoodVideoSystem.Models.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BaseDbContext context) : base(context) { }

        //获取用户列表
        public IEnumerable<User> getUsers(Expression<Func<User, bool>> filter, out int recordCount)
        {
            recordCount = this.DbSet.Count();
            return this.Get(filter);
        }

        //根据电话获取用户
        public User getUserByPhone(string phone)
        {
            return this.Get(p => p.Phone == phone).FirstOrDefault();
        }

        //根据邀请码获取用户
        public User getUserByInviteCode(string inviteCode)
        {
            return this.Get(p => p.InviteCodes.Contains(inviteCode)).FirstOrDefault();
        }
        //根据id获取用户

        public User getUserById(int UserId)
        {
            return this.Get(p => p.UserId == UserId).FirstOrDefault();
        }
        //添加用户
        public void addUser(User user)
        {
            this.Add(user);
        }

        //更新用户
        public void updateUser(User user)
        {
            this.Update(user);
        }
    }
}