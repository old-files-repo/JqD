using System.Collections.Generic;
using JqD.Common.Command.SystemManage;
using JqD.Common.Entities;
using JqD.Common.Models;
using JqD.Data.Logic;
using JqD.Entities.QueryModels;

namespace JqD.Common.ILogic
{
    public interface ISystemUserLogic : ILogicBase<SystemUser>
    {
        void Add(AddUserCommand user);

        void Update(UpdateUserCommand user);

        LoginUserInformation Login(string userName, string password);

        void LogOut(string userNo);

        LoginUserInformation GetLoginUserInformationById(int sysUserId);

        IEnumerable<SystemUser> QueryByPage(QuerySystemUserModel queryParams,out int totleCount);
    }
}