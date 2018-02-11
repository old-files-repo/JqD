using System.Collections.Generic;
using JqD.Infrustruct;
using JqD.Infrustruct.Enums;

namespace JqueryDapper.ViewModels.Work
{
    public class WorkPageViewModel
    {
        public List<WorkViewModel> Works { get; set; }
        public Pagination Pagination { get; set; }
    }

    public class WorkViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Enums.Category Category { get; set; }
        public string Remark { get; set; }
        public string CreateUser { get; set; }
        public string CreateDate { get; set; }
    }
}