using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

public class SizeConfiguration : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
        builder.Property(p => p.SizeAsName).IsRequired(false).HasMaxLength(10).HasColumnType("varchar");
        builder.Property(p => p.SizeAsNumber).IsRequired(false).HasColumnType("smallint");

        builder.HasData(FillData());
    }

    private List<Size> FillData() =>
        new List<Size>()
        {
                // Named sizes (textual) - deterministic GUIDs for stable migrations
                new Size { Id = Guid.Parse("f1a1f6a1-0001-4a6b-9b1a-000000000001"), SizeAsName = "XS",  SizeAsNumber = null },
                new Size { Id = Guid.Parse("f1a1f6a1-0002-4a6b-9b1a-000000000002"), SizeAsName = "S",   SizeAsNumber = null },
                new Size { Id = Guid.Parse("f1a1f6a1-0003-4a6b-9b1a-000000000003"), SizeAsName = "M",   SizeAsNumber = null },
                new Size { Id = Guid.Parse("f1a1f6a1-0004-4a6b-9b1a-000000000004"), SizeAsName = "L",   SizeAsNumber = null },
                new Size { Id = Guid.Parse("f1a1f6a1-0005-4a6b-9b1a-000000000005"), SizeAsName = "XL",  SizeAsNumber = null },
                new Size { Id = Guid.Parse("f1a1f6a1-0006-4a6b-9b1a-000000000006"), SizeAsName = "XXL", SizeAsNumber = null },

                // Numeric sizes (clothing numbers) - deterministic GUIDs as well
                new Size { Id = Guid.Parse("f1a1f6a1-0010-4a6b-9b1a-000000000010"), SizeAsName = null, SizeAsNumber = 28 },
                new Size { Id = Guid.Parse("f1a1f6a1-0011-4a6b-9b1a-000000000011"), SizeAsName = null, SizeAsNumber = 30 },
                new Size { Id = Guid.Parse("f1a1f6a1-0012-4a6b-9b1a-000000000012"), SizeAsName = null, SizeAsNumber = 32 },
                new Size { Id = Guid.Parse("f1a1f6a1-0013-4a6b-9b1a-000000000013"), SizeAsName = null, SizeAsNumber = 34 },
                new Size { Id = Guid.Parse("f1a1f6a1-0014-4a6b-9b1a-000000000014"), SizeAsName = null, SizeAsNumber = 36 },
                new Size { Id = Guid.Parse("f1a1f6a1-0015-4a6b-9b1a-000000000015"), SizeAsName = null, SizeAsNumber = 38 }
        };
}
