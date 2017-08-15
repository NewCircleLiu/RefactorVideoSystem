using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodVideoSystem.Models.Abstract
{
    public interface IPaging
    {
        Object[] GetCurrentPageData(IEnumerable<Object> data, int page);
    }
}
