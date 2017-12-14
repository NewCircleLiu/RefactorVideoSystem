using GoodVideoSystem.Models.VO;
using GoodVideoSystem.Repositories.IRepository;
using GoodVideoSystem.Services.IService;
using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodVideoSystem.Services.Service
{
    public class UserService : BaseService, IUserService
    {
        private IUserRepository userRepository { get; set; }
        private ICodeRepository codeRepository { get; set; }
        private IVideoService videoService { get; set; }

        public UserService(IUserRepository userRepository, ICodeRepository codeRepository, IVideoService videoService)
        {
            this.userRepository = userRepository;
            this.AddDisposableObject(userRepository);

            this.codeRepository = codeRepository;
            this.AddDisposableObject(codeRepository);

            this.videoService = videoService;
            this.AddDisposableObject(videoService);
        }

        public void updateUserInfo(Code inviteCode, string UserId)
        {
            //保证当前用户已经创建，在User/RegisterUser接口中已经
            //deviceUniqueCode = deviceUniqueCode.Trim();
            UserId = UserId.Trim();
            if (!string.IsNullOrEmpty(UserId))
            {
                //1.获取当前用户（从邀请码和设备信息获取）
                User currentUser = getUserByInviteCode(inviteCode);
                if (currentUser == null)
                {
                    currentUser = getUserById(int.Parse(UserId));
                }
                //2.1 如果获取到用户，则将当前邀请码添加到当前用户
                if (currentUser != null)
                {
                    if (!currentUser.InviteCodes.Contains(inviteCode.CodeValue))
                    {
                        currentUser.InviteCodes += ("," + inviteCode.CodeValue);
                        updateUser(currentUser);
                    }
                }
            }
        }

        public void deleteInviteCode(Code inviteCode)
        {
            User currentUser = getUserByInviteCode(inviteCode);
            if (currentUser == null)
                return;

            string toDelInviteCodeStr = inviteCode.CodeValue.Trim();
            string[] splitCodes = currentUser.InviteCodes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string updatedInviteCodes = "";
            for (int i = 0; i < splitCodes.Count(); i++)
            {
                string currStr = splitCodes[i].Trim();
                if(!currStr.Equals(toDelInviteCodeStr))
                    updatedInviteCodes += "," + currStr;
            }
            currentUser.InviteCodes = updatedInviteCodes;
            updateUser(currentUser);
        }

        public bool deleteUser(int userID)
        {
            User user = userRepository.getUserById(userID);

            if(user == null)
                return false;

            string[] splitCodes = user.InviteCodes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < splitCodes.Count(); i++)
            {
                Code currCode = codeRepository.getInviteCode(splitCodes[i].Trim());
                if (currCode != null)
                {
                    int vid = currCode.vid;
                    Video currVideo = videoService.getVideo(vid);

                    if (currVideo != null)
                    {
                        currVideo.CodeCounts--;
                        videoService.updateVideo(currVideo);
                    }

                    codeRepository.deleteInviteCode(currCode);
                }
            }

            userRepository.deleteUser(user);
            return true;
        }


        //用户注册
        public void registeUser(User user)
        {
            userRepository.addUser(user);
        }

        //更新用户信息
        public void updateUser(User user)
        {
            userRepository.updateUser(user);
        }
        /*       
        public IEnumerable<User> getUsers(int page_id,int pageSize,out int recordCount)
        {
            return userRepository.getUsers(p=>true,page_id,pageSize,out recordCount);
        }
        */
        public IEnumerable<User> getUsers(out int recordCount)
        {
            return userRepository.getUsers(p => true, out recordCount);
        }

        public IEnumerable<User> getUsersByPhone(string phone, out int recordCount)
        {
            return userRepository.getUsers(p => p.Phone.Contains(phone), out recordCount);
        }

        public User getUserById(int userid)
        {
            return userRepository.getUserById(userid);
        }

        //根据电话号码获取用户
        public User getUserByPhone(string phone)
        {
            return userRepository.getUserByPhone(phone.Trim());
        }

        public User getUserByInviteCode(Code inviteCode)
        {
            if (inviteCode == null) return null;
            return userRepository.getUserByInviteCode(inviteCode.CodeValue);
        }

        //根据设备标识获取邀请码，进而获取用户
        public User getUserByDevice(string deviceUniqueCode)
        {
            Code existingCode = codeRepository.getInviteCodes(deviceUniqueCode).FirstOrDefault();
            if (existingCode == null)
                return null;
            User user = userRepository.getUserByInviteCode(existingCode.CodeValue);
            return user;
        }

        //根据设备信息和邀请码判断用户是否存在
        public bool IsCurrentUserExist(string deviceUniqueCode, Code inputCode)
        {
            User user_1 = getUserByDevice(deviceUniqueCode);
            User user_2 = getUserByInviteCode(inputCode);
            return (user_1 != null || user_2 != null);
        }
    }
}