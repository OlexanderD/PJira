using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommand:IRequest
    {
        public Guid Id { get; set; }
    }
}
