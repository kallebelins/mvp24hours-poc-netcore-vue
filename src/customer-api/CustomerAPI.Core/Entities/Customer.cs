using Mvp24Hours.Core.Entities;
using System.Collections.Generic;

namespace CustomerAPI.Core.Entities
{
    public class Customer : EntityBase<Customer, int>
    {
        public virtual string Name { get; set; }
        public virtual string Note { get; set; }
        public virtual bool Active { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
