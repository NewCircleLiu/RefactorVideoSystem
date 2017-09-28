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
        IEnumerable<Code> getInviteCodes(string deviceUniqueCode);
        Code getInviteCode(string inviteCode);
        void addInviteCode(Code code);
        Code getInviteCodeById(int id);
        IEnumerable<Code> getInviteCodes(Object tar, int videoID, int pageIndex, int pageSize, bool isStatus);
        IEnumerable<Code> getInviteCodes(Object tar, int videoID, bool isStatus);
        void getCounts(int videoID, out int codeCount, out int codeCountNotExport, out int codeCountNotUsed, out int codeCountUsed);
        void updateInviteCode(Code code);
        void deleteInviteCode(Code code);

    }
}
