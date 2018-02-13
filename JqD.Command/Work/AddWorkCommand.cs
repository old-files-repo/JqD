using JqD.Infrustruct.Enums;

namespace JqD.Command.Work
{
    public class AddWorkCommand
    {
        public string Title { get; set; }

        public Enums.Category Category { get; set; }

        public string Content { get; set; }
    }
}