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
        IEnumerable<Code> getCodesContainsCode(string inviteCode, int videoID, int pageIndex, int pageSize);
        IEnumerable<Code> getCodesByStatus(int status, int videoID, int pageIndex, int pageSize);
        IEnumerable<Code> getCodesByStatus(int status, int videoID);
        IEnumerable<Code> getCodesContainsCode(string inviteCode, int videoID);
        void getCounts(int videoID, out int codeCount, out int codeCountNotExport, out int codeCountNotUsed, out int codeCountUsed);
        Code getCodeById(int id);
        void updateCode(Code code);
        void deleteCode(Code code);
        void addCode(Code code);
    }
}
