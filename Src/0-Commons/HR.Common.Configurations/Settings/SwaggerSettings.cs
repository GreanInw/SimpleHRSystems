namespace HR.Common.Configurations.Settings
{
    public class SwaggerSettings
    {
        public SwaggerSettings()
        {
            Versions = new DocumentVersion[0];
        }

        public string DocumentTitle { get; set; }
        public bool EnableDocument { get; set; }
        public DocumentVersion[] Versions { get; set; }

        public class DocumentVersion
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Version { get; set; }
        }
    }
}
