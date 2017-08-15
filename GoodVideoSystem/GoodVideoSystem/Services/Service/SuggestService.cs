using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RefactorVideoSystem.Models.Models;

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
        public void addSuggest(Suggest suggest)
        {
            suggestRepository.addSuggest(suggest);
        }

        public IEnumerable<Suggest> getSuggests(out int recordcount)
        {
            return suggestRepository.getSuggests(item => true, out recordcount);
        }
        public IEnumerable<Suggest> getSuggestsbByPhone(string phone, out int recordcount) //按页获取
        {
            return suggestRepository.getSuggests(item => item.UserPhone.Contains(phone), out recordcount);
        }
        public IEnumerable<Suggest> getSuggestsByText(string text, out int recordcount)
        {
            return suggestRepository.getSuggests(item => item.Text.Contains(text), out recordcount);
        }
    }
}