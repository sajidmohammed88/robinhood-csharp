using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Rb.Integration.Api;
using Rb.Integration.Api.Abstractions;
using Rb.Integration.Api.Configurations;
using Rb.Integration.Api.Services;

namespace Rb.Integration.Api.Startup;

public static class ConfigurationStartup
{
	public static void Startup(IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient();

		services.Configure<AuthConfiguration>(configuration.GetSection(AuthConfiguration.Authentication));

		services.AddSingleton<SessionManager>();
		services.AddSingleton<ISessionManager>(_ => _.GetRequiredService<SessionManager>());
		services.AddSingleton<IHttpClientManager>(_ => _.GetRequiredService<SessionManager>());
		services.AddSingleton<IQuoteDataService, QuoteDataService>();
		services.AddSingleton<IOptionsInformationService, OptionsInformationService>();
		services.AddSingleton<IOrderService, OrderService>();
		services.AddSingleton<IRobinhood, Robinhood>();
		services.AddSingleton<ICryptoCurrencyService, CryptoCurrencyService>();
		services.AddSingleton<IPaginator, Paginator>();
	}
}
