using Users.Domain.Users.Entities;
using Users.Generics.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Users.Infra.Data.Mapping
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.Property(_ => _.Email)
                   .IsRequired();

            builder.Property(_ => _.Password)
                   .IsRequired();

            builder.Property(_ => _.Name)
                   .IsRequired();

            builder.Ignore(_ => _.CascadeMode);
            builder.Ignore(_ => _.ValidationResult);

            builder.ToTable("Users");
        }
    }
}
