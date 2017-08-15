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
        void addCode(Code code);
        Code getCodeById(int id);
        IEnumerable<Code> getCodes(Object tar, int videoID, int pageIndex, int pageSize, bool isStatus);
        IEnumerable<Code> getCodes(Object tar, int videoID, bool isStatus);
        void getCounts(int videoID, out int codeCount, out int codeCountNotExport, out int codeCountNotUsed, out int codeCountUsed);
        void updateCode(Code code);
        void deleteCode(Code code);

    }
}
