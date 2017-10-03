using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodVideoSystem.Repositories.Repository
{
    public interface IVideoRepository
    {
        Video getVideo(string inviteCode);
        Video getVideo(int vid);
        IEnumerable<Video> getVideos(object tar, bool isID);
        void updateVideo(Video video);
        IEnumerable<Video> getVideos();
        void addVideo(Video v);
        void deleteVideo(Video v);
    }
}
