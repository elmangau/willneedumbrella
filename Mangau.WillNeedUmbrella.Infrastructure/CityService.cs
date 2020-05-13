using Mangau.WillNeedUmbrella.Configuration;
using Mangau.WillNeedUmbrella.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mangau.WillNeedUmbrella.Infrastructure
{
    public interface ICityService
    {
        public Task<PageResponse<City>> GetAll(PageRequest pageRequest, CancellationToken cancellationToken);

        public Task<PageResponse<City>> GetAllByCountry(PageRequest pageRequest, string country, CancellationToken cancellationToken);

        public Task<PageResponse<City>> GetAllByName(PageRequest pageRequest, string name, CancellationToken cancellationToken);

        public Task<PageResponse<City>> GetAllByCountryAndName(PageRequest pageRequest, string country, string name, CancellationToken cancellationToken);
    }

    public class CityService : ICityService
    {
        private AppSettings _appSettings;
        private WnuContextBase _wnuContext;

        public CityService(AppSettings appSettings, WnuContextBase wnuContext)
        {
            _appSettings = appSettings;
            _wnuContext = wnuContext;
        }

        public async Task<PageResponse<City>> GetAll(PageRequest pageRequest, CancellationToken cancellationToken)
        {
            var totalCount = await _wnuContext.Cities.CountAsync(cancellationToken);
            var content = await _wnuContext.Cities
                .OrderBy(c => c.Name)
                .Skip(pageRequest.PageIndex)
                .Take(pageRequest.Size)
                .ToListAsync(cancellationToken);

            return new PageResponse<City>(pageRequest, totalCount, content);
        }

        public async Task<PageResponse<City>> GetAllByCountry(PageRequest pageRequest, string country, CancellationToken cancellationToken)
        {
            var query = _wnuContext.Cities
                .Where(c => c.Country.Equals(country));

            var totalCount = await query
                .CountAsync(cancellationToken);
            var content = await query
                .OrderBy(c => c.Name)
                .Pagination(pageRequest)
                .ToListAsync(cancellationToken);

            return new PageResponse<City>(pageRequest, totalCount, content);
        }

        public async Task<PageResponse<City>> GetAllByName(PageRequest pageRequest, string name, CancellationToken cancellationToken)
        {
            var query = _wnuContext.Cities
                .Where(c => c.Name.Contains(name));

            var totalCount = await query
                .CountAsync(cancellationToken);
            var content = await query
                .OrderBy(c => c.Name)
                .Pagination(pageRequest)
                .ToListAsync(cancellationToken);

            return new PageResponse<City>(pageRequest, totalCount, content);
        }

        public async Task<PageResponse<City>> GetAllByCountryAndName(PageRequest pageRequest, string country, string name, CancellationToken cancellationToken)
        {
            var query = _wnuContext.Cities
                .Where(c => c.Country.Equals(country))
                .Where(c => c.Name.Contains(name));

            var totalCount = await query
                .CountAsync(cancellationToken);
            var content = await query
                .OrderBy(c => c.Name)
                .Pagination(pageRequest)
                .ToListAsync(cancellationToken);

            return new PageResponse<City>(pageRequest, totalCount, content);
        }
    }
}
