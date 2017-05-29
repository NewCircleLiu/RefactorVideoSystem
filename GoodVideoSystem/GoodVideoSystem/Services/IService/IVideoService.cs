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
        Video getVideo(int videoID);
        IEnumerable<Code> getCodes(int videoID);
    }
}
