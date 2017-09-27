using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GoodVideoSystem.Repository.IRepository
{
    interface IRepository<T>:IDisposable where T:class
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T,bool>> filter);
        IEnumerable<T> Get<TOrder>( Expression<Func<T, bool>> filter,
                                    int pageIndex, int pageSize, 
                                    Expression<Func<T, TOrder>> sortExpression,
                                    bool isAsc = true);
        int Count(Expression<Func<T, bool>> filter);
        void Add(T instance);
        void Update(T instance);
        void Delete(T instance);


    }
}
