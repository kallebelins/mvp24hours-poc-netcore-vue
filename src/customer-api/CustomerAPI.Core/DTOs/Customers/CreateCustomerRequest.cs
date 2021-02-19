using AutoMapper;
using CustomerAPI.Core.Entities;
using Mvp24Hours.Core.Contract.Mappings;

namespace CustomerAPI.Core.DTOs.Creates
{
    public class CreateCustomerRequest : IMapFrom<Customer>
    {
        public string Name { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCustomerRequest, Customer>();
        }
    }
}
