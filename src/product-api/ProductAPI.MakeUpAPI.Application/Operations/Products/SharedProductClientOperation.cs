using Microsoft.Extensions.Caching.Memory;
using Mvp24Hours.Core.Contract.Infrastructure.Pipe;
using Mvp24Hours.Infrastructure.Helpers;
using Mvp24Hours.Infrastructure.Pipe.Operations;
using Newtonsoft.Json;
using ProductAPI.MakeUpAPI.Application.DTOs;
using ProductAPI.MakeUpAPI.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.MakeUpAPI.Application.Operations.Products
{
    internal class SharedProductClientOperation : OperationBaseAsync
    {
        private readonly IMemoryCache _cache;

        public SharedProductClientOperation()
            : base()
        {
            _cache = HttpContextHelper.GetService<IMemoryCache>();
        }

        public async override Task<IPipelineMessage> Execute(IPipelineMessage input)
        {
            IEnumerable<Product> products;

            if (!_cache.TryGetValue(ApiHelper.ProductCacheKey, out products))
            {
                var response = await WebRequestHelper.GetAsync($"{ApiHelper.GetUrl()}");
                products = JsonConvert.DeserializeObject<IEnumerable<Product>>(response);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(24));

                // Save data in cache.
                _cache.Set(ApiHelper.ProductCacheKey, products, cacheEntryOptions);
            }

            if (products != null)
            {
                input.AddContent(products);
            }

            return input;
        }
    }
}
