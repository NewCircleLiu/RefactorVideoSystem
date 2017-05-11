using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Services.Service
{
    public class CodeService : BaseService, ICodeService
    {
        private ICodeRepository codeRepository{get; set;}
        public CodeService(ICodeRepository codeRepository)
        {
            this.codeRepository = codeRepository;
            this.AddDisposableObject(codeRepository);
        }
    }
}