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

namespace Pjira.Application.Assigments.Queries.GetAllAssigments
{
    public class GetAllAssigmentsQueryHandler : IRequestHandler<GetAllAssigmentQuery, List<AssigmentDto>>
    {
        private readonly IPjiraDbContext _pjiraDbContext;

        private readonly IMapper _mapper;

        public GetAllAssigmentsQueryHandler(IPjiraDbContext pjiraDbContext,IMapper mapper)
        {
            _pjiraDbContext = pjiraDbContext;   

            _mapper = mapper;
        }

        public async Task<List<AssigmentDto>> Handle(GetAllAssigmentQuery query,CancellationToken cancellationToken)
        {
            var assigments = await _pjiraDbContext.Assignments.ToListAsync(cancellationToken);

            var assigmentDto = _mapper.Map<List<AssigmentDto>>(assigments);
            
            return assigmentDto;
        }
    }
}
