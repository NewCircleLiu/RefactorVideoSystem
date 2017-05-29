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

        public Video getVideo(int videoID)
        {
            return videoRepository.getVideo(videoID);
        }

        IEnumerable<Code> getCodes(int videoID)
        {
            return get
        }
    }
}