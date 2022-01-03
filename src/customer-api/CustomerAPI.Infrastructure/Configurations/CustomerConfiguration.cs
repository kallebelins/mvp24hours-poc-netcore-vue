using CustomerAPI.Core.Entities;
using CustomerAPI.Infrastructure.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerAPI.Infrastructure.Builders
{
    public class CustomerConfiguration : IModelConfiguration<Customer>
    {
        public void Apply(EntityTypeBuilder<Customer> builder)
        {
            // customer->id
            builder
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Id)
                .IsRequired();
            // customer->name
            builder
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();
            // customer->note
            builder
                .Property(p => p.Note)
                .HasMaxLength(2000);
            // customer->active
            builder
                .Property(p => p.Active)
                .IsRequired();
            // customer->contacts
            builder
                .HasMany(p => p.Contacts);
        }
    }
}
