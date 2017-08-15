using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodVideoSystem.Services.filters;
using GoodVideoSystem.Services.IService;
using GoodVideoSystem.Services.Service;
using RefactorVideoSystem.Models.Models;

namespace GoodVideoSystem.Repositories.IRepository
{
    public interface IManagerRepository
    {
        Manager getManager(string account, string password); //获得账号为account，密码为password的manager用户
        void editManager(Manager manager);
    }
}
