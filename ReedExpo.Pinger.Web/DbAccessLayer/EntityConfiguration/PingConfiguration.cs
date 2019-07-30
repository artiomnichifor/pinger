using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbAccessLayer
{
    public class PingConfiguration : IEntityTypeConfiguration<Ping>
    {
        public void Configure(EntityTypeBuilder<Ping> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.UpTime)
                .IsRequired()
                .HasColumnType("Date")
                .HasDefaultValueSql("GetDate()");


        }

    }
}