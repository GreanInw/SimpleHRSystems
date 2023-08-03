using HR.Common.Constants;
using HR.Common.Libs.Extensions;
using HR.Common.Models.Commons;
using HR.Common.Results;
using HR.Common.Services.AppConfigurations.Helpers;
using HR.Common.Services.AppConfigurations.Queries;
using HR.Common.Services.Languages.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Reflection;
using System.Security.Claims;

namespace HR.Common.Services.Bases
{
    public abstract class BaseCommonService : IBaseService
    {
        protected BaseCommonService(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        protected IHttpContextAccessor HttpContextAccessor { get; }
        protected HttpContext Context => HttpContextAccessor?.HttpContext;
        protected ClaimsPrincipal CurrentUser => Context?.User;
        protected IConfiguration Configuration => Context?.RequestServices?.GetService<IConfiguration>();
        protected int CurrentLanguageId
        {
            get
            {
                return Context.Request.Headers.TryGetValue(HeaderParameterConstants.LanguageId, out StringValues value)
                    ? (int.TryParse(value, out int langId) ? langId : 0)
                    : 0;
            }
        }

        protected IAppConfigurationQueryService AppConfigurationQueryService
            => Context?.RequestServices?.GetService<IAppConfigurationQueryService>()
                ?? throw CreateNewException($"Cannot resolve : '{nameof(IAppConfigurationQueryService)}'.");

        protected ILanguageQueryService LanguageQueryService
            => Context?.RequestServices?.GetService<ILanguageQueryService>()
                ?? throw CreateNewException($"Cannot resolve : '{nameof(ILanguageQueryService)}'.");

        protected ValueTask<AppConfiguration> GetAppConfigurationsByNameInternalAsync(string name)
            => AppConfigurationQueryService is null
                ? throw new Exception($"Cannot resolve '{nameof(IAppConfigurationQueryService)}'.")
                : AppConfigurationQueryService.GetByNameAsync(name);

        protected ValueTask<IEnumerable<AppConfiguration>> GetAppConfigurationsByNamesInternalAsync(params string[] names)
            => AppConfigurationQueryService is null
                ? throw new Exception($"Cannot resolve '{nameof(IAppConfigurationQueryService)}'.")
                : AppConfigurationQueryService.GetByNamesAsync(names);

        protected async ValueTask<TConfigs> BindAppConfigurationsByNamesInternalAsync<TConfigs>()
            where TConfigs : class, new()
            => await AppConfigurationQueryService.BindConfigurationAsync<TConfigs>();

        protected TConfigs BindAppConfigurationsByNamesInternal<TConfigs>() where TConfigs : class, new()
            => BindAppConfigurationsByNamesInternalAsync<TConfigs>()
                .ConfigureAwait(false).GetAwaiter().GetResult();

        protected TConfigs BindConfigurations<TConfigs>(IEnumerable<AppConfiguration> configurations)
            where TConfigs : class, new()
            => AppConfigurationBinderHelper.Bind<TConfigs>(configurations);

        protected async ValueTask<Language> GetLanguageByIdInternalAsync(int id)
            => await LanguageQueryService.GetByAsync(id);

        protected async ValueTask<Language> GetLanguageByCodeInternalAsync(string code)
            => await LanguageQueryService.GetByCodeAsync(code);

        protected async ValueTask<bool> IsExistLanguageIdInternalAsync(int id)
            => await LanguageQueryService.AnyAsync(w => w.Id == id);

        protected async ValueTask<ServiceResult> ValidateLanguageIdInternalAsync(int languageId)
        {
            if (languageId < 1 || !await IsExistLanguageIdInternalAsync(languageId))
            {
                return new ServiceResult(ErrorMessageConstants.Commons.Languages.LanguageNotFound);
            }

            return new ServiceResult(true);
        }

        protected internal void DisposeWithProperties()
        {
            var properties = GetPropertyOrField<PropertyInfo>()
                .Where(w => w.PropertyType.GetInterface(nameof(IDisposable), true) is not null);

            foreach (var property in properties)
            {
                if (property.GetValue(this) is IDisposable disposable)
                {
                    disposable?.Dispose();
                }
            }
        }

        protected internal void DisposeWithFields()
        {
            var fields = GetPropertyOrField<FieldInfo>()
                .Where(w => w.DeclaringType.GetInterface(nameof(IDisposable), true) is not null);
            foreach (var field in fields)
            {
                if (field.GetValue(this) is IDisposable disposable)
                {
                    disposable?.Dispose();
                }
            }
        }

        protected internal Exception CreateNewException(string message)
            => new Exception(message);

        protected internal TOptions GetOptionsValueFromConfiguration<TOptions>(string section) where TOptions : class, new()
            => Configuration.GetValueBySection<TOptions>(section);

        private IEnumerable<T> GetPropertyOrField<T>() where T : class
        {
            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var type = GetType();

            if (typeof(T) == typeof(PropertyInfo))
            {
                return (IEnumerable<T>)type.GetProperties(flags).AsEnumerable();
            }
            else if (typeof(T) == typeof(FieldInfo))
            {
                return (IEnumerable<T>)type.GetFields(flags).AsEnumerable();
            }
            else
            {
                throw new Exception($"Method is support type '{nameof(PropertyInfo)}' or '{nameof(FieldInfo)}'");
            }
        }

    }
}