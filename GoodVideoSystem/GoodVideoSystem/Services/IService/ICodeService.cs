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
        IEnumerable<Code> getInviteCodes(string deviceUniqueCode);
        string checkInviteCode(string inviteCode, out Code code);
        void updateInviteCodeInfo(Code inviteCode, string deviceUniqueCode);
        IEnumerable<Code> getInviteCodesContainsCode(string inviteCode, int vid, int pageIndex, int pageSize);
        IEnumerable<Code> getInviteCodesByStatus(int status, int vid, int pageIndex, int pageSize);
        IEnumerable<Code> getInviteCodesByStatus(int status, int vid);
        IEnumerable<Code> getInviteCodesContainsCode(string inviteCode, int vid);
        void getCounts(int vid, out int codeCount, out int codeCountNotExport, out int codeCountNotUsed, out int codeCountUsed);
        Code getInviteCodeById(int id);
        void updateInviteCode(Code code);
        void deleteInviteCode(Code code);
        void deleteInviteCode(string codeStr);
        void addInviteCode(Code code);
        IEnumerable<Code> getAllInviteCodes();
    }
}
