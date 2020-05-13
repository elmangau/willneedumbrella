using Mangau.WillNeedUmbrella.Configuration;
using Mangau.WillNeedUmbrella.Entities;
using Mangau.WillNeedUmbrella.Infrastructure;
using Mangau.WillNeedUmbrella.Web.Clients;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Mangau.WillNeedUmbrella.Web.Services
{
    public class WeatherUpdateBackgroundService : BackgroundService
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private AppSettings _appSettings;

        private IServiceProvider _serviceProvider;


        public WeatherUpdateBackgroundService(AppSettings appSettings, IServiceProvider serviceProvider)
            : base()
        {
            _appSettings = appSettings;

            _serviceProvider = serviceProvider;
        }

        public async Task<bool> SendEmail(City city, DateTime time, IEnumerable<string> to)
        {
            if (to.Any())
            {
                using (var client = new SmtpClient())
                {
                    var creds = new NetworkCredential(_appSettings.SmtpUser, _appSettings.SmtpPassword);
                    var message = new MailMessage();

                    client.Host = _appSettings.SmtpHost;
                    client.Port = _appSettings.SmtpPort;
                    client.EnableSsl = true;
                    client.Credentials = creds;

                    message.From = new MailAddress(_appSettings.SmtpUser, "Mangau Need Umbrella");

                    foreach (var address in to)
                    {
                        message.Bcc.Add(address);
                    }

                    message.Subject = $"Will rain in {city.Name}";
                    message.Body = $"Hello:\n\nWill rain in {city.Name} at {time.ToLongTimeString()}.\n\nPlease be prepared with your umbrella!!.\n\nBest Regards.\n\nMangau Need Umbrella.";

                    await client.SendMailAsync(message);

                    return true;
                };
            }

            return false;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.Info("Weather Update Background Service is starting.");
            var interval = Math.Min(3600, Math.Max(10, _appSettings.WeatherUpdateInterval)) * 1000;

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var client = scope.ServiceProvider.GetRequiredService<IWeatherClient>();
                        var usersCities = scope.ServiceProvider.GetRequiredService<IUserCityService>();

                        if (usersCities != null)
                        {
                            var cities = await usersCities.GetAllCities(new PageRequest(), cancellationToken);

                            while (cities.HasContent)
                            {
                                foreach (var city in cities.Content)
                                {
                                    var weather = await client.GetWeather(city.Id, cancellationToken);

                                    foreach (var wd in weather.List)
                                    {
                                        var exit = false;

                                        foreach (var ww in wd.Weather)
                                        {
                                            if (ww.Main.Contains("rain", StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                var users = await usersCities.GetUsersByCity(new PageRequest(), city.Id, cancellationToken);

                                                while (users.HasContent)
                                                {
                                                    var emails = users.Content
                                                        .Where(u => !string.IsNullOrEmpty(u.Email) && u.Email.Length > 5)
                                                        .Select(u => $"{u.FirstName} {u.LastName}<{u.Email}>".Trim());

                                                    await SendEmail(city, wd.Dt_Txt, emails);

                                                    if (!users.HasNext)
                                                    {
                                                        break;
                                                    }

                                                    users = await usersCities.GetUsersByCity(users.NextPageRequest, city.Id, cancellationToken);
                                                }

                                                exit = true;

                                                break;
                                            }
                                        }

                                        if (exit)
                                        {
                                            break;
                                        }
                                    }

                                    await Task.Delay(90000, cancellationToken);
                                }

                                if (!cities.HasNext)
                                {
                                    break;
                                }

                                cities = await usersCities.GetAllCities(cities.NextPageRequest, cancellationToken);
                            }
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Error updating Weather forecast.");
                }
            }

            logger.Info("Weather Update Sessions Background Service is stopping.");
        }
    }
}
