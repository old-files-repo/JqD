using System;
using JqD.Infrustruct.Enums;

namespace JqD.Common.Entities
{
    public class SystemUser : Entity
    {

        /// <summary>
        /// 登陆用户名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户最后登录时间
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        public string EditUser { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? EditDate { get; set; }

        /// <summary>
        /// 判断用户是否登录，1=登录，0=未登录
        /// </summary>
        public UserEnum.IsLogin IsLogin { get; set; }
    }
}