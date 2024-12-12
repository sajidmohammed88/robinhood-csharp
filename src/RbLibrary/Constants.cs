namespace Rb.Integration.Api;

/// <summary>
/// The constants used in the Library.
/// </summary>
internal static class Constants
{
	internal static class Routes
	{
		//Base
		private const string _rbApiBaseUrl = "https://api.robinhood.com/";
		private const string _rbCryptoCurrencyBaseUrl = "https://nummus.robinhood.com/";
		private const string _ordersBase = "orders/";
		private const string _accountsBase = "accounts/";

		//Crypto
		internal const string CurrencyPairs = _rbCryptoCurrencyBaseUrl + "currency_pairs";
		internal const string NummusOrders = _rbCryptoCurrencyBaseUrl + _ordersBase;
		internal const string OrderStatus = NummusOrders + "{0}/";
		internal const string NummusAccounts = _rbCryptoCurrencyBaseUrl + _accountsBase;
		internal const string NummusHistoricals = MarketDataBase + "forex/historicals/{0}/?interval={1}&span={2}&bounds={3}";
		internal const string Holdings = _rbCryptoCurrencyBaseUrl + "holdings/";
		internal const string NummusQuotes = MarketDataBase + "forex/quotes/{0}/";

		//General
		internal const string Accounts = _rbApiBaseUrl + _accountsBase;
		internal const string Orders = _rbApiBaseUrl + _ordersBase;
		internal const string Portfolios = _rbApiBaseUrl + "portfolios/";
		internal const string Positions = _rbApiBaseUrl + "positions/";
		internal const string TagsBase = _rbApiBaseUrl + "midlands/tags/tag/";
		internal const string WatchLists = _rbApiBaseUrl + "watchlists/";
		internal const string MarketDataBase = _rbApiBaseUrl + "marketdata/";
		internal const string NewsBase = _rbApiBaseUrl + "midlands/news/";
		internal const string Dividends = _rbApiBaseUrl + "dividends/";
		internal const string FundamentalsBase = _rbApiBaseUrl + "fundamentals/";
		internal const string InstrumentsBase = _rbApiBaseUrl + "instruments/";
		internal const string Challenge = _rbApiBaseUrl + "challenge/{0}/respond/";

		// not Implemented
		internal const string AchBase = _rbApiBaseUrl + "ach/";
		internal const string Applications = _rbApiBaseUrl + "applications/";
		internal const string Documents = _rbApiBaseUrl + "documents/";
		internal const string DocumentRequests = _rbApiBaseUrl + "upload/document_requests/";
		internal const string MarginUpgrades = _rbApiBaseUrl + "margin/upgrades/";
		internal const string Markets = _rbApiBaseUrl + "markets/";
		internal const string Notifications = _rbApiBaseUrl + "notifications/";

		// Options
		internal const string OptionsBase = _rbApiBaseUrl + "options/";
		internal const string OptionsChainBase = _rbApiBaseUrl + "options/chains/";
		internal const string OptionsInstrumentsBase = _rbApiBaseUrl + "options/instruments/";

		// User
		internal const string User = _rbApiBaseUrl + "user/";
		internal const string InvestmentProfile = _rbApiBaseUrl + "user/investment_profile/";

		// Quotes
		internal const string Quotes = _rbApiBaseUrl + "quotes/";
		internal const string Historicals = _rbApiBaseUrl + "quotes/historicals/";

		// Auth
		internal const string Oauth = _rbApiBaseUrl + "oauth2/token/";
		internal const string OauthRevoke = _rbApiBaseUrl + "oauth2/revoke_token/";

		// not Implemented
		internal const string MigrateToken = _rbApiBaseUrl + "oauth2/migrate_token/";
		internal const string PasswordReset = _rbApiBaseUrl + "password_reset/request/";
	}

	internal static readonly IDictionary<string, string> Pairs = new Dictionary<string, string>
	{
		{"BTCUSD", "3d961844-d360-45fc-989b-f6fca761d511"},
		{"ETHUSD", "76637d50-c702-4ed1-bcb5-5b0732a81f48"},
		{"ETCUSD", "7b577ce3-489d-4269-9408-796a0d1abb3a"},
		{"BCHUSD", "2f2b77c4-e426-4271-ae49-18d5cb296d3a"},
		{"BSVUSD", "086a8f9f-6c39-43fa-ac9f-57952f4a1ba6"},
		{"LTCUSD", "383280b1-ff53-43fc-9c84-f01afd0989cd"},
		{"DOGEUSD", "1ef78e1b-049b-4f12-90e5-555dcf2fe204"}
	};

	internal static class Authentication
	{
		/**
		 * This is extracted from Robinhood web app, same for all web client users:
		 * https://stackoverflow.com/a/66020561
		 */
		internal const string ClientId = "c82SH0WZOsabOXGP2sxqcj34FxkvfnWRZBKlBjFS";

		internal const string GrantType = "password";

		internal const string Scope = "internal";
	}
}
