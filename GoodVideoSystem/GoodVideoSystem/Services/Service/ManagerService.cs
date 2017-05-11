using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Services.Service
{
    public class ManagerService : BaseService, IManagerService
    {
        private IManagerRepository managerRepository{get;set;}
        public ManagerService(IManagerRepository managerRepository)
        {
            this.managerRepository = managerRepository;
            this.AddDisposableObject(managerRepository);
        }
    }
}