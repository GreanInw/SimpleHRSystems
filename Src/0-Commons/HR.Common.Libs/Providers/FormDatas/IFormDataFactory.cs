using HR.Common.Results;
using Microsoft.AspNetCore.Http;

namespace HR.Common.Libs.Providers.FormDatas
{
    public interface IFormDataFactory
    {
        ValueTask<ServiceResult> PostAsync<TRequest>(string requestUri, TRequest request = null
            , IEnumerable<IFormFile> files = null, IDictionary<string, string> headers = null)
            where TRequest : class, new();

        ValueTask<ServiceResult<TResponse>> PostAsync<TRequest, TResponse>(string requestUri, TRequest request = null
            , IEnumerable<IFormFile> files = null, IDictionary<string, string> headers = null)
            where TRequest : class, new() where TResponse : class, new();

        ValueTask<ServiceResult> PutAsync<TRequest>(string requestUri, TRequest request = null
            , IEnumerable<IFormFile> files = null, IDictionary<string, string> headers = null)
            where TRequest : class, new();

        ValueTask<ServiceResult<TResponse>> PutAsync<TRequest, TResponse>(string requestUri, TRequest request = null
            , IEnumerable<IFormFile> files = null, IDictionary<string, string> headers = null)
            where TRequest : class, new() where TResponse : class, new();
    }
}