using Mangau.WillNeedUmbrella.Configuration;
using Mangau.WillNeedUmbrella.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mangau.WillNeedUmbrella.Web.Services
{
    public class LogoutExpiredBackgroundService : BackgroundService
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private AppSettings _appSettings;

        private IServiceProvider _serviceProvider;


        public LogoutExpiredBackgroundService(AppSettings appSettings, IServiceProvider serviceProvider)
            : base()
        {
            _appSettings = appSettings;

            _serviceProvider = serviceProvider;
        }

        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.Info("Logout Expired Sessions Background Service is starting.");
            var interval = Math.Min(3600, Math.Max(10, _appSettings.LogoutExpiredInterval)) * 1000;

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(interval, cancellationToken);

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var users = scope.ServiceProvider.GetRequiredService<IUserService>();

                        if (users != null)
                        {
                            await users.LogoutExpired(cancellationToken);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception ex)
                {
                    logger.Error(ex, $"Error occurred loggin out expired sessions.");
                }
            }

            logger.Info("Logout Expired Sessions Background Service is stopping.");
        }
    }
}
