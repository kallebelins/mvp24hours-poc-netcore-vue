using Mvp24Hours.Core.Contract.Infrastructure.Pipe;
using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using Mvp24Hours.Infrastructure.Extensions;
using Mvp24Hours.Infrastructure.Helpers;
using ProductAPI.Core.Contract.Logic;
using ProductAPI.Core.Contract.Pipeline.Builders.Products;
using ProductAPI.Core.DTOs.Products;
using System.Threading.Tasks;

namespace ProductAPI.Application.Logic
{
    public class ProductService : IProductService
    {
        public async Task<IPagingResult<ListProductResponse>> List(IPagingCriteria criteria)
        {
            IPipelineAsync pipeline = HttpContextHelper.GetService<IPipelineAsync>();

            var builder = HttpContextHelper.GetService<IListProductBuilder>();
            builder.Builder(pipeline);

            var result = await pipeline.Execute(criteria.ToMessage());
            return result.GetContent<IPagingResult<ListProductResponse>>();
        }

        public async Task<IBusinessResult<GetByIdProductResponse>> GetById(int id)
        {
            IPipelineAsync pipeline = HttpContextHelper.GetService<IPipelineAsync>();

            var builder = HttpContextHelper.GetService<IGetByIdProductBuilder>();
            builder.Builder(pipeline);

            var result = await pipeline.Execute(id.ToMessage("Id"));
            return result.GetContent<IBusinessResult<GetByIdProductResponse>>();
        }
    }
}
