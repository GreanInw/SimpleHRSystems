using HR.Common.Models.Commons;
using Microsoft.EntityFrameworkCore;

namespace HR.Common.DbContexts.Extensions
{
    public static class CommonTableConfigurationExtensions
    {
        public static ModelBuilder ConfigurationCommonTables(this ModelBuilder model)
        {
            model.Entity<AppConfiguration>(t =>
            {
                t.HasIndex(t => t.Name).IsUnique()
                    .HasDatabaseName($"Idx_{nameof(AppConfiguration)}_Name");

                t.HasIndex(t => new { t.Name, t.IsActive }).IsUnique()
                    .HasDatabaseName($"Idx_{nameof(AppConfiguration)}_Name_IsActive");
            });

            model.Entity<Language>(t =>
            {
                t.HasIndex(t => t.Code).IsUnique()
                    .HasDatabaseName($"Idx_{nameof(Language)}_Code");
            });

            return model;
        }

        public static ModelBuilder InitializeDataCommonTables(this ModelBuilder model)
        {
            #region Seed data of Application Configuration
            var appConfigs = new List<AppConfiguration>();
            appConfigs.AddRange(GetAppConfigurationsOfPassword());
            appConfigs.AddRange(GetAppConfigurationsOfJwtConfigs());
            appConfigs.AddRange(GetAppConfigurationOfPasswordExired());

            model.Entity<AppConfiguration>().HasData(appConfigs);
            #endregion

            #region Seed data of Language
            model.Entity<Language>().HasData(new[]
            {
                new Language
                {
                    Id = 9,
                    Code = "en",
                    Name = "English",
                    IsActive = true,
                    IsDefault = true
                },
                new Language
                {
                    Id = 30,
                    Code = "th",
                    Name = "Thai",
                    IsActive = true,
                    IsDefault = false
                }
            });
            #endregion

            return model;
        }

        private static IEnumerable<AppConfiguration> GetAppConfigurationsOfPassword()
            => new List<AppConfiguration>
            {
                new AppConfiguration
                {
                    Id = Guid.Parse("3B97F7CD-F230-44EA-9E04-4624EF47D99D"),
                    Name = "PasswordMinimumLength",
                    Value = "6",
                    Descriptions = "Password Minimum Length",
                    IsActive = true
                },
                new AppConfiguration
                {
                    Id = Guid.Parse("3407D303-D67F-4319-A742-9095B7CBCED4"),
                    Name = "PasswordMaximumLength",
                    Value = "100",
                    Descriptions = "Password Maximum Length",
                    IsActive = true
                }
            };

        private static IEnumerable<AppConfiguration> GetAppConfigurationsOfJwtConfigs()
            => new List<AppConfiguration>
            {
                new AppConfiguration
                {
                    Id = Guid.Parse("BC1E15C7-2AA7-4E79-B06E-405706A234EE"),
                    Name = "JwtExpiresInOfMinutes",
                    Value = "60",
                    Descriptions = "Expired in of minutes of JWT",
                    IsActive = true
                },
                new AppConfiguration
                {
                    Id = Guid.Parse("3B326CFC-6C0B-4BF7-A23A-44D3CBB6237E"),
                    Name = "JwtRefreshTokenExpiryTimeOfMinutes",
                    Value = "120",
                    Descriptions = "Refresh Token Expiry Time Of Minutes",
                    IsActive = true
                }
            };

        private static IEnumerable<AppConfiguration> GetAppConfigurationOfPasswordExired()
            => new List<AppConfiguration>
            {
                new AppConfiguration
                {
                    Id = Guid.Parse("8655F15A-79AF-4E8A-8B2E-0CB271910A1C"),
                    Name = "EnablePasswordExpired",
                    Value = "true",
                    IsActive = true,
                    Descriptions = "Enable password expired."
                },
                new AppConfiguration
                {
                    Id = Guid.Parse("8BFB6C26-1AAD-4FD5-BAB4-743445C8C254"),
                    Name = "DaysOfPasswordExpired",
                    Value = "90",
                    IsActive = true,
                    Descriptions = "Days of password expired."
                },
                new AppConfiguration
                {
                    Id = Guid.Parse("2B2F630D-F415-4E32-91B4-DC6177AF9EA2"),
                    Name = "EnableForceChangePassword",
                    Value = "true",
                    IsActive = true,
                    Descriptions = "Enable force change password."
                }
            };
    }
}