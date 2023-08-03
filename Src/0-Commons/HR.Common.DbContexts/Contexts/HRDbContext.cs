using HR.Common.DbContexts.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HR.Common.DbContexts.Contexts
{
    public class HRDbContext : DbContext, IHRDbContext
    {
        public HRDbContext(DbContextOptions<HRDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigurationSSOTables()
                .ConfigurationCommonTables()
                .ConfigurationHRsTables()
                .ConfigurationAuditableEntities();

            //Seed data.
            modelBuilder.InitializeDataCommonTables()
                .InitializeDataIdentityTables();
        }
    }
}