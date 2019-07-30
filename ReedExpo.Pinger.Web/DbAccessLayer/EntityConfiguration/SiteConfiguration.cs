using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbAccessLayer
{
    public class SiteConfiguration : IEntityTypeConfiguration<Site>
    {
        public void Configure(EntityTypeBuilder<Site> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(30);

            builder.HasMany(x => x.Pings)
                .WithOne(x => x.Site)
                .HasForeignKey(x => x.SiteId);
        }

    }
}