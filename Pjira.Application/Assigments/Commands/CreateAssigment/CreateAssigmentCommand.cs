using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Tasks.Commands.CreateTask
{
    public class CreateAssigmentCommand:IRequest<Guid>
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TaskStatus Status { get; set; }


    }
}
