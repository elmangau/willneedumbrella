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
            options.UseMySql("database=willneedumbrella;server=localhost;port=3306;user id=willneedumbrella;password=Prueba#8"); // TODO: Load from configuration/settings file
        }
    }
}
