using CustomerAPI.Core.ValueObjects.Contacts;
using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using Mvp24Hours.Core.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAPI.Core.Contract.Logic
{
    public interface ICustomerContactService
    {
        Task<IBusinessResult<int>> Create(CreateContactRequest dto, int customerId, CancellationToken cancellationToken = default);
        Task<IBusinessResult<VoidResult>> Update(int id, UpdateContactRequest dto, int customerId, CancellationToken cancellationToken = default);
        Task<IBusinessResult<VoidResult>> Delete(int id, int customerId, CancellationToken cancellationToken = default);
    }
}
