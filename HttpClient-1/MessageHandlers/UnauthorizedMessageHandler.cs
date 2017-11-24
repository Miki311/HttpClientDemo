using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace HttpClient_1.MessageHandlers
{
    public class UnauthorizedMessageHandler : DelegatingHandler
    {
        private const string Charset = "charset=utf-8";
        private const string ApplicationJson = "application/json";
        
        public UnauthorizedMessageHandler(){
            InnerHandler = new HttpClientHandler();
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", "dadasdasdsa");
            return base.SendAsync(request, cancellationToken);
        }
    }

    public class AuthorizedMessageHandler : UnauthorizedMessageHandler
    {
        private readonly HttpContextBase _httpContextBase = null;

        public AuthorizedMessageHandler(HttpContextBase httpContextBase)
        {
            InnerHandler = new HttpClientHandler();
            _httpContextBase = httpContextBase;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var token = _httpContextBase.Request.Cookies["Auth-Token"].Value;
            request.Headers.Add("Authorization", token);
            return base.SendAsync(request, cancellationToken);
        }

     
    }
}