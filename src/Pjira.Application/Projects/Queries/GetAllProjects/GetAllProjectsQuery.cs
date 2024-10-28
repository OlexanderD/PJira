using MediatR;
using Pjira.Application.DtoModels;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Projects.Queries.GetAllProjects
{
    public class GetAllProjectsQuery:IRequest<List<ProjectDto>>
    {
    }
}
