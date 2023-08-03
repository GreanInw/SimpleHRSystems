namespace HR.Common.Configurations.Options
{
    public class DbContextModuleOptions
    {
        public DbContextModuleOptions() { }

        public DbContextModuleOptions(string sectionName)
            => SectionName = sectionName;

        public string SectionName { get; set; }
    }
}