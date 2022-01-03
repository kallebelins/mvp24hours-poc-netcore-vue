using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerAPI.Infrastructure.Contract
{
    public interface IModelConfiguration<TEntity> where TEntity : class
    {
        void Apply(EntityTypeBuilder<TEntity> builder);
    }
}
