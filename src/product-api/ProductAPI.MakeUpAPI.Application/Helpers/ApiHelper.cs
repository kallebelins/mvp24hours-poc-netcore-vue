using Mvp24Hours.Core.Extensions;
using Mvp24Hours.Infrastructure.Helpers;

namespace ProductAPI.MakeUpAPI.Application.Helpers
{
    internal class ApiHelper
    {
        readonly static string SERVICE_URL = ConfigurationHelper.GetSettings("MakeUpAPI:ServiceUrl");
        readonly static string PAGING_LIMIT = ConfigurationHelper.GetSettings("MakeUpAPI:PagingLimit");
        public readonly static string ProductCacheKey = "products_webclient";

        public static string GetUrl()
        {
            return SERVICE_URL;
        }

        public static int GetPagingLimit()
        {
            return (int)PAGING_LIMIT.ToInt(20);
        }
    }
}
