using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommand:IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsActive {  get; set; }
    }
}
