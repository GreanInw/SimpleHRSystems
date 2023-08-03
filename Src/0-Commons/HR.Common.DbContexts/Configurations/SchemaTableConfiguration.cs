using HR.Common.Libs.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.Common.DbContexts.Configurations
{
    /// <summary>
    /// Set schema of table in database.
    /// </summary>
    /// <typeparam name="TEntity">Type of class</typeparam>
    public class SchemaTableConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Database schema
        /// </summary>
        public string Schema { get; }

        /// <summary>
        /// Initial class schema table configuration
        /// </summary>
        /// <param name="schema">Database schema</param>
        public SchemaTableConfiguration(string schema)
        {
            if (schema.IsEmpty())
            {
                throw new ArgumentNullException(nameof(schema));
            }
            Schema = schema;
        }

        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var name = typeof(TEntity).Name;
            builder.ToTable(name, Schema);
        }
    }
}
