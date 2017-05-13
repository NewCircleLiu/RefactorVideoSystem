using GoodVideoSystem.Repositories.IRepository;
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
    }
}