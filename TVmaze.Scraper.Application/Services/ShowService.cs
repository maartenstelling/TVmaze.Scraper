using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVmaze.Scraper.Application.Interfaces.ApiClients;
using TVmaze.Scraper.Application.Interfaces.Repositories;
using TVmaze.Scraper.Application.Interfaces.Services;
using TVmaze.Scraper.Domain.Models;

namespace TVmaze.Scraper.Application.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowApiClient _showApiClient;
        private readonly IShowRepository _showRepository;

        public ShowService(IShowApiClient showApiClient, IShowRepository showRepository)
        {
            _showApiClient = showApiClient ?? throw new ArgumentNullException(nameof(showApiClient));
            _showRepository = showRepository ?? throw new ArgumentNullException(nameof(showRepository));
        }

        public IEnumerable<Show> GetShows(int pageNumber, int pageSize)
        {
            return _showRepository.Get(pageNumber, pageSize);
        }

        public async Task<IList<Show>> UpdateShowsAndCast()
        {
            var shows = await GetShowsFromApi();

            foreach (var show in shows.Take(1000))
            {
                show.Cast = await GetCastFromApi(show) ?? new List<Person>();
                await _showRepository.Create(show);
            }

            return shows;
        }

        private async Task<IList<Show>> GetShowsFromApi()
        {
            var shows = new List<Show>();
            var pageNumber = 0;

            while (true)
            {
                var showsForPage = await _showApiClient.GetShows(pageNumber);
                if (showsForPage == null) break;

                shows.AddRange(showsForPage);
                pageNumber++;
            }

            return shows;
        }

        private async Task<IList<Person>> GetCastFromApi(Show show)
        {
            return await _showApiClient.GetCast(show.Id);
        }
    }
}
