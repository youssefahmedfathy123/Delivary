using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tawsel.Models;

namespace Tawsel.Configure.BuyConfigure
{
        public class BuyConfigure : IEntityTypeConfiguration<Buy>
        {
            public void Configure(EntityTypeBuilder<Buy> builder)
            {
            builder.Property(x => x.Day).HasConversion<string>();
            builder.Property(x => x.Date).HasColumnName("Date of delivery");
            builder.Property(x => x.Hour).HasConversion<string>();

        }

    }

}

