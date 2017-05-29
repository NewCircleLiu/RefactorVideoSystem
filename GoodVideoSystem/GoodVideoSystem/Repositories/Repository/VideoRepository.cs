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

        public Video getVideo(string inviteCode)
        {
            CodeRepository codes = new CodeRepository(new BaseDbContext());
            Code code = codes.Get(item => item.CodeValue == inviteCode).FirstOrDefault();
            return code != null ? Get(item => item.VideoID == code.VideoID).FirstOrDefault() : null;
        }

        public Video getVideo(int videoID)
        {
           return  this.Get(item => item.VideoID == videoID).FirstOrDefault();
        }
    }
}