using AuthenticationAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using Mvp24Hours.Core.DTOs.Models;
using Mvp24Hours.Core.Extensions;
using Mvp24Hours.WebAPI.Extensions;
using ProductAPI.Application;
using ProductAPI.Core.DTOs.Products;
using System.Threading.Tasks;

namespace ProductAPI.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [JWTAuthorize]
    public class ProductController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [Route("", Name = "ProductList")]
        public async Task<IPagingResult<ListProductResponse>> List([FromQuery] PagingCriteriaRequest criteria)
        {
            var result = await FacadeService.ProductService.List(criteria.ToPagingService());
            result.AddLinkSelf("ProductList");
            result.AddLinkItem("ProductGetById");
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [Route("{id}", Name = "ProductGetById")]
        public async Task<IBusinessResult<GetByIdProductResponse>> GetById(int id)
        {
            var result = await FacadeService.ProductService.GetById(id);
            result.AddLinkSelf("ProductGetById");
            return result;
        }

    }
}
