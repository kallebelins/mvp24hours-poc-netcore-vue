using AuthenticationAPI.Filters;
using CustomerAPI.Application;
using CustomerAPI.Core.ValueObjects.Customers;
using Microsoft.AspNetCore.Mvc;
using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using Mvp24Hours.Core.DTOs;
using Mvp24Hours.Core.DTOs.Models;
using Mvp24Hours.Core.Extensions;
using Mvp24Hours.WebAPI.Controller;
using System.Collections.Generic;
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
    public class CustomerController : BaseMvpController
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [Route("", Name = "CustomerGetBy")]
        public Task<IPagingResult<IList<GetByCustomerResponse>>> GetBy([FromQuery] GetByCustomerRequest filter, [FromQuery] PagingCriteriaRequest clause)
        {
            return FacadeService.CustomerService.GetBy(filter, clause.ToPagingService());
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [Route("{id}", Name = "CustomerGetById")]
        public Task<IBusinessResult<GetByIdCustomerResponse>> GetById(int id)
        {
            return FacadeService.CustomerService.GetById(id);
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
