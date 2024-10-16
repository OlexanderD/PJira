using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Assigments.Commands.DeleteAssigment
{
    public class DeleteAssigmentCommand:IRequest
    {
        public Guid Id { get; set; }
    }
}
