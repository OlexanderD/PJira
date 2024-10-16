using Microsoft.EntityFrameworkCore;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Common.Interfaces
{
    public interface IPjiraDbContext
    {
        DbSet <Assignment> Assignments {  get;}

         Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
