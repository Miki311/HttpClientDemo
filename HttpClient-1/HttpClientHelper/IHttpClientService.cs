using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HttpClient_1.HttpClientHelper
{
    public interface IHttpClientService
    {
        Task<HttpStatusCode> DeleteItem<T>(T item, string Url);
        Task<T> GetItem<T>(string url);
        Task<List<T>> GetListItems<T>(string url);
        Task PostItem<T>(T item, string url);
        Task<T> PutItem<T>(T item, string url);
        Task SetToken();

    }
}