using Pjira.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.DtoModels
{
    public class AssigmentDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public AssigmentStatus Status { get; set; }

        public Guid? ProjectId { get; set; }
    }
}
