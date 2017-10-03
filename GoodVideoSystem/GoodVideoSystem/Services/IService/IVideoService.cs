using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodVideoSystem.Services.IService
{
    public interface IVideoService
    {
        Video getVideo(int vid);
        IEnumerable<Code> getInviteCodes(int vid); //获得这个视频的所有code
        IEnumerable<Video> getVideosById(int vid);
        IEnumerable<Video> getVideosByName(string name);
        IEnumerable<Video> getVideos();
        int getVideoCount();
        void updateVideo(Video video);
        void addVideo(Video v);
        void deleteVideo(Video v);
    }
}
