using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Services.Service
{
    public class ActionLogService : BaseService, IActionLogService
    {
        public IActionLogRepository actionLogRepository{get;set;}
        public ActionLogService(IActionLogRepository actionLogRepository)
        {
            this.actionLogRepository = actionLogRepository;
            this.AddDisposableObject(actionLogRepository);
        }
    }
}