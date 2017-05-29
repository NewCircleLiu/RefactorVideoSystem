using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodVideoSystem.Services.IService
{
    public interface ICodeService
    {
        IEnumerable<Code> getCodes(string deviceCode);
        string checkCode(string inviteCode, out Code code);
        void updateCodeInfo(Code inviteCode, string deviceUniqueCode);
    }
}
