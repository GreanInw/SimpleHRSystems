using HR.Common.Libs.Extensions;
using HR.Common.Results.Responses;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace HR.Common.Libs.Providers.Bases
{
    public abstract class BaseApiProvider : BaseHeaderProvider
    {
        protected BaseApiProvider(IHttpContextAccessor httpContextAccessor
            , IHttpClientFactory httpClientFactory) : base(httpContextAccessor)
        {
            HttpClientFactory = httpClientFactory;
        }

        protected IHttpClientFactory HttpClientFactory { get; }

        /// <summary>
        /// Re-throw exception from <seealso cref="HttpResponseMessage"/>.
        /// Validation status from <seealso cref="HttpResponseMessage.IsSuccessStatusCode"/>.
        /// </summary>
        /// <param name="responseMessage">The http response message.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Throw excception message</exception>
        protected async Task RethrowExceptionAsync(HttpResponseMessage responseMessage)
        {
            if ((int)responseMessage.StatusCode >= 200 && (int)responseMessage.StatusCode <= 401)
            {
                return;
            }

            string messageResponse = await responseMessage.Content.ReadAsStringAsync();
            var exceptionResponse = GetTryBaseErrorResponse(messageResponse);
            if (!exceptionResponse.IsNullable())
            {
                throw new Exception($"{nameof(exceptionResponse.TraceId)} : '{exceptionResponse.TraceId}'. {exceptionResponse.Title}");
            }
            else
            {
                string content = messageResponse.IsEmpty() ? "" : $"Content : {messageResponse}.";
                string code = $"{nameof(responseMessage.StatusCode)} : {responseMessage.StatusCode}.";
                string reasonPhrase = $"{nameof(responseMessage.ReasonPhrase)} : {responseMessage.ReasonPhrase}.";
                throw new Exception($"{code} {reasonPhrase} {content}");
            }
        }

        private ExceptionBaseResponse GetTryBaseErrorResponse(string message)
        {
            try
            {
                return JsonSerializer.Deserialize<ExceptionBaseResponse>(message);
            }
            catch
            {
                return null;
            }
        }

    }
}