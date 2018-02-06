using System;

namespace JqD.Entities
{
    public class UserInfo: Entity
    {
        public string Account { get; set; }

        public string PassWord { get; set; }

        public int Status { get; set; }

        public string CreateOperator { get; set; }

        public DateTime CreateOperateDate { get; set; }

        public string EditOperator { get; set; }

        public DateTime? EditOperateDate { get; set; }
    }
}
