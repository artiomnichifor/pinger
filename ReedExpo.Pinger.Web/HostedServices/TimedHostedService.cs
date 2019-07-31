using Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HostedServices
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;

        //private readonly IServiceSite _serviceSite;
        private IList<SiteDto> _targets;


        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            // Create a new scope to retrieve scoped services
            using (var scope = _serviceProvider.CreateScope())
            {
                // Get the DbContext instance
                var _serviceSite = scope.ServiceProvider.GetRequiredService<IServiceSite>();
                 _targets = _serviceSite.GetAllSites();

                //Do the migration asynchronously
                //await myDbContext.Database.MigrateAsync();
            }

            

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            _logger.LogInformation("Timed Background Service is working.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var _serviceSite = scope.ServiceProvider.GetRequiredService<IServiceSite>();

                // iterate over the _targets, check if any timer is active now
                // if so, launch a background task(fire and forget) that does a http call into the target system
                // measures the response time, and records that to the db
                foreach (var target in _targets)
                {
                    TimeSpan timeout = TimeSpan.FromSeconds((int)target.PollingTime);
                    DateTime lastStartTime = (target.LastTimeChecked == null ? DateTime.Now : target.LastTimeChecked);
                    if (DateTime.Now - lastStartTime > timeout) // check if the target timer is active
                    {
                        // launch fire and forget task
                        await Task.Run(async () =>
                        {
                            var stopWatch = Stopwatch.StartNew();
                            using (var client = new HttpClient())
                            {
                                var response = await client.GetAsync(target.Url);
                                if (response.IsSuccessStatusCode)
                                {
                                    _logger.LogInformation($"{target.Url} is pinged");
                                    //will add constructor
                                    Site site = new Site() { Url = target.Url, PollingTime = target.PollingTime, ExpectedTime = target.ExpectedTime, LastCheckedTime = DateTime.Now };
                                    _serviceSite.EditSite(site, target.Id);
                                    target.LastTimeChecked = DateTime.Now;
                                }
                            }
                            // Start timespan
                            // do http call
                            // stop timestapn
                            // record to the db
                        });
                    }
                }
            }
            


        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
