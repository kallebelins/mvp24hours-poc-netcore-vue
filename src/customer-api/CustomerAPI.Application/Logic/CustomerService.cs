using CustomerAPI.Core.Contract.Logic;
using CustomerAPI.Core.DTOs.Creates;
using CustomerAPI.Core.DTOs.Details;
using CustomerAPI.Core.DTOs.Filters;
using CustomerAPI.Core.DTOs.Lists;
using CustomerAPI.Core.DTOs.Updates;
using CustomerAPI.Core.Entities;
using CustomerAPI.Infrastructure.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Mvp24Hours.Business.Logic;
using Mvp24Hours.Core.Contract.Data;
using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using Mvp24Hours.Core.DTOs;
using Mvp24Hours.Core.Enums;
using Mvp24Hours.Core.ValueObjects.Logic;
using Mvp24Hours.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerAPI.Application.Logic
{
    public class CustomerService : RepositoryPagingServiceAsync<Customer, IUnitOfWorkAsync>, ICustomerService
    {
        #region [ Fields ]
        private readonly IMemoryCache _cache;
        private readonly string keyGetByCache = "customer_getby_{0}_{1}";
        private readonly string keyGetByIdCache = "customer_getbyid_{0}";
        #endregion

        #region [ Ctors ]
        public CustomerService(IMemoryCache cache)
        {
            _cache = cache;
        }
        #endregion

        #region [ Queries ]

        #region [ GetBy ]
        public async Task<IPagingResult<GetByCustomerResponse>> GetBy(GetByCustomerRequest filter, IPagingCriteria criteria)
        {
            #region [ Config Cache ]
            string keyCache = string.Format(keyGetByCache, filter.Name, criteria?.GetHashCode() ?? 0);
            TimeSpan expirationCache = TimeSpan.FromHours(1);
            #endregion

            if (!_cache.TryGetValue(keyCache, out IPagingResult<GetByCustomerResponse> result))
            {
                result = await GetByCache(filter, criteria);

                if (!result.HasErrors)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(expirationCache);

                    // Save data in cache.
                    _cache.Set(keyCache, result, cacheEntryOptions);
                }
            }

            return result;
        }

        private async Task<IPagingResult<GetByCustomerResponse>> GetByCache(GetByCustomerRequest filter, IPagingCriteria criteria)
        {
            try
            {
                int limit = MaxQtyByQueryPage;
                int offset = 0;

                if (criteria != null)
                {
                    limit = criteria.Limit > 0 ? criteria.Limit : limit;
                    offset = criteria.Offset;
                }

                var repo = UnitOfWork.GetRepositoryAsync<Customer>();

                // apply filter
                Expression<Func<Customer, bool>> clause =
                    x => string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name);

                // get items
                var items = await repo.GetByAsync(clause, criteria);

                // summary results
                var totalCount = await repo.GetByCountAsync(clause);
                var totalPages = (int)Math.Ceiling((double)totalCount / limit);

                var dtos = items.MapTo<IList<GetByCustomerResponse>>();

                var result = await dtos.ToBusinessPagingAsync(
                    new PageResult(limit, offset, items.Count),
                    new SummaryResult(totalCount, totalPages)
                );

                return result;
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
                throw ex;
            }
        }

        private void RemoveGetByCache()
        {
            foreach (string key in _cache.GetKeys())
            {
                if (key.StartsWith("customer_getby_"))
                    _cache.Remove(key);
            }
        }

        #endregion

        #region [ GetById ]

        public async Task<IBusinessResult<GetByIdCustomerResponse>> GetById(int id)
        {
            #region [ Config Cache ]
            string keyCache = string.Format(keyGetByIdCache, id);
            TimeSpan expirationCache = TimeSpan.FromMinutes(20);
            #endregion

            if (!_cache.TryGetValue(keyCache, out IBusinessResult<GetByIdCustomerResponse> result))
            {
                result = await GetByIdCache(id);

                if (!result.HasErrors)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(expirationCache);

                    // Save data in cache.
                    _cache.Set(keyCache, result, cacheEntryOptions);
                }
            }

            if (!result.Data.AnyOrNotNull())
            {
                return BusinessResult<GetByIdCustomerResponse>.Create(
                    "Customer not found.".ToMessageResult("GetById", MessageType.Warning)
                );
            }

            return result;
        }

        private async Task<IBusinessResult<GetByIdCustomerResponse>> GetByIdCache(int id)
        {
            try
            {
                var repo = UnitOfWork.GetRepositoryAsync<Customer>();
                var item = await repo.GetByIdAsync(id);
                return item
                    .MapTo<GetByIdCustomerResponse>()
                    .ToBusiness();
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
                throw ex;
            }
        }

        private void RemoveGetByIdCache(int id)
        {
            _cache.Remove(string.Format(keyGetByIdCache, id));
        }

        #endregion

        #endregion

        #region [ Commands ]

        public async Task<IBusinessResult<CreateCustomerResponse>> Create(CreateCustomerRequest dto)
        {
            var customer = dto.MapTo<Customer>();

            if (await this.AddAsync(customer) > 0)
            {
                RemoveGetByCache();

                return customer
                    .MapTo<CreateCustomerResponse>()
                    .ToBusinessWithMessage(
                        "Operation performed successfully.".ToMessageResult("CustomerCreate", MessageType.Success)
                    );
            }
            return BusinessResult<CreateCustomerResponse>.Create(
                "An attempt to perform an operation failed.".ToMessageResult("CustomerCreate", MessageType.Error)
            );
        }

        public async Task<IBusinessResult<VoidResult>> Update(int id, UpdateCustomerRequest dto)
        {
            var customer = dto.MapTo<Customer>();
            customer.Id = id;

            if (await this.ModifyAsync(customer) > 0)
            {
                RemoveGetByIdCache(id);
                RemoveGetByCache();

                return BusinessResult<VoidResult>.Create(
                    "Operation performed successfully.".ToMessageResult("Update", MessageType.Success)
                );
            }

            return BusinessResult<VoidResult>.Create(
                "Customer not found.".ToMessageResult("Update", MessageType.Warning)
            );
        }

        public async Task<IBusinessResult<VoidResult>> Delete(int id)
        {
            if (await this.RemoveByIdAsync(id) > 0)
            {
                RemoveGetByIdCache(id);
                RemoveGetByCache();

                return BusinessResult<VoidResult>.Create(
                    "Operation performed successfully.".ToMessageResult("Delete", MessageType.Success)
                );
            }

            return BusinessResult<VoidResult>.Create(
                "Customer not found.".ToMessageResult("Delete", MessageType.Warning)
            );
        }

        #endregion
    }
}
