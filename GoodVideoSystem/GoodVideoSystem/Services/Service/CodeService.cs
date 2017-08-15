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

        public IEnumerable<Code> getCodes(string deviceCode)
        {
            return codeRepository.getCodes(deviceCode);
        }

        public void addCode(Code code)
        {
            codeRepository.addCode(code);
        }

        public string checkCode(string inviteCode, out Code code)
        {
            code = codeRepository.getCode(inviteCode);
            if (code == null)
                return INVALID;
            if (code.BindedDeviceCount >= MAX_DEVICE_COUNT)
                return OUTOFTIMES;
            return AVAILABLE;
        }

        public void updateCodeInfo(Code inviteCode, string deviceUniqueCode)
        {
            deviceUniqueCode = deviceUniqueCode.Trim();

            if (string.IsNullOrEmpty(deviceUniqueCode))
                return;

            bool isNewDevice = (codeRepository.getCodes(deviceUniqueCode).FirstOrDefault() != null);

            if (isNewDevice)  //但凡用户切换到新的设备播放，需要绑定邀请码的硬件信息
            {
                if (inviteCode.BindedDeviceCount < MAX_DEVICE_COUNT)
                {
                    inviteCode.DeviceUniqueCode += ("," + deviceUniqueCode);
                    inviteCode.BindedDeviceCount = inviteCode.CodeValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Count();
                    codeRepository.updateCode(inviteCode);
                }
            }
        }
        public IEnumerable<Code> getCodesContainsCode(string inviteCode, int videoID, int pageIndex, int pageSize)
        {
            return codeRepository.getCodes(inviteCode, videoID, pageIndex, pageSize, false);
        }
        public IEnumerable<Code> getCodesContainsCode(string inviteCode, int videoID)
        {
            return codeRepository.getCodes(inviteCode, videoID, false);
        }
        public IEnumerable<Code> getCodesByStatus(int status, int videoID, int pageIndex, int pageSize)
        {
            return codeRepository.getCodes(status, videoID, pageIndex, pageSize, true);
        }
        public IEnumerable<Code> getCodesByStatus(int status, int videoID)
        {
            return codeRepository.getCodes(status, videoID, true);
        }
        public void getCounts(int videoID, out int codeCount, out int codeCountNotExport, out int codeCountNotUsed, out int codeCountUsed)
        {
            codeRepository.getCounts(videoID, out codeCount, out codeCountNotExport, out codeCountNotUsed, out codeCountUsed);
        }

        public Code getCodeById(int id)
        {
            return codeRepository.getCodeById(id);
        }

        public void updateCode(Code code)
        {
            codeRepository.updateCode(code);
        }

        public void deleteCode(Code code)
        {
            codeRepository.deleteCode(code);
        }
    }
}