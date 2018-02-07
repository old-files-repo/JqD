using System;
using System.Linq;
using JqD.Common.Entities;
using JqD.Common.ILogic;
using JqD.Common.IRepository;
using JqD.Common.Models;
using JqD.Data.Logic;

namespace JqD.Common.Logic
{
    public class SystemUserLogic : LogicBase<SystemUser>, ISystemUserLogic
    {
        private readonly ISystemUserRepository _systemUserRepository;
        private readonly ICurrentTimeProvider _currentTimeProvider;

        public SystemUserLogic(ISystemUserRepository systemUserRepository,
            ICurrentTimeProvider currentTimeProvider)
            : base(systemUserRepository)
        {
            _systemUserRepository = systemUserRepository;
            _currentTimeProvider = currentTimeProvider;
        }

        public LoginUserInformation Login(string userName, string password)
        {
            SystemUser systemUser;
            try
            {
                var list = _systemUserRepository.GetAll().ToList();
                systemUser= list.Single(x=>x.LoginName== userName);
            }
            catch
            {
                throw new Exception("用户名不存在");
            }
            if (!PasswordHasher.ValidateHash(password, systemUser.Password))
            {
                throw new Exception("登陆密码错误");
            }
            systemUser.LastLoginDate =_currentTimeProvider.CurrentTime();
            systemUser.IsLogin = 1;
            _systemUserRepository.Update(systemUser);
            var loginUserInfo = new LoginUserInformation
            {
                SystemUserId= systemUser.Id,
                LoginName= systemUser.UserNo
            };
            return loginUserInfo;
        }

        public void LogOut(string userNo)
        {
            var systemUser = _systemUserRepository.GetAll().Single(x => x.UserNo == userNo);
            systemUser.IsLogin = 0;
            _systemUserRepository.Update(systemUser);
        }

        public LoginUserInformation GetLoginUserInformationById(int sysUserId)
        {
            var systemUser = _systemUserRepository.Get(sysUserId);
            var loginUserInfo =new LoginUserInformation
            {
                SystemUserId = systemUser.Id,
                LoginName = systemUser.UserNo
            };
            return loginUserInfo;
        }
    }
}