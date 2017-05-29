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
    }
}