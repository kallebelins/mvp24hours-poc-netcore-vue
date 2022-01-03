using CustomerAPI.Core.Contract.Logic;
using CustomerAPI.Core.Entities;
using CustomerAPI.Core.ValueObjects.Customers;
using Mvp24Hours.Application.Logic;
using Mvp24Hours.Core.Contract.Data;
using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using Mvp24Hours.Core.DTOs;
using Mvp24Hours.Core.Enums;
using Mvp24Hours.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CustomerAPI.Application.Logic
{
    public class CustomerService : RepositoryPagingServiceAsync<Customer, IUnitOfWorkAsync>, ICustomerService
    {
        #region [ Queries ]

        public async Task<IPagingResult<IList<GetByCustomerResponse>>> GetBy(GetByCustomerRequest filter, IPagingCriteria criteria)
        {
            try
            {
                Expression<Func<Customer, bool>> clause =
                    x => string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name);

                return await GetByWithPaginationAsync(clause, criteria)
                    .MapPagingToAsync<IList<Customer>, IList<GetByCustomerResponse>>();
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
                throw ex;
            }
        }

        public async Task<IBusinessResult<GetByIdCustomerResponse>> GetById(int id)
        {
            try
            {
                return await GetByIdAsync(id)
                    .MapBusinessToAsync<Customer, GetByIdCustomerResponse>();
            }
            catch (Exception ex)
            {
                Logging.Error(ex);
                throw ex;
            }
        }

        #endregion

        #region [ Commands ]

        public async Task<IBusinessResult<CreateCustomerResponse>> Create(CreateCustomerRequest dto)
        {
            var customer = dto.MapTo<Customer>();

            if (await this.AddAsync(customer) > 0)
            {
                return customer
                    .MapTo<CreateCustomerResponse>()
                    .ToBusiness();
                //.ToBusiness(
                //    "Operation performed successfully.".ToMessageResult("CustomerCreate", MessageType.Success)
                //);
            }
            return "An attempt to perform an operation failed."
                .ToMessageResult("CustomerCreate", MessageType.Error)
                .ToBusiness<CreateCustomerResponse>();
        }

        public async Task<IBusinessResult<VoidResult>> Update(int id, UpdateCustomerRequest dto)
        {
            var customer = dto.MapTo<Customer>();
            customer.Id = id;

            if (await this.ModifyAsync(customer) > 0)
            {
                return "Operation performed successfully."
                    .ToMessageResult("Update", MessageType.Success)
                    .ToBusiness<VoidResult>();
            }

            return "Customer not found."
                .ToMessageResult("Update", MessageType.Warning)
                .ToBusiness<VoidResult>();
        }

        public async Task<IBusinessResult<VoidResult>> Delete(int id)
        {
            if (await this.RemoveByIdAsync(id) > 0)
            {
                return "Operation performed successfully."
                    .ToMessageResult("Delete", MessageType.Success)
                    .ToBusiness<VoidResult>();
            }

            return "Customer not found."
                .ToMessageResult("Delete", MessageType.Warning)
                .ToBusiness<VoidResult>();
        }

        #endregion
    }
}
