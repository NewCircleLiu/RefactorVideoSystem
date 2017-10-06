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
        IEnumerable<Code> getInviteCodes(Object tar, int vid, int pageIndex, int pageSize, bool isStatus);
        IEnumerable<Code> getInviteCodes(Object tar, int vid, bool isStatus);
        void getCounts(int vid, out int codeCount, out int codeCountNotExport, out int codeCountNotUsed, out int codeCountUsed);
        void updateInviteCode(Code code);
        void deleteInviteCode(Code code);
        IEnumerable<Code> getAllInviteCodes();

    }
}
