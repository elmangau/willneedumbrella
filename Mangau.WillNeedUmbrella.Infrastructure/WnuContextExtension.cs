using Mangau.WillNeedUmbrella.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mangau.WillNeedUmbrella.Infrastructure
{
    public static class WnuContextExtension
    {
        public static bool AllMigrationsApplied(this WnuContextBase context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this WnuContextBase context)
        {
            if (!context.Cities.Any())
            {
                var cities = JsonConvert.DeserializeObject<List<City>>(File.ReadAllText("seed" + Path.DirectorySeparatorChar + "city.list.json"))
                    .Where(c => c.Country.Length <= 2 && c.Name.Length >= 2);

                context.Cities.AddRange(cities);
                context.SaveChanges();
            }
        }

        public static IEnumerable<TEntity> Pagination<TEntity>(this IEnumerable<TEntity> query, PageRequest pageRequest)
        {
            return query
                .Skip(pageRequest.PageIndex)
                .Take(pageRequest.Size);
        }

        public static IQueryable<TEntity> Pagination<TEntity>(this IQueryable<TEntity> query, PageRequest pageRequest)
        {
            return query
                .Skip(pageRequest.PageIndex)
                .Take(pageRequest.Size);
        }

        public static IEnumerable<TEntity> Distinct<TEntity>(this IEnumerable<TEntity> query, Func<TEntity, object> expr)
        {
            return query
                .Distinct(new GenericComparer<TEntity>(expr));
        }

        public static IQueryable<TEntity> Distinct<TEntity>(this IQueryable<TEntity> query, Func<TEntity, object> expr)
        {
            return query
                .Distinct(new GenericComparer<TEntity>(expr));
        }
    }
}
