using System.Collections.Generic;
using System.Threading.Tasks;
using TVmaze.Scraper.Domain.Models;

namespace TVmaze.Scraper.Application.Interfaces.Repositories
{
    public interface IShowRepository
    {
        IList<Show> Get(int pageNumber, int pageSize);

        Task Create(Show show);
    }
}
