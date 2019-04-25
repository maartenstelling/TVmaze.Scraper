using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVmaze.Scraper.Application.Interfaces.Repositories;
using TVmaze.Scraper.Domain.Models;
using TVmaze.Scraper.Persistence.Entities;

namespace TVmaze.Scraper.Persistence.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly TVmazeContext _tvmazeContext;
        private readonly IMapper _mapper;

        public ShowRepository(TVmazeContext tvmazeContext, IMapper mapper)
        {
            _tvmazeContext = tvmazeContext ?? throw new ArgumentNullException(nameof(tvmazeContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IList<Show> Get(int pageNumber, int pageSize)
        {
            return _mapper.Map<IList<Show>>(_tvmazeContext.Shows.Skip(pageNumber * pageSize).Take(pageSize));
        }

        public async Task Create(Show show)
        {
            var entity = _mapper.Map<ShowEntity>(show);

            await _tvmazeContext.Database.EnsureCreatedAsync();
            await _tvmazeContext.Shows.AddAsync(entity);
            await _tvmazeContext.SaveChangesAsync();
        }
    }
}
