using Looses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Looses.Infrastructure.EF.Config;

internal sealed class LoosesDbConfiguration : IEntityTypeConfiguration<Looses.Domain.Entities.Looses>, 
    IEntityTypeConfiguration<Well>

{
    public void Configure(EntityTypeBuilder<Looses.Domain.Entities.Looses> builder)
    {
        builder.ToTable("Looses");
        builder.HasKey(x => x.Id);
        builder.Property(t => t.EventName)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(t => t.WellName)
            .HasMaxLength(50)
            .IsRequired();
        builder.Property(x => x.LossDate).HasColumnType("date").IsRequired();

        builder
            .HasOne(x => x.Well)
            .WithMany(x => x.Looses)
            .HasForeignKey(x=> x.WellName);
        // .OnDelete(DeleteBehavior.SetNull)
        //.IsRequired();
    }

    public void Configure(EntityTypeBuilder<Well> builder)
    {
        builder.ToTable("Wells");
        //builder.HasKey( x=> new { x.Id, x.Name});
        builder.HasKey( x=>  x.Name);
        builder.HasIndex(x => x.Id).IsUnique();
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();
        
       // builder.HasMany(x=> x.Looses).wit
    }
}