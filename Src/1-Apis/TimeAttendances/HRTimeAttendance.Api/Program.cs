using HR.Common.Configurations.Options;
using HR.Common.Libs.Extensions;
using HRTimeAttendance.Api.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Host.IncludeOtherSettingFiles(new IncludeOtherSettingOptions
{
    IncludeUrlSettings = true
});
builder.Host.RegisterCompomentWithAutofac(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();
app.ConfigurationApplication();
app.Run();
