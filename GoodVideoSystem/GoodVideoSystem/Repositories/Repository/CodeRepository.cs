using GoodVideoSystem.Repositories.IRepository;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Models.Repository
{
    public class CodeRepository : BaseRepository<Code>, ICodeRepository
    {
        public CodeRepository(BaseDbContext context) : base(context) { }

        public IEnumerable<Code> getCodes(string deviceCode)
        {
            return Get(item => item.DeviceUniqueCode.Contains(deviceCode));
        }

        public Code getCode(string inviteCode)
        {
            return  Get(item => item.CodeValue.Contains(inviteCode)).FirstOrDefault();
        }

        public void updateCode(Code code)
        {
            Update(code);
        }
    }
}