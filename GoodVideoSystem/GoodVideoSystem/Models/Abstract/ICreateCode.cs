using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RefactorVideoSystem.Models.Models;

namespace GoodVideoSystem.Models.Abstract
{
    public interface ICreateCode
    {
        List<string> createCode(int count, Video v);
    }
}
