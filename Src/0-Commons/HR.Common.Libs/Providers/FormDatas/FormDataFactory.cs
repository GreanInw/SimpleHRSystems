using HR.Common.Libs.Extensions;
using HR.Common.Libs.Helpers;
using HR.Common.Libs.Jsons;
using HR.Common.Libs.Providers.Bases;
using HR.Common.Libs.Webs.Attributes;
using HR.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text;

namespace HR.Common.Libs.Providers.FormDatas
{
    public class FormDataFactory : BaseApiProvider, IFormDataFactory
    {
        public FormDataFactory(IHttpContextAccessor httpContextAccessor
            , IHttpClientFactory httpClientFactory) : base(httpContextAccessor, httpClientFactory)
        { }

        #region Post methods
        public async ValueTask<ServiceResult> PostAsync<TRequest>(string requestUri, TRequest request = null
            , IEnumerable<IFormFile> files = null, IDictionary<string, string> headers = null)
            where TRequest : class, new()
            => ServiceResult.Change(await SendInternalAsync<TRequest, object>(true, requestUri, request, files, headers));

        public async ValueTask<ServiceResult<TResponse>> PostAsync<TRequest, TResponse>(string requestUri, TRequest request = null
            , IEnumerable<IFormFile> files = null, IDictionary<string, string> headers = null)
            where TRequest : class, new()
            where TResponse : class, new()
            => await SendInternalAsync<TRequest, TResponse>(true, requestUri, request, files, headers);
        #endregion

        #region Put methods
        public async ValueTask<ServiceResult> PutAsync<TRequest>(string requestUri, TRequest request = null
            , IEnumerable<IFormFile> files = null, IDictionary<string, string> headers = null)
            where TRequest : class, new()
            => ServiceResult.Change(await SendInternalAsync<TRequest, object>(false, requestUri, request, files, headers));

        public async ValueTask<ServiceResult<TResponse>> PutAsync<TRequest, TResponse>(string requestUri, TRequest request = null
            , IEnumerable<IFormFile> files = null, IDictionary<string, string> headers = null)
            where TRequest : class, new()
            where TResponse : class, new()
         => await SendInternalAsync<TRequest, TResponse>(false, requestUri, request, files, headers);
        #endregion

        protected internal async ValueTask<ServiceResult<TResponse>> SendInternalAsync<TRequest, TResponse>(bool isPost, string requestUri
            , TRequest request = null, IEnumerable<IFormFile> files = null, IDictionary<string, string> headers = null)
            where TRequest : class, new()
            where TResponse : class, new()
        {
            if (requestUri.IsEmpty())
            {
                throw new ArgumentException(nameof(requestUri));
            }

            if (files.IsNullable() && request.IsNullable())
            {
                throw new ArgumentNullException($"{nameof(files)} and {nameof(request)}");
            }

            var contents = new MultipartFormDataContent();
            if (!files.IsNullable())
            {
                AddMultipartFormDataContentWithFormFiles(contents, files.ToArray());
            }

            if (!request.IsNullable())
            {
                AddMultipartFormDataContentWithObject(contents, request);
            }

            using (var client = HttpClientFactory.CreateClient())
            {
                if (headers is not null && headers.Any())
                {
                    AddHeaders(client, headers);
                }
                ForwardParameterInternal(client);

                var httpMethod = isPost ? HttpMethod.Post : HttpMethod.Put;
                var requestMessage = new HttpRequestMessage(httpMethod, new Uri(requestUri))
                {
                    Content = contents
                };

                var responseResult = await client.SendAsync(requestMessage);
                await RethrowExceptionAsync(responseResult);
                return await JsonHelpers.DeserializeToServiceResultAsync<TResponse>(responseResult);
            }
        }

        /// <summary>
        /// Add file into <seealso cref="MultipartFormDataContent"/>
        /// </summary>
        /// <param name="content">The content of request.</param>
        /// <param name="files">The list of file.</param>
        protected void AddMultipartFormDataContentWithFormFiles(MultipartFormDataContent content, params IFormFile[] files)
        {
            foreach (var file in files)
            {
                if (file == null)
                {
                    return;
                }

                var streamContent = new StreamContent(file.OpenReadStream());
                foreach (var header in file.Headers)//add header of file.
                {
                    streamContent.Headers.Add(header.Key, header.Value.AsEnumerable());
                }
                content.Add(streamContent, file.Name, file.FileName);
            }
        }

        /// <summary>
        /// Add request into <seealso cref="MultipartFormDataContent"/>
        /// </summary>
        /// <param name="content">The content of request</param>
        /// <param name="requests">The request parameters.</param>
        /// <exception cref="Exception"></exception>
        protected void AddMultipartFormDataContentWithObject(MultipartFormDataContent content, object requests)
        {
            Type requestType = requests.GetType();
            if (!requestType.IsClass || typeof(string) == requestType)
            {
                throw new Exception($"Function not support type : '{requestType.Name}'");
            }

            foreach (var property in requestType.GetProperties())
            {
                object value = property.GetValue(requests);
                if (value.IsNullable())
                {
                    continue;
                }

                var valueType = value.GetType();
                bool isCollection = valueType.IsCollectionType();

                //Support nested class. remark string is class.
                if (valueType.IsClass && valueType != typeof(string) && !isCollection)
                {
                    AddMultipartFormDataContentWithObject(content, value);
                }
                else if (isCollection && valueType.IsGenericType)//Support property is list or array or collection
                {
                    AddMultipartFormDataContentWithCollectionType(content, property, value);
                }
                else
                {
                    SetMultipartFormDataContent(content, property, requests);
                }
            }
        }

        private void AddMultipartFormDataContentWithCollectionType(MultipartFormDataContent content
            , PropertyInfo parentProperty, object value)
        {
            var sourceType = value.GetType();
            var sourceValues = value as IEnumerable<object>;
            if (sourceValues == null)
            {
                throw new Exception("Method is not support. Parameter is required type of collection or list or array.");
            }

            var variableType = CommonHelpers.GetVariableType(sourceType);
            if (!variableType.IsClass || variableType == typeof(string))
            {
                throw new Exception($"Type : '{variableType}' not support. Method required class.");
            }

            int index = 0;
            var sourceProperties = sourceValues.FirstOrDefault().GetType().GetProperties();
            var parentParamName = GetParameterName(parentProperty);
            foreach (var item in sourceValues)
            {
                foreach (var property in sourceProperties)
                {
                    SetMultipartFormDataContent(content, property, item, parentParamName, index);
                }
                index++;
            }
        }

        private void SetMultipartFormDataContent(MultipartFormDataContent content, PropertyInfo property, object data
            , string parentPropertyName = "", int index = -1)
        {
            string paramName = GetParameterName(property);
            string paramValue = property.GetValue(data)?.ToString() ?? "";

            if (index > -1 && !parentPropertyName.IsEmpty())
            {
                paramName = $"{parentPropertyName}[{index}].{paramName}";
            }

            content.Add(new StringContent(paramValue, Encoding.UTF8), paramName);
        }

        private string GetParameterName(PropertyInfo property)
        {
            var fromDataAttr = property.GetCustomAttribute<FromFormAttribute>();
            if (fromDataAttr is not null)
            {
                return fromDataAttr.Name;
            }

            var customFromDataAttr = property.GetCustomAttribute<SnakeCaseFromFormAttribute>();
            return customFromDataAttr.IsNullable() ? property.Name : customFromDataAttr.Name;
        }
    }
}