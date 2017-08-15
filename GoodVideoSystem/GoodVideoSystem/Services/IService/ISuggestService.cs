using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RefactorVideoSystem.Models.Models;
using Microsoft.Practices.ObjectBuilder2;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;

namespace GoodVideoSystem.Services.IService
{
    public interface ISuggestService
    {
        void addSuggest(Suggest suggest);
        IEnumerable<Suggest> getSuggestsbByPhone(string phone, out int recordcount);
        IEnumerable<Suggest> getSuggestsByText(string text, out int recordcount); //获得当前suggest
        IEnumerable<Suggest> getSuggests(out int recordcount);
    }
}
