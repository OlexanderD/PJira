using MediatR;
using Microsoft.EntityFrameworkCore;
using Pjira.Application.Common.Interfaces;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Assigments.Queries.GetAllAssigments
{
    public class GetAllAssigmentsQueryHandler:IRequestHandler<GetAllAssigmentQuery , List<Assignment>>
    {
        private readonly IPjiraDbContext _pjiraDbContext;

        public GetAllAssigmentsQueryHandler(IPjiraDbContext pjiraDbContext)
        {
            _pjiraDbContext = pjiraDbContext;   
        }

        public async Task<List<Assignment>> Handle(GetAllAssigmentQuery query,CancellationToken cancellationToken)
        {
            var assigments = await _pjiraDbContext.Assignments.ToListAsync(cancellationToken);
            
            return assigments;
        }
    }
}
