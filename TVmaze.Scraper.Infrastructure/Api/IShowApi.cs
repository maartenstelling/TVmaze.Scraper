using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;
using TVmaze.Scraper.Domain.Models;
using TVmaze.Scraper.Infrastructure.Entities;

namespace TVmaze.Scraper.Infrastructure.Api
{
    public interface IShowApi
    {
        [Get("/shows?page={pageNumber}")]
        Task<IList<Show>> Get(int pageNumber);

        [Get("/shows/{id}/cast")]
        Task<IList<CastEntity>> GetCast(int id);
    }
}
