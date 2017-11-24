using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Net;
using HttpClient_1.MessageHandlers;
using System.ComponentModel;
using static HttpClient_1.AuthorisationTypes;

namespace HttpClient_1.HttpClientHelper
{
    

    public class AuthenticationService : IAuthenticationService
    {  
        private Dictionary<string, HttpClient> _httpClients = new Dictionary<string, HttpClient>();
       

        public AuthenticationService(HttpContextBase httpContextBase)
        {
            _httpClients.Add(HttpClientTypes.Unauthorized.GetDescription(), new HttpClient(new UnauthorizedMessageHandler()));
            _httpClients.Add(HttpClientTypes.AuthorizationToken.GetDescription(), new HttpClient(new AuthorizedMessageHandler(httpContextBase)));            
        }

        public async Task<T> Get<T>(string authType, string url)
        {
            HttpClient httpClient = _httpClients[authType];

            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Item = JsonConvert.DeserializeObject<T>(content);
                return Item;
            }
            throw new Exception(response.ReasonPhrase);
        }

        public async Task<List<T>> GetList<T>(string authType, string url)
        {
            HttpClient httpClient = _httpClients[authType];

            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<T>>(content);
                return Items;
            }
            throw new Exception(response.ReasonPhrase);
        }

        public async Task Post<T>(string authType, T item, string url)
        {
            HttpClient httpClient = _httpClients[authType];

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8);

            HttpResponseMessage response = null;
            response = await httpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new Exception(response.ReasonPhrase);
        }

        public async Task<T> Put<T>(string authType, T item, string url)
        {
            HttpClient httpClient = _httpClients[authType];

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8);

            HttpResponseMessage response = await httpClient.PutAsync(url, content);
          
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseItem = JsonConvert.DeserializeObject<T>(responseContent);
                return responseItem;
            }
            throw new Exception(response.ReasonPhrase);
        }

        public async Task<HttpStatusCode> Delete<T>(string authType, T item, string Url)
        {
            HttpClient httpClient = _httpClients[authType];

            var uri = new Uri(Url);

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8);

            HttpResponseMessage response = await httpClient.DeleteAsync(uri);
            return response.StatusCode;
        }
    }
}