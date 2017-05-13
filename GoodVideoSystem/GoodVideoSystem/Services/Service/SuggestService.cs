using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Services.Service
{
    public class SuggestService : BaseService, ISuggestService
    {
        private ISuggestRepository suggestRepository;

        public SuggestService(ISuggestRepository suggestRepository)
        {
            this.suggestRepository = suggestRepository;
            this.AddDisposableObject(suggestRepository);
        }
    }
}