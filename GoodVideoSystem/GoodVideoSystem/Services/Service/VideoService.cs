using GoodVideoSystem.Repositories.Repository;
using GoodVideoSystem.Services.IService;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Services.Service
{
    public class VideoService : BaseService, IVideoService
    {
        private IVideoRepository videoRepository;

        public VideoService(IVideoRepository videoRepository)
        {
            this.videoRepository = videoRepository;
            this.AddDisposableObject(videoRepository);
        }

        public Video getVideo(int vid)
        {
            return videoRepository.getVideo(vid);
        }
        public IEnumerable<Code> getInviteCodes(int vid) 
        {
            return videoRepository.getVideo(vid).Code;
        }
        public IEnumerable<Video> getVideosById(int vid)
        {
            if (vid != -1)
            {
                return videoRepository.getVideos(vid, true);
            }
            else
            {
                return videoRepository.getVideos();
            }
            
        }
        public IEnumerable<Video> getVideosByName(string name)
        {
            return videoRepository.getVideos(name, false);
        }
        public IEnumerable<Video> getVideos()
        {
            return videoRepository.getVideos();
        }
        public int getVideoCount()
        {
            return videoRepository.getVideos().Count();
        }
        public void updateVideo(Video video)
        {
            videoRepository.updateVideo(video);
        }
        public void addVideo(Video v)
        {
            videoRepository.addVideo(v);
        }
        public void deleteVideo(Video v)
        {
            videoRepository.deleteVideo(v);
        }
    }
}