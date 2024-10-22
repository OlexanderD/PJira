using MediatR;
using Microsoft.EntityFrameworkCore;
using Pjira.Application.Common.Interfaces;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery,Project>
    {
        private readonly IPjiraDbContext _pjiraDbContext;

        public GetProjectByIdQueryHandler(IPjiraDbContext pjiraDbContext)
        {
            _pjiraDbContext = pjiraDbContext;
        }

        public async Task<Project> Handle(GetProjectByIdQuery query,CancellationToken cancellationToken)
        {
            var project = await _pjiraDbContext.Projects.Include(x => x.Assignments).FirstOrDefaultAsync(x => x.Id == query.Id,cancellationToken);

            if (project == null)
            {
                throw new Exception($"Project with ID {query.Id} not found.");
            }

            return project;
        }
    }
}
