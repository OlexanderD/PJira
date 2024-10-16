using MediatR;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Assigments.Queries.GetAllAssigments
{
    public class GetAllAssigmentQuery:IRequest<List<Assignment>>
    {
       
    }
}
