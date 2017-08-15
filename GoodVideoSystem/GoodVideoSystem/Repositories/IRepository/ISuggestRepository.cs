using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RefactorVideoSystem.Models.Models;
using System.Linq.Expressions;

namespace GoodVideoSystem.Repositories.IRepository
{
    public interface ISuggestRepository
    {
        void addSuggest(Suggest suggest);
        IEnumerable<Suggest> getSuggests(Expression<Func<Suggest, bool>> filter, out int recordcount); //获得当前suggest
    }
}
