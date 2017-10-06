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
            return code != null ? Get(item => item.vid == code.vid).FirstOrDefault() : null;
        }

        public Video getVideo(int vid)
        {
           return  this.Get(item => item.vid == vid).FirstOrDefault();
        }

        public Video getVideoByPolyvid(string polyvid)
        {
           return this.Get(item => item.polyVid.Equals(polyvid)).FirstOrDefault();
        }

        public IEnumerable<Video> getVideos(object tar, bool isID)
        {
            if (isID)
            {
                return this.Get(item => item.vid == (int)tar);
            }
            else
            {
                return this.Get(item => item.VideoName.Contains((string)tar));
            }
        }
        public IEnumerable<Video> getVideos() //获得所有video
        {
            return Get(item => true);
        }

        public void updateVideo(Video video)
        {
            Update(video);
        }
        public void addVideo(Video v)
        {
            Add(v);
        }
        public void deleteVideo(Video v)
        {
            Delete(v);
        }
    }
}