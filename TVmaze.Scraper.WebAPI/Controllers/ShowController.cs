using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TVmaze.Scraper.Application.Interfaces.Services;
using TVmaze.Scraper.Domain.Models;

namespace TVmaze.Scraper.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IShowService _showService;

        public ShowController(IShowService showService)
        {
            _showService = showService ?? throw new ArgumentNullException(nameof(showService));
        }

        [HttpGet("getShows")]
        public IEnumerable<Show> GetShows([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return _showService.GetShows(pageNumber, pageSize);
        }
    }
}
