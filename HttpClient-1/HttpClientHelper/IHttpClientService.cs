using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HttpClient_1.HttpClientHelper
{
    public interface IAuthenticationService
    {
        Task<HttpStatusCode> Delete<T>(string authType, T item, string Url);
        Task<T> Get<T>(string authType, string url);
        Task<List<T>> GetList<T>(string authType, string url);
        Task Post<T>(string authType, T item, string url);
        Task<T> Put<T>(string authType, T item, string url);
  

    }
}