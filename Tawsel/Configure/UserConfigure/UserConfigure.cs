using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tawsel.Models;

namespace Tawsel.Configure.UserConfigure
{
    public class UserConfigure : IEntityTypeConfiguration<User>
    {
       public void Configure(EntityTypeBuilder<User> builder)
       {
           builder.Property(x => x.Role).HasConversion<string>();
       }


    }
}


