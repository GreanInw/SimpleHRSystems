using HR.Common.Constants;
using HR.Common.Models.Identities;
using Microsoft.EntityFrameworkCore;

namespace HR.Common.DbContexts.Extensions
{
    public static class IdentityTableConfigurationExtensions
    {
        public static ModelBuilder ConfigurationSSOTables(this ModelBuilder builder)
        {
            builder.Entity<User>(t =>
            {
                t.HasAlternateKey(w => w.Username).HasName($"UK_{nameof(User.Username)}");
                t.HasOne(f => f.UserEmployee).WithOne(f => f.User).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<UserInRole>(t =>
            {
                t.HasKey(f => new { f.UserId, f.RoleId }).HasName($"PK_{nameof(UserInRole)}");
                t.HasOne(f => f.User).WithMany(f => f.UserInRoles).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(f => f.Role).WithMany(f => f.UserInRoles).OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Role>(t =>
            {
                t.HasAlternateKey(w => w.Name).HasName($"UK_{nameof(Role.Name)}");
            });

            return builder;
        }

        public static ModelBuilder InitializeDataIdentityTables(this ModelBuilder model)
        {
            model.InitializeRolesData();
            model.InitializeUserAdministrator();
            return model;
        }

        private static void InitializeUserAdministrator(this ModelBuilder model)
        {
            model.Entity<User>().HasData(new[]
            {
                new User
                {
                    Id = Guid.Parse("0ADB92B6-AD00-45D2-87F9-9E0843835646"),
                    Username = "admin",
                    //Raw password is : Admin@1234
                    Password = "4Tzm1sy7dcD0OrKKqQ/NkGsWVjORPjuqAguSuUHu5ybK7TrgZgQerH/eEmT61TFZqYGOUYtvdKJWYFBZ1OJ4vw==",
                    PasswordSalt = "OZesbfE9yolANogRH+ERrdShqarryvl/CGvVjBufsDqzcKWxVAxzRWqa8fG4dlnY1rh93r4h6kVKBk8LcyPwRQ==",
                    IsActive = true,
                    ModifiedBy = "System",
                    CreatedBy = "System",
                    CreatedDate = new DateTime(2023, 5, 1),
                    ModifiedDate = new DateTime(2023, 5, 1),
                }
            });
        }

        private static void InitializeAssignAdminRoleToUserAdmin(this ModelBuilder model)
        {
            model.Entity<UserInRole>().HasData(new[]
            {
                new UserInRole
                {
                    UserId = Guid.Parse("0ADB92B6-AD00-45D2-87F9-9E0843835646"),
                    RoleId = Guid.Parse("FCB21698-CC6B-46CA-80BB-5FF52712BD31")
                }
            });
        }

        private static void InitializeRolesData(this ModelBuilder model)
        {
            var roles = new List<Role>
            {
                new Role
                {
                    Id = Guid.Parse("44848FCC-DE53-47AF-8A43-BE578736CA52"),
                    Name = RolesConstants.SystemAdmins,
                    Description = "System Administrator",
                    IsActive = true,
                    CreatedDate = new DateTime(2023,3, 30),
                    ModifiedDate = new DateTime(2023,3, 30),
                    CreatedBy = "System",
                    ModifiedBy = "System"
                },
                new Role
                {
                    Id = Guid.Parse("FCB21698-CC6B-46CA-80BB-5FF52712BD31"),
                    Name = RolesConstants.Administrator,
                    Description = "Administrator",
                    IsActive = true,
                    CreatedDate = new DateTime(2023,3, 30),
                    ModifiedDate = new DateTime(2023,3, 30),
                    CreatedBy = "System",
                    ModifiedBy = "System"
                },
                new Role
                {
                    Id = Guid.Parse("4CFC7BEF-8149-49F9-AB45-030457C29C56"),
                    Name = RolesConstants.User,
                    Description = "User",
                    IsActive = true,
                    CreatedDate = new DateTime(2023,3, 30),
                    ModifiedDate = new DateTime(2023,3, 30),
                    CreatedBy = "System",
                    ModifiedBy = "System"
                },
                new Role
                {
                    Id = Guid.Parse("78AA239A-F26E-4675-AFDD-1DE2934A72AF"),
                    Name = RolesConstants.Manager,
                    Description = "Manager",
                    IsActive = true,
                    CreatedDate = new DateTime(2023,3, 30),
                    ModifiedDate = new DateTime(2023,3, 30),
                    CreatedBy = "System",
                    ModifiedBy = "System"
                },
            };

            model.Entity<Role>().HasData(roles);
        }
    }
}