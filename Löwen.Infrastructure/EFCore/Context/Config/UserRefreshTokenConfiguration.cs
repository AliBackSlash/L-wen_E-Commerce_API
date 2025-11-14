using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Löwen.Infrastructure.EFCore.Context.Config;

public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(x => x.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()"); ;

        builder.Property(e => e.Token).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone").HasDefaultValueSql("NOW() AT TIME ZONE 'utc'");
        builder.Property(e => e.ExpiresAt).IsRequired();
        builder.Property(e => e.DeviceName).IsRequired(false).HasMaxLength(200);
        builder.Property(e => e.IpAddress).IsRequired(false).HasColumnType("varchar(45)");

    }
}
