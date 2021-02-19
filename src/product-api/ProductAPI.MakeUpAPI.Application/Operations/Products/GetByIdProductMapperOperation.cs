using Mvp24Hours.Core.Contract.Infrastructure.Pipe;
using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using Mvp24Hours.Infrastructure.Extensions;
using Mvp24Hours.Infrastructure.Pipe.Operations;
using ProductAPI.Core.DTOs.Products;
using ProductAPI.MakeUpAPI.Application.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.MakeUpAPI.Application.Operations.Products
{
    internal class GetByIdProductMapperOperation : OperationMapperAsync<IBusinessResult<GetByIdProductResponse>>
    {
        public override async Task<IBusinessResult<GetByIdProductResponse>> MapperAsync(IPipelineMessage input)
        {
            if (!input.HasContent<IEnumerable<Product>>() || !input.HasContent("Id"))
            {
                AddNotificationProductNotFound();
                return null;
            }

            var products = input?.GetContent<IEnumerable<Product>>();
            int productId = input.GetContent<int>("Id");

            var product = products.FirstOrDefault(x => x.Id == productId);

            if (product == null)
            {
                AddNotificationProductNotFound();
                return null;
            }

            var result = new GetByIdProductResponse()
            {
                Id = product.Id,
                Name = product.Name,
                ImageLink = product.ImageLink,
                Category = product.Category,
                Description = product.Description,
                Price = product.Price,
                ProductType = product.ProductType
            };

            return await Task.FromResult(result.ToBusiness());
        }

        private void AddNotificationProductNotFound()
        {
            NotificationContext.AddNotification("ProductGetByIdMapperOperation", "Product not found.");
        }
    }
}
