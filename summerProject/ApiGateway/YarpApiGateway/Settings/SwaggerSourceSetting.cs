namespace YarpApiGatway.Settings
{
    public class SwaggerSourceSetting
    {
        public const string SectionName = "SwaggerSources";

        public string Catalog { get; set; } = string.Empty;
        public string Scheduling { get; set; } = string.Empty;
        public string Ordering { get; set; } = string.Empty;
    }
}
