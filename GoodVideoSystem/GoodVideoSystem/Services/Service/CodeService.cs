﻿using GoodVideoSystem.Repositories.IRepository;
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
        public readonly string UNACTIVE = "UNACTIVE";
        public readonly string AVAILABLE = "AVAILABLE";
        public readonly string OUTOFTIMES = "OUTOFTIMES";
        public readonly int MAX_DEVICE_COUNT = 3;
        public readonly int UNACTIVE_ = 0;
        public readonly int ACTIVE_ = 1;
        public readonly int USED_ = 2;


        private ICodeRepository codeRepository { get; set; }
        private IVideoService videoService { get; set; }

        public CodeService(ICodeRepository codeRepository, IVideoService videoService)
        {
            this.codeRepository = codeRepository;
            this.videoService = videoService;
            this.AddDisposableObject(codeRepository);
            this.AddDisposableObject(videoService);
        }

        public IEnumerable<Code> getInviteCodes(string deviceUniqueCode)
        {
            return codeRepository.getInviteCodes(deviceUniqueCode);
        }
        public IEnumerable<Code> getInviteCodes(int userid)
        {
            return codeRepository.getInviteCodesByUserId(userid);
        }

        public void addInviteCode(Code code)
        {
            codeRepository.addInviteCode(code);
        }

        public string checkInviteCode(string inviteCode, out Code code)
        {
            code = codeRepository.getInviteCode(inviteCode);
            if (code == null)  //邀请码不存在
                return INVALID;
            if (code.CodeStatus == UNACTIVE_) //邀请码未激活
                return UNACTIVE;
            if (code.BindedDeviceCount >= MAX_DEVICE_COUNT) //邀请码登录设备超过3次
                return OUTOFTIMES;
            return AVAILABLE;
        }

        public void updateInviteCodeInfo(Code inviteCode, string userId)
        {
            userId = userId.Trim();
            if (string.IsNullOrEmpty(userId))
            {
                return;
            }
            if (inviteCode.UserID != null)
            {
                inviteCode.CodeStatus = USED_;
                inviteCode.UserID = int.Parse(userId);
                codeRepository.updateInviteCode(inviteCode);
            }
        }

        public IEnumerable<Code> getInviteCodesContainsCode(string inviteCode, int vid, int pageIndex, int pageSize)
        {
            return codeRepository.getInviteCodes(inviteCode, vid, pageIndex, pageSize, false);
        }

        public IEnumerable<Code> getInviteCodesContainsCode(string inviteCode, int vid)
        {
            return codeRepository.getInviteCodes(inviteCode, vid, false);
        }

        public IEnumerable<Code> getInviteCodesByStatus(int status, int vid, int pageIndex, int pageSize)
        {
            return codeRepository.getInviteCodes(status, vid, pageIndex, pageSize, true);
        }

        public IEnumerable<Code> getInviteCodesByStatus(int status, int vid)
        {
            return codeRepository.getInviteCodes(status, vid, true);
        }

        public void getCounts(int vid, out int codeCount, out int codeCountNotExport, out int codeCountNotUsed, out int codeCountUsed)
        {
            codeRepository.getCounts(vid, out codeCount, out codeCountNotExport, out codeCountNotUsed, out codeCountUsed);
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
            if (code == null)
                return;

            //修改视频邀请码数量
            Video video = videoService.getVideo(code.vid);
            if (video == null)
                return;

            int codeCounts, codesNotUsed, codesNotExport, codesUsed;
            getCounts(video.vid, out codeCounts, out codesNotExport, out codesNotUsed, out codesUsed);
            video.CodeNotUsed = codesNotUsed;
            video.CodeUsed = codesUsed;
            video.CodeCounts = codeCounts - 1;

            videoService.updateVideo(video);
            codeRepository.deleteInviteCode(code);
        }

        public void deleteInviteCode(string codeStr)
        {
            Code code = codeRepository.getInviteCode(codeStr.Trim());
            if(code != null)
                codeRepository.deleteInviteCode(code);
        }

        public IEnumerable<Code> getAllInviteCodes()
        {
            return codeRepository.getAllInviteCodes();
        }
    }
}