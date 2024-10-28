using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pjira.Application.Common.Interfaces;
using Pjira.Core.Models;
using System.Reflection;

namespace Pjira.Infrastructure.Data
{
    public  class PjiraDbContext: IdentityDbContext<IdentityUser>,IPjiraDbContext
    {
        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Project> Projects { get; set; }

        public PjiraDbContext(DbContextOptions<PjiraDbContext> options) : base(options)
        {
            
            Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}
