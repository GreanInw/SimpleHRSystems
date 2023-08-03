using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HR.Common.Libs.Swaggers.Filters
{
    public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths
                .ToDictionary(path =>
                {
                    return path.Key.Replace("v{version}"
                        , swaggerDoc.Info.Version.Replace("v", "").VersionFormatter());
                }, path => path.Value).ToList();

            swaggerDoc.Paths.Clear();
            paths.ForEach(path => swaggerDoc.Paths.Add(path.Key, path.Value));
        }
    }
}
