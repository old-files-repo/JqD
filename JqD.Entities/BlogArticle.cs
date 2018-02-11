using System;
using JqD.Infrustruct.Enums;

namespace JqD.Entities
{
    public class BlogArticle:Entity
    { 
        public string Title { get; set; }

        public Enums.Category Category { get; set; }

        public string Content { get; set; }

        public string Remark { get; set; }

        public string CreateUser { get; set; }

        public DateTime CreateDate { get; set; }

        public string EditUser { get; set; }

        public DateTime? EditDate { get; set; }

        public Enums.Status Status { get; set; }
    }
}
