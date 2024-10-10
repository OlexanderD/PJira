using MediatR;
using Pjira.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Assigments.Commands.DeleteAssigment
{
    public class DeleteAssigmentCommandHandler:IRequestHandler<DeleteAssigmentCommand>
    {
        private readonly IPjiraDbContext _pjiracontext;

        public DeleteAssigmentCommandHandler(IPjiraDbContext context)
        {
            _pjiracontext = context;
        }

        public async Task Handle(DeleteAssigmentCommand command,CancellationToken cancellationToken)
        {
            var assigment = _pjiracontext.Assignments.FirstOrDefault(x => x.Id == command.Id);

            if (assigment == null)
            {
                throw new Exception("Puka");
            }

            _pjiracontext.Assignments.Remove(assigment);

            await _pjiracontext.SaveChangesAsync(cancellationToken);
        }
    }
}
