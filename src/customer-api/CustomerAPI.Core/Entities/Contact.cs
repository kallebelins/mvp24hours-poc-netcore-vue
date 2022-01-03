using CustomerAPI.Core.Enums;
using Mvp24Hours.Core.Entities;
using System.Text.Json.Serialization;

namespace CustomerAPI.Core.Entities
{
    public class Contact : EntityBase<Contact, int>
    {
        public virtual int CustomerId { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public virtual ContactType Type { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Active { get; set; }
    }
}
