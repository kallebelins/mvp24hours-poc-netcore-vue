using Mvp24Hours.Core.Contract.ValueObjects.Logic;
using ProductAPI.Core.DTOs.Products;
using System.Threading.Tasks;

namespace ProductAPI.Core.Contract.Logic
{
    public interface IProductService
    {
        Task<IPagingResult<ListProductResponse>> List(IPagingCriteria criteria);
        Task<IBusinessResult<GetByIdProductResponse>> GetById(int id);
    }
}
