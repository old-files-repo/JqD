using JqD.Data.Logic;
using JqD.Entities;
using JqD.ILogic;
using JqD.IRepository;

namespace JqD.Logic
{
    public class UserInfoLogic: LogicBase<UserInfo>, IUserInfoLogic
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public UserInfoLogic(IUserInfoRepository userInfoRepository):base(userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }

    }
}
