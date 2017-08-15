using GoodVideoSystem.Repositories.IRepository;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Models.Repository
{
    public class ManagerRepository : BaseRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(BaseDbContext context) : base(context) { }
        public Manager getManager(string account, string password)
        {
            return Get(item => item.Account == account && item.Password == password).FirstOrDefault();
        }
        public void editManager(Manager manager)
        {
            this.Update(manager);
        }
    }
}