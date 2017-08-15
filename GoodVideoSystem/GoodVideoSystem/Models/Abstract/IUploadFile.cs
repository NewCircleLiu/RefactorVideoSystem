using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using GoodVideoSystem.Models.Concrete;

namespace GoodVideoSystem.Models.Abstract
{
    public interface IUploadFile
    {
        UploadInfo UploadImage(HttpPostedFileBase file, string saveLocal);
        UploadInfo UploadVideo(HttpPostedFileBase file, string saveLocal, int chunk, int chunks);
    }
}