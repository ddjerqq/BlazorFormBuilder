using System.Numerics;
using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class UserFormConfiguration : IEntityTypeConfiguration<UserForm>
{
    public void Configure(EntityTypeBuilder<UserForm> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(256);

        builder.HasMany(form => form.Fields)
            .WithOne()
            .HasForeignKey(comp => comp.FormId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Navigation(form => form.Fields).AutoInclude();
    }
}