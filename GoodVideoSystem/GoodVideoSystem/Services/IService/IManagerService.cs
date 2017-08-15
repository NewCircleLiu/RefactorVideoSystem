using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Services.IService;
using RefactorVideoSystem.Models.Models;

namespace GoodVideoSystem.Services.IService
{
    public interface IManagerService
    {
        string checkManager(string account, string password, out Manager currentManager);
        void editManager(Manager manager);
    }
}
