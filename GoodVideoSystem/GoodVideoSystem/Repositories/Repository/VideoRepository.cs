using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodVideoSystem.Repositories.Repository;

namespace GoodVideoSystem.Models.Repository
{
    public class VideoRepository : BaseRepository<Video>, IVideoRepository
    {
        public VideoRepository(BaseDbContext context) : base(context) { }
    }
}