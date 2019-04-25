using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TVmaze.Scraper.Application.Interfaces.Services;

namespace TVmaze.Scraper.Functions
{
    public static class UpdateShowsAndCast
    {
        [FunctionName("UpdateShowsAndCast")]
        public static async Task Run([TimerTrigger("0 */10 * * * *")]TimerInfo myTimer,
            ILogger log,
            ExecutionContext context)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var serviceProvider = Startup.ConfigureServices(context);
            var showService = serviceProvider.GetService<IShowService>();

            await showService.UpdateShowsAndCast();
        }
    }
}
