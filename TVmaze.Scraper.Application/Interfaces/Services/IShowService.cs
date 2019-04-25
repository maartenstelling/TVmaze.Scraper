using System.Collections.Generic;
using System.Threading.Tasks;
using TVmaze.Scraper.Domain.Models;

namespace TVmaze.Scraper.Application.Interfaces.Services
{
    public interface IShowService
    {
        IEnumerable<Show> GetShows(int pageNumber, int pageSize);

        Task<IList<Show>> UpdateShowsAndCast();
    }
}
