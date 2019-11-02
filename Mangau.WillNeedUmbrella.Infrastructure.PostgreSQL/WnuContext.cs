﻿using Mangau.WillNeedUmbrella.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Mangau.WillNeedUmbrella.Infrastructure
{
    public class WnuContext : WnuContextBase
    {
        public WnuContext(AppSettings appSettings) :
            base(appSettings)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("PostgreSQL"); // TODO: Load from configuration/settings file
        }
    }
}
