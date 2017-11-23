using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System.Web.Services.Description;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Web.Mvc;
using HttpClient_1.HttpClientHelper;

//[assembly: OwinStartupAttribute(typeof(HttpClient_1.App_Start.Startup))]
[assembly: OwinStartup(typeof(HttpClient_1.App_Start.Startup))]
namespace HttpClient_1.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            ConfigureServices(services);

            var resolver = new DefaultDependencyResolver(services.BuildServiceProvider());
            DependencyResolver.SetResolver(resolver);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersAsServices(typeof(Startup).Assembly.GetExportedTypes()
                   .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                   .Where(t => typeof(IController).IsAssignableFrom(t)
                            || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));

            services.AddSingleton<HttpContextBase>(provider =>
            {
                var context = HttpContext.Current;
                if (context == null) return new FakeHttpContext();
                return new HttpContextWrapper(context);
            });

            Uri baseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            services.AddSingleton<IHttpClientService>(provider => new HttpClientService(provider.GetService<HttpContextBase>(), baseAddress));
        }
    }
}