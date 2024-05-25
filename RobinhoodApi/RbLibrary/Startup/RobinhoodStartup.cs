using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RobinhoodApi.Abstractions;
using RobinhoodApi.Configurations;
using RobinhoodApi.Services;

namespace RobinhoodApi.Startup;

public static class RobinhoodStartup
{
	public static void Startup(IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient();

		services.Configure<RobinhoodConfiguration>(configuration.GetSection(RobinhoodConfiguration.Authentication));

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
