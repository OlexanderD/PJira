using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Pjira.Application.Common.Interfaces;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Infrastructure.Data
{
    public  class PjiraDbContext:DbContext, IPjiraDbContext
    {
        public DbSet<Assignment> Assignments { get; set; }

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
