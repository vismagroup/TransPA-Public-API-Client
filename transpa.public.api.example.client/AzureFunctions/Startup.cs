using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TransPA.OpenSource.External.Datalon;

[assembly: FunctionsStartup(typeof(TransPA.OpenSource.Startup))]

namespace TransPA.OpenSource
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
            services.AddHttpClient();
            services.AddSingleton<IPublicApiClient, PublicApiClient>();
            services.AddSingleton<IDatalonApiClient, DatalonApiClient>();
            services.AddSingleton<SalaryConverter>();
            services.AddSingleton<EmployeeValidator>();
            services.AddSingleton<HttpObjectResultHelper>();
            services.AddSingleton<SalaryValidator>();
        }
    }
}