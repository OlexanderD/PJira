using MediatR;
using Pjira.Application.Common.Interfaces;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler:IRequestHandler<CreateProjectCommand,Guid>
    {
        private readonly IPjiraDbContext _pjiraDbContext;


        public CreateProjectCommandHandler(IPjiraDbContext pjiraDbContext)
        {
            _pjiraDbContext = pjiraDbContext;
        }

        public async Task<Guid> Handle(CreateProjectCommand command,CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Name = command.Name,
                IsActive = false,
            };

             _pjiraDbContext.Projects.Add(project);

            await _pjiraDbContext.SaveChangesAsync(cancellationToken);

            return project.Id;
        }

    }
}
