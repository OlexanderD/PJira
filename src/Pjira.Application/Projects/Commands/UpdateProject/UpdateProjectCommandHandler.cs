using MediatR;
using Pjira.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler:IRequestHandler<UpdateProjectCommand>
    {
        private readonly IPjiraDbContext _pjiraDbContext;

        public UpdateProjectCommandHandler(IPjiraDbContext pjiraDbContext)
        {
            _pjiraDbContext = pjiraDbContext;
        }

        public async Task Handle(UpdateProjectCommand command,CancellationToken cancellationToken)
        {
            var project = _pjiraDbContext.Projects.FirstOrDefault(x => x.Id == command.Id);

            if (project == null)
            {
                throw new Exception("Project Update Exception");
            }

            project.Name = command.Name;

            project.IsActive = command.IsActive;

            _pjiraDbContext.Projects.Update(project);

            await _pjiraDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
