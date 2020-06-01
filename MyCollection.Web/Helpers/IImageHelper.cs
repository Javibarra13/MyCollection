using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MyCollection.Web.Helpers
{
    public interface IImageHelper
    {
        Task<string> UploadImageAsync(IFormFile imageFile);

        Task<string> UploadCustomerImageAsync(IFormFile imageFile);

        Task<string> UploadProductImageAsync(IFormFile imageFile);
    }
}
