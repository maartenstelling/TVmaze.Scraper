using Polly;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TVmaze.Scraper.Application.Interfaces.ApiClients;
using TVmaze.Scraper.Domain.Models;
using TVmaze.Scraper.Infrastructure.Api;
using TVmaze.Scraper.Infrastructure.Entities;

namespace TVmaze.Scraper.Infrastructure.ApiClients
{
    public class ShowApiClient : IShowApiClient
    {
        private readonly IShowApi _showApi;

        public ShowApiClient(IShowApi showApi)
        {
            _showApi = showApi ?? throw new ArgumentNullException(nameof(showApi));
        }

        public async Task<IList<Show>> GetShows(int pageNumber)
        {
            var tooManyRequestsPolicy = Policy<IList<Show>>
                .Handle<ApiException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10) });

            var notFoundPolicy = Policy<IList<Show>>
                .Handle<ApiException>(ex => ex.StatusCode == HttpStatusCode.NotFound)
                .FallbackAsync(token => Task.FromResult((IList<Show>) null));
            
            var policyWrapper = Policy.WrapAsync(tooManyRequestsPolicy, notFoundPolicy);

            return await policyWrapper.ExecuteAsync(async () => await _showApi.Get(pageNumber));
        }

        public async Task<IList<Person>> GetCast(int id)
        {
            var tooManyRequestsPolicy = Policy<IList<CastEntity>>
                .Handle<ApiException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(new[] { TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10) });

            var notFoundPolicy = Policy<IList<CastEntity>>
                .Handle<ApiException>(ex => ex.StatusCode == HttpStatusCode.NotFound)
                .FallbackAsync(token => Task.FromResult((IList<CastEntity>) null));

            var policyWrapper = Policy.WrapAsync(tooManyRequestsPolicy, notFoundPolicy);
            var cast = await policyWrapper.ExecuteAsync(async () => await _showApi.GetCast(id));

            return cast.OrderByDescending(c => c.Person.Birthday).Select(c => c.Person).ToList();
        }
    }
}
