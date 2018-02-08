using JqD.Command.SystemManage;
using JqD.Common.Entities;
using JqD.Common.Models;
using JqD.Data.Logic;

namespace JqD.Common.ILogic
{
    public interface ISystemUserLogic : ILogicBase<SystemUser>
    {
        void Insert(AddUserCommand user);

        LoginUserInformation Login(string userName, string password);

        void LogOut(string userNo);

        LoginUserInformation GetLoginUserInformationById(int sysUserId);
    }
}