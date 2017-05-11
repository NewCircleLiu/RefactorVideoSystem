using GoodVideoSystem.Repositories.IRepository;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Models.Repository
{
    public class SuggestRepository : BaseRepository<Suggest>, ISuggestRepository
    {
        public SuggestRepository(BaseDbContext context) : base(context) { }
    }
}