using System.Collections.Generic;
using System.Threading.Tasks;
using TVmaze.Scraper.Domain.Models;

namespace TVmaze.Scraper.Application.Interfaces.ApiClients
{
    public interface IShowApiClient
    {
        Task<IList<Show>> GetShows(int pageNumber);

        Task<IList<Person>> GetCast(int id);
    }
}
