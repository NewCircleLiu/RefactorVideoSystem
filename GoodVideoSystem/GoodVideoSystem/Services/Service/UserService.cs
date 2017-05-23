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
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.AddDisposableObject(userRepository);
        }

        //用户登录
        public User userLogin(User user)
        {
            User userDB = userRepository.getUserByAccount(user.UserUniqueCode);
            if (userDB == null)
            {
                //对登录失败的情况做处理
                
                return userRregister(user);
            }

            //用户登录成功后应该查找用户拥有的视频

            return userDB;
        }

        //用户注册
        public User userRregister(User user)
        {
            userRepository.addUser(user);
            User userDB = userRepository.getUserByAccount(user.UserUniqueCode);
            return userDB;
        }
    }
}