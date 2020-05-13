using Mangau.WillNeedUmbrella.Configuration;
using Mangau.WillNeedUmbrella.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mangau.WillNeedUmbrella.Web.Clients
{
    public interface IWeatherClient
    {
        public Task<Weather> GetWeather(int cityId, CancellationToken cancellationToken);
    }

    public class OpenWeatherMapClient : IWeatherClient
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly HttpClient _client;

        private readonly AppSettings _settings;

        public OpenWeatherMapClient(HttpClient client, AppSettings settings)
        {
            _client = client;

            _settings = settings;
        }

        public async Task<Weather> GetWeather(int cityId, CancellationToken cancellationToken)
        {
            Weather res = null;

            try
            {
                var response = await _client.GetAsync($"?appid={_settings.WeatherKey}&lang=en&cnt=8&id={cityId}", cancellationToken);
                var strres = await response.Content.ReadAsStringAsync();
                logger.Info($"OWM RS: {strres}");

                res = JsonConvert.DeserializeObject<Weather>(strres);
            }
            catch (Exception ex)
            {
                res = null;
                logger.Error(ex, $"Test ER: {ex.Message}");
            }

            return res;
        }
    }
}
