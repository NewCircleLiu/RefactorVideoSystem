using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoodVideoSystem.Models.Repository
{
    public class BaseRepository
    {
        public BaseDbContext db = new BaseDbContext();
        public ModelStateDictionary ModelState { get; }
    }
}