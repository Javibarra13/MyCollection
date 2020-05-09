using MyCollection.Common.Models;
using System.Threading.Tasks;

namespace MyCollection.Common.Services
{
    public interface IApiService
    {
        Task<Response<CollectorResponse>> GetCollectorByEmail(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, string email);
        Task<Response<TokenResponse>> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request);
    }
}