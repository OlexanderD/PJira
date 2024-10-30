using Microsoft.EntityFrameworkCore;
using Pjira.Core.Models;


namespace Pjira.Application.Common.Interfaces
{
    public interface IPjiraDbContext
    {
        DbSet<Assignment> Assignments { get; }

        DbSet <Project> Projects { get;}

         Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
