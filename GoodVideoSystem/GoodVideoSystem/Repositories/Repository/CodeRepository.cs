using GoodVideoSystem.Repositories.IRepository;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Models.Repository
{
    public class CodeRepository : BaseRepository<Code>, ICodeRepository
    {
        public CodeRepository(BaseDbContext context) : base(context) { }


        //某设备的code
        public IEnumerable<Code> getCodes(string deviceUniqueCode)
        {
            return Get(item => item.DeviceUniqueCode.Contains(deviceUniqueCode));
        }


        public void addCode(Code code)
        {
            Add(code);
        }

        // 按照邀请码查找
        public Code getCode(string inviteCode)
        {
            return  Get(item => item.CodeValue.Contains(inviteCode)).FirstOrDefault();
        }

        //按照ID查找
        public Code getCodeById(int id)
        {
            return Get(item => item.CodeID == id).FirstOrDefault();
        }

        //分页版本
        public IEnumerable<Code> getCodes(Object tar, int videoID, int pageIndex, int pageSize, bool isStatus)
        {
            if (!isStatus) 
            {
                return Get(item => item.CodeValue.Contains((String)tar) && item.VideoID == videoID, pageIndex, pageSize, p => p.ModifyTime, true);
            }
            else
            {
                return Get(item => item.CodeStatus == (int)tar && item.VideoID == videoID, pageIndex, pageSize, p => p.ModifyTime, true);
            }
        }


        //不分页版本
        public IEnumerable<Code> getCodes(Object tar, int videoID, bool isStatus)
        {
            if (!isStatus)
            {
                if (videoID != -1)
                {
                    return Get(item => item.CodeValue.Contains((String)tar) && item.VideoID == videoID);
                }
                else
                {
                    return Get(item => item.CodeValue.Contains((String)tar));
                }
            }
            else
            {
                if (videoID != -1)
                {
                    return Get(item => item.CodeStatus == (int)tar && item.VideoID == videoID);
                }
                else
                {
                    return Get(item => item.CodeStatus == (int)tar);
                }
            }
        }
        public void getCounts(int videoID, out int codeCount, out int codeCountNotExport, out int codeCountNotUsed, out int codeCountUsed)
        {
            codeCount = Get(item => item.VideoID == videoID).Count();
            codeCountNotExport = Get(item => item.VideoID == videoID && item.CodeStatus == 0).Count();
            codeCountNotUsed = Get(item => item.VideoID == videoID && item.CodeStatus == 1).Count();
            codeCountUsed = Get(item => item.VideoID == videoID && item.CodeStatus == 2).Count();
        }
        public void updateCode(Code code)
        {
            Update(code);
        }
        public void deleteCode(Code code)
        {
            Delete(code);
        }
    }
}