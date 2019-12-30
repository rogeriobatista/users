using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Users.Generics.Configuration
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
