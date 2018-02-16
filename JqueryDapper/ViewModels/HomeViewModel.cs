using System.Collections.Generic;
using JqD.Infrustruct.Enums;

namespace JqueryDapper.ViewModels
{
    public class HomePageViewModel
    {
        public List<HomeViewModel> Homes { get; set; }
    }

    public class HomeViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Enums.Category Category { get; set; }

        public string ImagePath { get; set; }

        public bool ImagePathStatus { get; set; }

        public string CreateUser { get; set; }

        public string CreateDate { get; set; }
    }
}