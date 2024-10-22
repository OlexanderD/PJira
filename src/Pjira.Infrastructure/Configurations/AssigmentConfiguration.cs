using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pjira.Core.Models;
using System.Reflection.Emit;

namespace Pjira.Infrastructure.Configurations
{
    public class AssigmentConfiguration:IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Description);

            builder.Property(x =>x.Status);


            builder.HasOne(a => a.Project)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(a => a.ProjectId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
