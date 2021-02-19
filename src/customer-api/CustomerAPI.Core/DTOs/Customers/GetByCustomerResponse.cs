using AutoMapper;
using CustomerAPI.Core.Entities;
using Mvp24Hours.Core.Contract.Mappings;
using System.Collections.Generic;

namespace CustomerAPI.Core.DTOs.Lists
{
    public class GetByCustomerResponse : IMapFrom<Customer>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, GetByCustomerResponse>();
            profile.CreateMap<List<Customer>, List<GetByCustomerResponse>>();
        }
    }
}
