using Mvp24Hours.Core.Contract.Infrastructure.Pipe;
using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using Mvp24Hours.Core.ValueObjects.Logic;
using Mvp24Hours.Infrastructure.Extensions;
using Mvp24Hours.Infrastructure.Pipe.Operations;
using ProductAPI.Core.DTOs.Products;
using ProductAPI.MakeUpAPI.Application.DTOs;
using ProductAPI.MakeUpAPI.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.MakeUpAPI.Application.Operations.Products
{
    internal class ListProductMapperOperation : OperationMapperAsync<IPagingResult<ListProductResponse>>
    {
        public override async Task<IPagingResult<ListProductResponse>> MapperAsync(IPipelineMessage input)
        {
            if (!input.HasContent<IEnumerable<Product>>() || !input.HasContent<IPagingCriteria>())
            {
                AddNotificationProductNotFound();
                return null;
            }

            var products = input.GetContent<IEnumerable<Product>>();
            var paging = input.GetContent<IPagingCriteria>();

            var items = new List<ListProductResponse>();

            int limit = ApiHelper.GetPagingLimit();
            if (paging.Limit > 0)
                limit = paging.Limit;

            var pagingProducts = products
                .Skip(limit * paging.Offset)
                .Take(limit);

            foreach (var item in pagingProducts)
            {
                items.Add(new ListProductResponse()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Category = item.Category,
                    ImageLink = item.ImageLink
                });
            }

            var totalCount = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / limit);

            var result = items.ToBusinessPaging(
                new PageResult(limit, paging.Offset, pagingProducts.Count()),
                new SummaryResult(totalCount, totalPages)
            );

            return await Task.FromResult(result);
        }

        private void AddNotificationProductNotFound()
        {
            NotificationContext.AddNotification("ProductGetByIdMapperOperation", "Product not found.");
        }
    }
}
