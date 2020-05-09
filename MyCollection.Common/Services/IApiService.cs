using MyCollection.Common.Models;
using System.Threading.Tasks;

namespace MyCollection.Common.Services
{
    public interface IApiService
    {
        Task<Response<CollectorResponse>> GetCollectorByEmailAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, string email);
        Task<Response<TokenResponse>> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);

        Task<bool> CheckConnectionAsync(string url);
    }
}