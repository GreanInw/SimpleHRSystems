using HR.Common.Enums;
using HR.Common.Libs.Extensions;
using HR.Common.Models.HumanResources;
using Microsoft.EntityFrameworkCore;

namespace HR.Common.DbContexts.Extensions
{
    public static class HumanResourceTableConfigurationExtensions
    {
        public static ModelBuilder ConfigurationHRsTables(this ModelBuilder model)
        {
            model.Entity<Employee>(t =>
            {
                t.HasMany(f => f.EmployeeNamesInfos).WithOne(f => f.Employee).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(f => f.EmployeeAddresses).WithOne(f => f.Employee).OnDelete(DeleteBehavior.NoAction);
                t.HasMany(f => f.LeaveRequests).WithOne(f => f.Employee).OnDelete(DeleteBehavior.NoAction);
                t.HasOne(f => f.UserEmployee).WithOne(f => f.Employee).OnDelete(DeleteBehavior.NoAction);

                t.HasIndex(f => f.InternalId).IsUnique()
                 .HasDatabaseName("Idx_Employee_InternalId");
            });
            model.Entity<EmployeeAddress>(t =>
            {
                t.Property(f => f.Type)
                 .HasConversion(f => f.ToString(), t => t.ToEnum<AddressType>(true));
            });
            model.Entity<EmployeeNamesInfo>(t =>
            {
                t.HasKey(f => new { f.EmployeeId, f.LanguageId }).HasName("PK_EmployeeLanguageInfo");
            });
            model.Entity<UserEmployee>(t =>
            {
                t.HasKey(f => new { f.UserId, f.EmployeeId }).HasName("PK_UserEmployee");
            });
            model.Entity<LeaveTable>(t =>
            {
                t.HasMany(f => f.LeaveRequests).WithOne(f => f.LeaveTable)
                 .OnDelete(DeleteBehavior.NoAction);
                t.HasIndex(f => new { f.Name, f.LanguageId }).IsUnique()
                 .HasDatabaseName("Idx_LeaveTable_Name_LanguageId");
            });
            model.Entity<Department>(t =>
            {
                t.HasIndex(f => new { f.Name, f.LanguageId, f.ParentId }).IsUnique()
                 .HasDatabaseName("Idx_Department_Name_LanguageId_ParentId");
            });
            model.Entity<Position>(t =>
            {
                t.HasMany(f => f.Employees).WithOne(f => f.Position).OnDelete(DeleteBehavior.NoAction);
                t.HasIndex(f => new { f.Name, f.LanguageId }).IsUnique()
                 .HasDatabaseName("Idx_Position_Name_LanguageId");
            });
            model.Entity<ApprovalFlow>(t =>
            {
                t.HasKey(f => new { f.ParentId, f.ChildId, f.Type }).HasName("PK_ApprovalFlow");
                t.Property(f => f.Type)
                 .HasConversion(f => f.ToString(), t => t.ToEnum<ApprovalFlowType>(true));
            });

            model.Entity<SummaryLeave>(t =>
            {
                t.HasKey(f => new { f.EmployeeId, f.LeaveId }).HasName("PK_SummaryLeave");
            });

            model.Entity<Holiday>(t =>
            {
                t.HasIndex(f => new { f.LanguageId, f.Date }).IsUnique()
                 .HasDatabaseName("Idx_Holiday_LanguageId_Date");
            });

            return model;
        }
    }
}
