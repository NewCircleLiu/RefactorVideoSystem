using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodVideoSystem.Repositories.IRepository
{
    public interface ICodeRepository
    {
        IEnumerable<Code> getCodes(string deviceCode);
        Code getCode(string inviteCode);
        void updateCode(Code code);
    }
}
