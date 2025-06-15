using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tawsel.Models;

namespace Tawsel.Configure.StatusConfigure
{
    public class StatusConfigure : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.Property(x => x.BuyStatus).HasConversion<string>();
        }

    }
}

