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
        public IEnumerable<Code> getInviteCodes(string deviceUniqueCode)
        {
            return Get(item => item.DeviceUniqueCode.Contains(deviceUniqueCode));
        }
        //根据用户的Id来获取视频邀请码
        public IEnumerable<Code> getInviteCodesByUserId(int userid)
        {
            return Get(item => item.UserID == userid);
        }

        public void addInviteCode(Code code)
        {
            Add(code);
        }

        // 按照邀请码查找
        public Code getInviteCode(string inviteCode)
        {
            return  Get(item => item.CodeValue.Contains(inviteCode)).FirstOrDefault();
        }

        //按照ID查找
        public Code getInviteCodeById(int id)
        {
            return Get(item => item.CodeID == id).FirstOrDefault();
        }

        //分页版本
        public IEnumerable<Code> getInviteCodes(Object tar, int vid, int pageIndex, int pageSize, bool isStatus)
        {
            if (!isStatus) 
            {
                return Get(item => item.CodeValue.Contains((String)tar) && item.vid == vid, pageIndex, pageSize, p => p.ModifyTime, true);
            }
            else
            {
                return Get(item => item.CodeStatus == (int)tar && item.vid == vid, pageIndex, pageSize, p => p.ModifyTime, true);
            }
        }


        //不分页版本
        public IEnumerable<Code> getInviteCodes(Object tar, int vid, bool isStatus)
        {
            if (!isStatus)
            {
                if (vid != -1)
                {
                    return Get(item => item.CodeValue.Contains((String)tar) && item.vid == vid);
                }
                else
                {
                    return Get(item => item.CodeValue.Contains((String)tar));
                }
            }
            else
            {
                if (vid != -1)
                {
                    return Get(item => item.CodeStatus == (int)tar && item.vid == vid);
                }
                else
                {
                    return Get(item => item.CodeStatus == (int)tar);
                }
            }
        }
        public void getCounts(int vid, out int codeCount, out int codeCountNotExport, out int codeCountNotUsed, out int codeCountUsed)
        {
            codeCount = Get(item => item.vid == vid).Count();
            codeCountNotExport = Get(item => item.vid == vid && item.CodeStatus == 0).Count();
            codeCountNotUsed = Get(item => item.vid == vid && item.CodeStatus == 1).Count();
            codeCountUsed = Get(item => item.vid == vid && item.CodeStatus == 2).Count();
        }
        public void updateInviteCode(Code code)
        {
            Update(code);
        }

        public void deleteInviteCode(Code code)
        {
            Delete(code);
        }

        public IEnumerable<Code> getAllInviteCodes()
        {
            return Get();
        }
    }
}