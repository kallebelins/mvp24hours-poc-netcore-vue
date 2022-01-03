using AutoMapper;
using CustomerAPI.Core.Entities;
using Mvp24Hours.Core.Contract.Mappings;

namespace CustomerAPI.Core.ValueObjects.Customers
{
    public class CreateCustomerResponse : IMapFrom<Customer>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, CreateCustomerResponse>();
        }
    }
}
