using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace HttpClient_1
{
    public static class AuthorisationTypes
    {
        public enum HttpClientTypes
        {
            [Description("Unauthorized")]
            Unauthorized,

            [Description("AuthorizationToken")]
            AuthorizationToken
        };

    }
}