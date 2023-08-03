using Microsoft.EntityFrameworkCore;

namespace HR.Common.EFCores.DbOptions
{
    public class DbOptionBuilders
    {
        public static DbContextOptionsBuilder<TDbContext> CreateDbContextOptionsBuilderByDbContext<TDbContext>(string connectionString)
            where TDbContext : DbContext
            => new DbContextOptionsBuilder<TDbContext>().UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure(3);
                builder.MigrationsAssembly(typeof(TDbContext).Assembly.GetName().Name);
            });

        public static DbContextOptions<TDbContext> CreateDbContextOptions<TDbContext>(string connectionString)
            where TDbContext : DbContext
            => CreateDbContextOptionsBuilderByDbContext<TDbContext>(connectionString).Options;
    }
}
