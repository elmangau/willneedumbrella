using Microsoft.EntityFrameworkCore;

namespace Mangau.WillNeedUmbrella.Infrastructure
{
    public class WnuContext : WnuContextBase
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=WillNeedUmbrella.db"); // TODO: Load from configuration/settings file
        }
    }
}
