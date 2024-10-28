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

namespace Pjira.Application.Projects.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler:IRequestHandler<GetAllProjectsQuery,List<ProjectDto>>
    {
        private readonly IPjiraDbContext _pjiraDbContext;

        private readonly IMapper _mapper;
        public GetAllProjectsQueryHandler(IPjiraDbContext pjiraDbContext,IMapper mapper)
        {
            _pjiraDbContext = pjiraDbContext;

            _mapper = mapper;
        }

        public async Task<List<ProjectDto>> Handle(GetAllProjectsQuery query,CancellationToken cancellationToken)
        {
            var projects =  await _pjiraDbContext.Projects.Include(x => x.Assignments).ToListAsync(cancellationToken);

            var projectDtos = _mapper.Map<List<ProjectDto>>(projects);

            return projectDtos;
        }

    }
}
