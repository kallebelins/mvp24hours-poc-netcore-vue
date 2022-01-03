using CustomerAPI.Core.Entities;
using CustomerAPI.Infrastructure.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerAPI.Infrastructure.Builders
{
    public class ContactConfiguration : IModelConfiguration<Contact>
    {
        public virtual void Apply(EntityTypeBuilder<Contact> builder)
        {
            // contact->id
            builder
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Id)
                .IsRequired();
            // contact->customerId
            builder
                .Property(p => p.CustomerId)
                .IsRequired();
            // contact->type
            builder
                .Property(p => p.Type)
                .HasConversion<string>()
                .IsRequired();
            // contact->description
            builder
                .Property(p => p.Description)
                .HasMaxLength(255)
                .IsRequired();
            // contact->active
            builder
                .Property(p => p.Active)
                .IsRequired();
        }
    }
}
