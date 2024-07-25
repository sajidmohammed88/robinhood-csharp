using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Rb.Integration.Api.Abstractions;
using Rb.Integration.Api.Configurations;
using Rb.Integration.Api.Services;

namespace Rb.Integration.Api.Extensions;
public static class AppConfiguration
{
	public static void ConfigueRb(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddHttpClient()
			.Configure<AuthConfiguration>(configuration.GetSection(AuthConfiguration.Authentication));

		services.AddSingleton<SessionManager>()
			.AddSingleton<ISessionManager>(_ => _.GetRequiredService<SessionManager>())
			.AddSingleton<IHttpClientManager>(_ => _.GetRequiredService<SessionManager>())
			.AddSingleton<IQuoteDataService, QuoteDataService>()
			.AddSingleton<IOptionsInformationService, OptionsInformationService>()
			.AddSingleton<IOrderService, OrderService>()
			.AddSingleton<IRobinhood, Robinhood>()
			.AddSingleton<ICryptoCurrencyService, CryptoCurrencyService>()
			.AddSingleton<IPaginator, Paginator>();
	}
}
