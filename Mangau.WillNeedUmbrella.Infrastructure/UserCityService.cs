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
    public interface IUserCityService
    {
        public Task<PageResponse<UserCity>> GetAll(PageRequest pageRequest, CancellationToken cancellationToken);

        public Task<PageResponse<City>> GetAllCities(PageRequest pageRequest, CancellationToken cancellationToken);

        public Task<PageResponse<User>> GetUsersByCity(PageRequest pageRequest, int cityId, CancellationToken cancellationToken);
    }

    public class UserCityService : IUserCityService
    {
        private AppSettings _appSettings;
        private WnuContextBase _wnuContext;

        public UserCityService(AppSettings appSettings, WnuContextBase wnuContext)
        {
            _appSettings = appSettings;
            _wnuContext = wnuContext;
        }

        public async Task<PageResponse<UserCity>> GetAll(PageRequest pageRequest, CancellationToken cancellationToken)
        {
            var totalCount = await _wnuContext.UsersCities.CountAsync(cancellationToken);
            var content = await _wnuContext.UsersCities
                .Pagination(pageRequest)
                .ToListAsync(cancellationToken);

            return new PageResponse<UserCity>(pageRequest, totalCount, content);
        }

        public async Task<PageResponse<City>> GetAllCities(PageRequest pageRequest, CancellationToken cancellationToken)
        {
            var query = _wnuContext.UsersCities
                .Select(uc => uc.City)
                .Distinct();

            var totalCount = await query
                .CountAsync(cancellationToken);
            var content = await query
                .Pagination(pageRequest)
                .ToListAsync(cancellationToken);

            return new PageResponse<City>(pageRequest, totalCount, content);
        }

        public async Task<PageResponse<User>> GetUsersByCity(PageRequest pageRequest, int cityId, CancellationToken cancellationToken)
        {
            var query = _wnuContext.UsersCities
                .Where(uc => uc.CityId == cityId)
                .Select(uc => uc.User)
                .Distinct();

            var totalCount = await query
                .CountAsync(cancellationToken);
            var content = await query
                .Pagination(pageRequest)
                .ToListAsync(cancellationToken);

            return new PageResponse<User>(pageRequest, totalCount, content);
        }
    }
}
