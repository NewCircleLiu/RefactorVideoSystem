using GoodVideoSystem.Repositories.IRepository;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace GoodVideoSystem.Models.Repository
{
    public class SuggestRepository : BaseRepository<Suggest>, ISuggestRepository
    {
        public SuggestRepository(BaseDbContext context) : base(context) { }

        public void addSuggest(Suggest suggest)
        {
            this.Add(suggest);
        }

        public IEnumerable<Suggest> getSuggests(Expression<Func<Suggest, bool>> filter, out int recordcount) //获得当前suggest
        {
            recordcount = this.DbSet.Count();
            return this.Get(filter);
        }
    }
}