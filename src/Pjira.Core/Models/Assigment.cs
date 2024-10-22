using Pjira.Core.Enums;

namespace Pjira.Core.Models
{
    public class Assignment
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public AssigmentStatus Status { get; set; }

        public Guid? ProjectId { get; set; }

        public Project? Project { get; set; }
        
    }


}
