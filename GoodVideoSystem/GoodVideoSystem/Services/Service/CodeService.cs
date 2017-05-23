using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Services.IService;
using RefactorVideoSystem.Models.Models;
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

        public IEnumerable<Code> getCodes()
        {
            return codeRepository.getCodes(); ;
        }
    }
}