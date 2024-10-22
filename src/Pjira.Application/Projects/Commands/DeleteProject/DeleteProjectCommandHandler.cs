using MediatR;
using Microsoft.EntityFrameworkCore;
using Pjira.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler:IRequestHandler<DeleteProjectCommand>
    {
        private readonly IPjiraDbContext _pjiraDbContext;

        public DeleteProjectCommandHandler(IPjiraDbContext pjiraDbContext)
        {
            _pjiraDbContext = pjiraDbContext;
        }

       public async Task Handle(DeleteProjectCommand command,CancellationToken cancellationToken)
        {
            var project = await _pjiraDbContext.Projects.FirstOrDefaultAsync(x => x.Id == command.Id);

            if (project == null)
            {
                throw new Exception("Project Exception");
            }

            _pjiraDbContext.Projects.Remove(project);

            await _pjiraDbContext.SaveChangesAsync(cancellationToken);


        }
    }
}
