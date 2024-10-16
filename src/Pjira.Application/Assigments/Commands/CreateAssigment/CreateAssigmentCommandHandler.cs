using MediatR;
using Pjira.Application.Common.Interfaces;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Tasks.Commands.CreateTask
{
    public class CreateAssigmentCommandHandler:IRequestHandler<CreateAssigmentCommand,Guid>
    {
        private readonly IPjiraDbContext _pjiraDbContext;

        public CreateAssigmentCommandHandler(IPjiraDbContext pjiraDbContext)
        {
            _pjiraDbContext = pjiraDbContext;
        }

        public async Task<Guid> Handle(CreateAssigmentCommand command,CancellationToken cancellationToken)
        {
            var task = new Assignment
            {
                Title = command.Title,

                Description = command.Description,

                Status = command.Status,

            };

            _pjiraDbContext.Assignments.Add(task);

            await _pjiraDbContext.SaveChangesAsync(cancellationToken);

            return task.Id;

        }
    }
}
