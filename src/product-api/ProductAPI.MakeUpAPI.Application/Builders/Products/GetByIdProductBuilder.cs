using Mvp24Hours.Core.Contract.Infrastructure.Pipe;
using ProductAPI.Core.Contract.Pipeline.Builders.Products;
using ProductAPI.MakeUpAPI.Application.Operations.Products;

namespace ProductAPI.MakeUpAPI.Application.Builders.Products
{
    public class GetByIdProductBuilder : IGetByIdProductBuilder
    {
        public IPipelineAsync Builder(IPipelineAsync pipeline)
        {
            return pipeline
                .AddAsync<SharedProductClientOperation>()
                .AddAsync<GetByIdProductMapperOperation>();
        }
    }
}
