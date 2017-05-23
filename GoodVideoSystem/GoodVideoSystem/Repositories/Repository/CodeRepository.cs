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

        public IEnumerable<Code> getCodes()
        {
            return this.Get();
        }

    }
}