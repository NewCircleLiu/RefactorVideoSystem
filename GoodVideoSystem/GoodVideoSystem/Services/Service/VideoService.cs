using GoodVideoSystem.Repositories.Repository;
using GoodVideoSystem.Services.IService;
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
    }
}