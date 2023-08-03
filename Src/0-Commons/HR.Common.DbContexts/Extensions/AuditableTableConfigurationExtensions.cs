using HR.Common.DbContexts.Configurations;
using HR.Common.Libs.Auditables;
using HR.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HR.Common.DbContexts.Extensions
{
    public static class AuditableTableConfigurationExtensions
    {
        public static ModelBuilder ConfigurationAuditableEntities(this ModelBuilder builder)
        {
            var entityTypes = new ModelAssembly().CurrentAssembly.GetTypes()
                .Where(w => w.GetInterface(nameof(IAuditableEntity), true) is not null);

            foreach (var type in entityTypes)
            {
                AuditableConfiguration.Configure(builder.Entity(type), type);
            }

            return builder;
        }
    }
}
