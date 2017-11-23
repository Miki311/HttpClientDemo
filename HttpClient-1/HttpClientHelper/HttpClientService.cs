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

namespace HttpClient_1.HttpClientHelper
{
    

    public class HttpClientService :  IHttpClientService
    {

        //enum (Unauthorized, token_auth...)

        //Dictionary<string, httpclient> httpclients

        //gethttpclient(enum)
 
        private const Int32 TimeOut = 15000;
        private const Int32 BufferSize = 256000;
        //private static readonly HttpClientService instance = null;
        private static HttpContextBase _httpContextBase;
        private static HttpClient _hp;
        private HttpMessageHandler hmh;

        public HttpClientService(HttpContextBase httpContextBase, Uri baseAddress, HttpMessageHandler _hmh)  
        {
            hmh.
            _hp = new HttpClient(hmh);
            _httpContextBase = httpContextBase;
            _hp.BaseAddress = baseAddress;
            _hp.Timeout = TimeSpan.FromMilliseconds(TimeOut);
            _hp.MaxResponseContentBufferSize = BufferSize;
            _hp.DefaultRequestHeaders.Accept.Clear();
            _hp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _hp.DefaultRequestHeaders.Add("jwt", "auth=" + _httpContextBase.Request.Cookies["jwt"]?.Value);
             
        }

        public HttpClientService(HttpMessageHandler hmh)
        {
            _hp = new HttpClient();
        }

        public HttpClientService(HttpContextBase httpContextBase, Uri baseAddress)
        {
            _hp = new HttpClient();
            _httpContextBase = httpContextBase;
            InitHttpClient(baseAddress);

        }

        private static void InitHttpClient(Uri baseAddress)
        {
            _hp.BaseAddress = baseAddress;
            _hp.Timeout = TimeSpan.FromMilliseconds(TimeOut);
            _hp.MaxResponseContentBufferSize = BufferSize;
            _hp.DefaultRequestHeaders.Accept.Clear();
            _hp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _hp.DefaultRequestHeaders.Add("Cookie", "auth=" + _httpContextBase.Request.Cookies["AuthToken"]?.Value);
        }

        public async Task<List<T>> GetListItems<T>(string url)
        {
            var response = await _hp.GetAsync((url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<T>>(content);
                return Items;
            }
            throw new Exception(response.ReasonPhrase);
        }

        public async Task<T> GetItem<T>(string url)
        {
            var response = await GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Item = JsonConvert.DeserializeObject<T>(content);
                return Item;
            }
            throw new Exception(response.ReasonPhrase);
        }

        public async Task PostItem<T>(T item, string url)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8);

            HttpResponseMessage response = null;
            response = await PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new Exception(response.ReasonPhrase);
        }

        public async Task<T> PutItem<T>(T item, string url)
        {

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8);

            HttpResponseMessage response = await PutAsync(url, content);
          
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseItem = JsonConvert.DeserializeObject<T>(responseContent);
                return responseItem;
            }
            throw new Exception(response.ReasonPhrase);
        }

        public async Task<HttpStatusCode> DeleteItem<T>(T item, string Url)
        {
            var uri = new Uri(Url);

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8);

            HttpResponseMessage response = await DeleteAsync(uri);
            return response.StatusCode;
        }

        public async Task SetToken()
        {
            await Task.CompletedTask;
        }
    }
}