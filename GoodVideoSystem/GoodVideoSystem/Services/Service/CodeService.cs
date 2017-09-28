using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Services.IService;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Services.Service
{
    public class CodeService : BaseService, ICodeService
    {
        public readonly string INVALID = "INVALID";
        public readonly string AVAILABLE = "AVAILABLE";
        public readonly string OUTOFTIMES = "OUTOFTIMES";
        public readonly int MAX_DEVICE_COUNT = 3;

        private ICodeRepository codeRepository { get; set; }

        public CodeService(ICodeRepository codeRepository)
        {
            this.codeRepository = codeRepository;
            this.AddDisposableObject(codeRepository);
        }

        public IEnumerable<Code> getInviteCodes(string deviceUniqueCode)
        {
            return codeRepository.getInviteCodes(deviceUniqueCode);
        }

        public void addInviteCode(Code code)
        {
            codeRepository.addInviteCode(code);
        }

        public string checkInviteCode(string inviteCode, out Code code)
        {
            code = codeRepository.getInviteCode(inviteCode);
            if (code == null)
                return INVALID;
            if (code.BindedDeviceCount >= MAX_DEVICE_COUNT)
                return OUTOFTIMES;
            return AVAILABLE;
        }

        public void updateInviteCodeInfo(Code inviteCode, string deviceUniqueCode)
        {
            deviceUniqueCode = deviceUniqueCode.Trim();

            if (string.IsNullOrEmpty(deviceUniqueCode))
                return;

            bool isNewDevice = (codeRepository.getInviteCodes(deviceUniqueCode).FirstOrDefault() != null);

            if (isNewDevice)  //但凡用户切换到新的设备播放，需要绑定邀请码的硬件信息
            {
                if (inviteCode.BindedDeviceCount < MAX_DEVICE_COUNT)
                {
                    inviteCode.DeviceUniqueCode += ("," + deviceUniqueCode);
                    inviteCode.BindedDeviceCount = inviteCode.CodeValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Count();
                    codeRepository.updateInviteCode(inviteCode);
                }
            }
        }
        public IEnumerable<Code> getInviteCodesContainsCode(string inviteCode, int videoID, int pageIndex, int pageSize)
        {
            return codeRepository.getInviteCodes(inviteCode, videoID, pageIndex, pageSize, false);
        }
        public IEnumerable<Code> getInviteCodesContainsCode(string inviteCode, int videoID)
        {
            return codeRepository.getInviteCodes(inviteCode, videoID, false);
        }
        public IEnumerable<Code> getInviteCodesByStatus(int status, int videoID, int pageIndex, int pageSize)
        {
            return codeRepository.getInviteCodes(status, videoID, pageIndex, pageSize, true);
        }
        public IEnumerable<Code> getInviteCodesByStatus(int status, int videoID)
        {
            return codeRepository.getInviteCodes(status, videoID, true);
        }
        public void getCounts(int videoID, out int codeCount, out int codeCountNotExport, out int codeCountNotUsed, out int codeCountUsed)
        {
            codeRepository.getCounts(videoID, out codeCount, out codeCountNotExport, out codeCountNotUsed, out codeCountUsed);
        }

        public Code getInviteCodeById(int id)
        {
            return codeRepository.getInviteCodeById(id);
        }

        public void updateInviteCode(Code code)
        {
            codeRepository.updateInviteCode(code);
        }

        public void deleteInviteCode(Code code)
        {
            codeRepository.deleteInviteCode(code);
        }
    }
}