using GoodVideoSystem.Repositories.IRepository;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Models.Repository
{
    public class ActionLogRepository : BaseRepository<ActionLog>, IActionLogRepository
    {
        public ActionLogRepository(BaseDbContext context) : base(context) { }
    }
}