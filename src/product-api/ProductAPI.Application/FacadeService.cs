using Mvp24Hours.Infrastructure.Helpers;
using ProductAPI.Core.Contract.Logic;

namespace ProductAPI.Application
{
    /// <summary>
    /// Provides all services available for use in this project
    /// </summary>
    public class FacadeService
    {
        #region [ Services ]
        /// <summary>
        /// <see cref="ProductAPI.Core.Contract.Logic.IProductService"/>
        /// </summary>
        public static IProductService ProductService
        {
            get { return HttpContextHelper.GetService<IProductService>(); }
        }
        #endregion
    }
}
