using Mangau.WillNeedUmbrella.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mangau.WillNeedUmbrella.Web.Clients
{
    public static class Extension
    {
        public static IServiceCollection RegisterClients(this IServiceCollection services, AppSettings settings)
        {
            Func<HttpClientHandler> configHandler = () =>
            {
                return new HttpClientHandler()
                {
                    UseDefaultCredentials = false
                };
            };

            Action<HttpClient> configClient = client =>
            {
                client.BaseAddress = new Uri(settings.WeatherUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json; charset=utf-8");
                client.DefaultRequestHeaders.Add("User-Agent", "Mangau Need Umbrella");
            };

            services.AddHttpClient<IWeatherClient, OpenWeatherMapClient>(configClient)
                .ConfigurePrimaryHttpMessageHandler(configHandler)
                ;

            return services;
        }

    }
}
