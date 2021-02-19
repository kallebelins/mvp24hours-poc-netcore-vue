using Mvp24Hours.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CustomerAPI.Core.Entities
{
    [DataContract(IsReference = false)]
    public class Customer : EntityBaseLog<int, string>
    {
        [DataMember]
        [Required]
        [StringLength(50)]
        public virtual string Name { get; set; }
        [DataMember]
        [StringLength(2000)]
        public virtual string Note { get; set; }
    }
}
