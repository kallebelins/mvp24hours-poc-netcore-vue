using AuthenticationAPI.Filters;
using CustomerAPI.Application;
using CustomerAPI.Core.DTOs.Creates;
using CustomerAPI.Core.DTOs.Details;
using CustomerAPI.Core.DTOs.Filters;
using CustomerAPI.Core.DTOs.Lists;
using CustomerAPI.Core.DTOs.Updates;
using Microsoft.AspNetCore.Mvc;
using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using Mvp24Hours.Core.DTOs;
using Mvp24Hours.Core.DTOs.Models;
using Mvp24Hours.Core.Extensions;
using Mvp24Hours.WebAPI.Extensions;
using System.Threading.Tasks;

namespace CustomerAPI.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [JWTAuthorize]
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [Route("", Name = "CustomerGetBy")]
        public async Task<IPagingResult<GetByCustomerResponse>> GetBy([FromQuery] GetByCustomerRequest filter, [FromQuery] PagingCriteriaRequest clause)
        {
            var result = await FacadeService.CustomerService.GetBy(filter, clause.ToPagingService());
            result.AddLinkSelf("CustomerGetBy");
            result.AddLinkItem("CustomerGetById");
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [Route("{id}", Name = "CustomerGetById")]
        public async Task<IBusinessResult<GetByIdCustomerResponse>> GetById(int id)
        {
            var result = await FacadeService.CustomerService.GetById(id);
            result.AddLinkSelf("CustomerGetById");
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [Route("", Name = "CustomerCreate")]
        public Task<IBusinessResult<CreateCustomerResponse>> Create([FromBody] CreateCustomerRequest dto)
        {
            return FacadeService.CustomerService.Create(dto);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpPut]
        [Route("{id}", Name = "CustomerUpdate")]
        public Task<IBusinessResult<VoidResult>> Update(int id, [FromBody] UpdateCustomerRequest dto)
        {
            return FacadeService.CustomerService.Update(id, dto);
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpDelete]
        [Route("{id}", Name = "CustomerDelete")]
        public Task<IBusinessResult<VoidResult>> Delete(int id)
        {
            return FacadeService.CustomerService.Delete(id);
        }

    }
}
