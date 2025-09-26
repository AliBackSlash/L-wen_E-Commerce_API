using Löwen.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

// Image Configuration
public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(i => i.Path).IsRequired().HasMaxLength(2048).HasColumnType("varchar");
        builder.Property(i => i.IsMain).IsRequired().HasColumnType("Boolean");
    }
}
