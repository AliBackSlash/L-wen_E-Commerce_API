using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

public class ColorConfiguration : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(x => x.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(p => p.Name).IsRequired().HasMaxLength(50).HasColumnType("varchar");
        builder.Property(p => p.HexCode).IsRequired().HasMaxLength(7).HasColumnType("varchar");

        builder.HasData(FillData());
    }

    private List<Color> FillData() =>
    new List<Color>
    {
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "White",     HexCode = "#FFFFFF" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Black",     HexCode = "#000000" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Navy",      HexCode = "#001F3F" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Gray",      HexCode = "#808080" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Light Gray",HexCode = "#D3D3D3" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), Name = "Charcoal",  HexCode = "#36454F" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), Name = "Red",       HexCode = "#FF0000" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), Name = "Burgundy",  HexCode = "#800020" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), Name = "Blue",      HexCode = "#0074D9" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-00000000000A"), Name = "Sky Blue",  HexCode = "#87CEEB" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-00000000000B"), Name = "Green",     HexCode = "#2ECC40" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-00000000000C"), Name = "Olive",     HexCode = "#708238" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-00000000000D"), Name = "Brown",     HexCode = "#8B4513" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-00000000000E"), Name = "Beige",     HexCode = "#F5F5DC" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-00000000000F"), Name = "Tan",       HexCode = "#D2B48C" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), Name = "Pink",      HexCode = "#FFC0CB" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000011"), Name = "Purple",    HexCode = "#800080" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000012"), Name = "Mustard",   HexCode = "#FFDB58" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000013"), Name = "Teal",      HexCode = "#008080" },
        new Color { Id = Guid.Parse("00000000-0000-0000-0000-000000000014"), Name = "Gold",      HexCode = "#D4AF37" }
    };
}
