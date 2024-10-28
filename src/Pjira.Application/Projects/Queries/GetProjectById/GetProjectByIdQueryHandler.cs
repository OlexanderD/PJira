using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pjira.Application.Common.Interfaces;
using Pjira.Application.DtoModels;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery,ProjectDto>
    {
        private readonly IPjiraDbContext _pjiraDbContext;

        private readonly IMapper _mapper;

        public GetProjectByIdQueryHandler(IPjiraDbContext pjiraDbContext,IMapper mapper)
        {
            _pjiraDbContext = pjiraDbContext;

            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdQuery query,CancellationToken cancellationToken)
        {
            var project = await _pjiraDbContext.Projects.Include(x => x.Assignments).FirstOrDefaultAsync(x => x.Id == query.Id,cancellationToken);

            if (project == null)
            {
                throw new Exception($"Project with ID {query.Id} not found.");
            }

            var ProjectDto = _mapper.Map<ProjectDto>(project);

            return ProjectDto;
        }
    }
}
