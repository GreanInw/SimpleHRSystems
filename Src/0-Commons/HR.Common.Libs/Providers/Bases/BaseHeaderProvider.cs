using HR.Common.Constants;
using Microsoft.AspNetCore.Http;

namespace HR.Common.Libs.Providers.Bases
{
    public abstract class BaseHeaderProvider
    {
        protected BaseHeaderProvider(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IHttpContextAccessor HttpContextAccessor { get; }
        public HttpContext Context => HttpContextAccessor.HttpContext;
        protected internal IEnumerable<string> ForwardHeaderParameters => new[]
        {
            HeaderParameterConstants.IsTokenExpired, HeaderParameterConstants.XApiKey,
            HeaderParameterConstants.LanguageId, HeaderParameterConstants.Authorization
        };

        protected internal void AddAuthorizeInternal(HttpClient client, string token)
            => client?.DefaultRequestHeaders?.Add(HeaderParameterConstants.Authorization, token);

        protected internal void AddHeaders(HttpClient client, IDictionary<string, string> headers)
        {
            if (client is null || headers is null)
            {
                return;
            }

            foreach (var item in headers)
            {
                client.DefaultRequestHeaders.Add(item.Key, item.Value);
            }
        }

        protected internal void ForwardParameterInternal(HttpClient client)
        {
            if (client is null)
            {
                return;
            }

            var forwardParameters = Context.Request.Headers.Where(w => ForwardHeaderParameters.Contains(w.Key));
            foreach (var item in forwardParameters)
            {
                client.DefaultRequestHeaders.Add(item.Key, $"{item.Value}");
            }
        }

    }
}