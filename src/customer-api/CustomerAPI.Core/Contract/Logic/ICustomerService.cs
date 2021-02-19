using CustomerAPI.Core.DTOs.Creates;
using CustomerAPI.Core.DTOs.Details;
using CustomerAPI.Core.DTOs.Filters;
using CustomerAPI.Core.DTOs.Lists;
using CustomerAPI.Core.DTOs.Updates;
using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using Mvp24Hours.Core.DTOs;
using System.Threading.Tasks;

namespace CustomerAPI.Core.Contract.Logic
{
    public interface ICustomerService
    {
        Task<IPagingResult<GetByCustomerResponse>> GetBy(GetByCustomerRequest filter, IPagingCriteria criteria);
        Task<IBusinessResult<GetByIdCustomerResponse>> GetById(int id);
        Task<IBusinessResult<CreateCustomerResponse>> Create(CreateCustomerRequest dto);
        Task<IBusinessResult<VoidResult>> Update(int id, UpdateCustomerRequest dto);
        Task<IBusinessResult<VoidResult>> Delete(int id);
    }
}
