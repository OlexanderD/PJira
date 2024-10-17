using MediatR;
using Pjira.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Assigments.Commands.UpdateAssigment
{
    public class UpdateAssigmentCommand:IRequest
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public AssigmentStatus Status { get; set; }
    }
}
