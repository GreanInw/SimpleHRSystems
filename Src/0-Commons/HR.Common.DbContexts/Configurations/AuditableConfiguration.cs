using HR.Common.Libs.Auditables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Common.DbContexts.Configurations
{
    public class AuditableConfiguration
    {
        public const int DefaultMaxLength = 256;
        public static IEnumerable<Type> AllowTypes => new[]
        {
            typeof(IAuditableEntity),
            typeof(ICreatedAuditableEntity)
        };

        public static void Configure(EntityTypeBuilder builder, Type type, int? maxLength = null)
        {
            var entityInterfaces = type.GetInterfaces();
            if (!entityInterfaces.Any(c => AllowTypes.Contains(c)))
            {
                throw new Exception($"Class : '{type.Name}' not inherited '{nameof(IAuditableEntity)}' or {nameof(ICreatedAuditableEntity)}.");
            }

            if (!maxLength.HasValue || (maxLength.HasValue && maxLength.Value < 1))
            {
                maxLength = DefaultMaxLength;
            }

            builder.Property(nameof(IAuditableEntity.CreatedBy)).HasMaxLength(maxLength.Value).IsRequired();
            builder.Property(nameof(IAuditableEntity.CreatedDate)).IsRequired();

            if (entityInterfaces.Any(w => w == typeof(IAuditableEntity)))
            {
                builder.Property(nameof(IAuditableEntity.ModifiedBy)).HasMaxLength(maxLength.Value).IsRequired();
                builder.Property(nameof(IAuditableEntity.ModifiedBy)).IsRequired();
            }
        }
    }

    public class AuditableConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IAuditableEntity
    {
        public AuditableConfiguration() : this(AuditableConfiguration.DefaultMaxLength) { }

        public AuditableConfiguration(int maxLength) => MaxLength = maxLength;

        public int MaxLength { get; }

        public void Configure(EntityTypeBuilder<TEntity> builder)
            => AuditableConfiguration.Configure(builder, typeof(TEntity), MaxLength);
    }
}