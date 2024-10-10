using MediatR;
using Microsoft.EntityFrameworkCore;
using Pjira.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Assigments.Commands.UpdateAssigment
{
    public class UpdateAssigmentCommandHandler:IRequestHandler<UpdateAssigmentCommand>
    {
        private readonly IPjiraDbContext _pjiraDbContext;

        public UpdateAssigmentCommandHandler(IPjiraDbContext pjiraDbContext)
        {
            _pjiraDbContext = pjiraDbContext;
        }

        public async Task Handle(UpdateAssigmentCommand command,CancellationToken cancellationToken)
        {
            var assigment = await _pjiraDbContext.Assignments.FirstOrDefaultAsync(x => x.Id == command.Id);

            if (assigment == null)
            {
                throw new Exception("Puka");
            }

            assigment.Title = command.Title;

            assigment.Description = command.Description;

            assigment.Status = command.Status;

            _pjiraDbContext.Assignments.Update(assigment);

            await _pjiraDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
