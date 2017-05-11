using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Services.Service
{
    public class ExceptionLogServcei : BaseService, IExceptionLogServcei
    {
        private IExceptionLogRepository exceptionLogRepository{get;set;}
        public ExceptionLogServcei(IExceptionLogRepository exceptionLogRepository)
        {
            this.exceptionLogRepository = exceptionLogRepository;
            this.AddDisposableObject(exceptionLogRepository);
        }
    }
}