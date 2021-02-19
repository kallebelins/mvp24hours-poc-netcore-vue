using AutoMapper;
using CustomerAPI.Core.DTOs.Lists;
using CustomerAPI.Core.Entities;
using Mvp24Hours.Core.Contract.Mappings;
using System;

namespace CustomerAPI.Core.DTOs.Details
{
    public class GetByIdCustomerResponse : GetByCustomerResponse, IMapFrom<Customer>
    {
        public DateTime Created { get; set; }
        public string Note { get; set; }

        public override void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, GetByIdCustomerResponse>();
        }
    }
}
