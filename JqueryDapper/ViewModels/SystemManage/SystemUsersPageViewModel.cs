using System.Collections.Generic;
using JqD.Infrustruct;

namespace JqueryDapper.ViewModels.SystemManage
{
    public class SystemUsersPageViewModel
    {
        public List<SystemUserViewModel> SystemUsers { get; set; }
        public Pagination Pagination { get; set; }
    }

    public class SystemUserViewModel
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}