using HR.Common.Libs.Extensions;
using Identities.Api.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Host.IncludeOtherSettingFiles();
builder.Host.RegisterCompomentWithAutofac(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();
app.ConfigurationApplication();
app.Run();
