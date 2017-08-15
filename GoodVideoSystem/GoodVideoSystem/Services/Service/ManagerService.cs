using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RefactorVideoSystem.Models.Models;

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
        public string checkManager(string account, string password, out Manager currentManager)
        {
            currentManager = managerRepository.getManager(account, password);
            if (currentManager == null)
            {
                return "INVALID";
            }
            else{
                return "SUCCESS";
            }
        }

        public void editManager(Manager manager)
        {
            managerRepository.editManager(manager);
        }
    }
}