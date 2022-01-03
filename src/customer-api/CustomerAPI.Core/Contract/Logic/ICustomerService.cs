using CustomerAPI.Core.ValueObjects.Customers;
using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using Mvp24Hours.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAPI.Core.Contract.Logic
{
    public interface ICustomerService
    {
        Task<IPagingResult<IList<GetByCustomerResponse>>> GetBy(GetByCustomerRequest filter, IPagingCriteria criteria);
        Task<IBusinessResult<GetByIdCustomerResponse>> GetById(int id);
        Task<IBusinessResult<CreateCustomerResponse>> Create(CreateCustomerRequest dto);
        Task<IBusinessResult<VoidResult>> Update(int id, UpdateCustomerRequest dto);
        Task<IBusinessResult<VoidResult>> Delete(int id);
    }
}
