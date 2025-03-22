using Domain.Aggregates;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class BaseComponentChoiceConfiguration : IEntityTypeConfiguration<BaseComponentChoice>
{
    public void Configure(EntityTypeBuilder<BaseComponentChoice> builder)
    {
        builder.Property(x => x.Label).HasMaxLength(64);
        builder.Property(x => x.Description).HasMaxLength(128);

        builder.HasDiscriminator<string>("type")
            .HasValue<ButtonComponentChoice>("button")
            .HasValue<CheckboxInput>("switch")
            .HasValue<DateInput>("date")
            .HasValue<NumberInput>("number")
            .HasValue<SelectInput>("select")
            .HasValue<TextInput>("text")
            .HasValue<GridComponentChoice>("grid");

        builder.HasOne<UserForm>()
            .WithMany(form => form.Fields)
            .HasForeignKey(comp => comp.FormId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}