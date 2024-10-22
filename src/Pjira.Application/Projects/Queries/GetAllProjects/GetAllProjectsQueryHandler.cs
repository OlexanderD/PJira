using MediatR;
using Microsoft.EntityFrameworkCore;
using Pjira.Application.Common.Interfaces;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Projects.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler:IRequestHandler<GetAllProjectsQuery,List<Project>>
    {
        private readonly IPjiraDbContext _pjiraDbContext;

        public GetAllProjectsQueryHandler(IPjiraDbContext pjiraDbContext)
        {
            _pjiraDbContext = pjiraDbContext;
        }

        public async Task<List<Project>> Handle(GetAllProjectsQuery query,CancellationToken cancellationToken)
        {
            var projects =  await _pjiraDbContext.Projects.Include(x => x.Assignments).ToListAsync(cancellationToken);

            return projects;
        }

    }
}
