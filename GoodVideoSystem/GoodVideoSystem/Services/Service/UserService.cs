using GoodVideoSystem.Models.VO;
using GoodVideoSystem.Repositories.IRepository;
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

        public UserService(IUserRepository userRepository, ICodeRepository codeRepository)
        {
            this.userRepository = userRepository;
            this.AddDisposableObject(userRepository);

            this.codeRepository = codeRepository;
            this.AddDisposableObject(codeRepository);
        }

        //根据设备信息获取用户
        public void updateUserInfo(Code inviteCode, string deviceUniqueCode)
        {
            deviceUniqueCode = deviceUniqueCode.Trim();
            if (string.IsNullOrEmpty(deviceUniqueCode))
                return;

            bool isNewDevice = (codeRepository.getInviteCodes(deviceUniqueCode).FirstOrDefault() == null);
            bool isNewInviteCode = (inviteCode.BindedDeviceCount == 0);
           
            if (isNewInviteCode) //但凡用户输入新的有效的邀请码，必须更新用户的邀请码
            { 
                if (isNewDevice) // 1.如果在一台新的设备上播放，需要新建用户（无法确定设备属于哪个用户）
                {
                    User user = new User() { InviteCodes = inviteCode.CodeValue, Phone = "无",Username = "无" };
                    registeUser(user);
                }

                else // 2.如果在一台旧的设备上播放，用户的邀请码更新，寻找用户得通过设备信息-》邀请码-》用户
                {
                    Code existingCode = codeRepository.getInviteCodes(deviceUniqueCode).FirstOrDefault();
                    User user = userRepository.getUserByInviteCode(existingCode.CodeValue);
                    user.InviteCodes += ("," + inviteCode.CodeValue);
                    updateUser(user);
                }
            }
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
/*        public IEnumerable<User> getUsers(int page_id,int pageSize,out int recordCount)
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
            return userRepository.getUserById(userid)
;
        }

        //根据设备标识获取邀请码，进而获取用户
        public User GetCurrentUser(string deviceUniqueCode)
        {
            Code existingCode = codeRepository.getInviteCodes(deviceUniqueCode).FirstOrDefault();
            if (existingCode == null)
            {
                return null;
            }
            else
            {
                User user = userRepository.getUserByInviteCode(existingCode.CodeValue);
                return user;
            }
        }
    }
}