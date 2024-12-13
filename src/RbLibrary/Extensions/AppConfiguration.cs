namespace Rb.Integration.Api.Extensions;

public static class AppConfiguration
{
	public static void ConfigureRb(this IServiceCollection services, IConfigurationSection configurationSection)
	{
		services
			.AddHttpClient()
			.Configure<AuthConfiguration>(configurationSection);

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
