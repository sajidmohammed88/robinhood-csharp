namespace RobinhoodLibrary
{
    /// <summary>
    /// The constants used in the Library.
    /// </summary>
    internal static class Constants
    {
        internal static class Routes
        {
            //Base
            private const string RbApiBaseUrl = "https://api.robinhood.com/";
            private const string RbCryptoCurrencyBaseUrl = "https://nummus.robinhood.com/";
            private const string OrdersBase = "orders/";
            private const string AccountsBase = "accounts/";

            //Crypto
            internal const string CurrencyPairs = RbCryptoCurrencyBaseUrl + "currency_pairs";
            internal const string NummusOrders = RbCryptoCurrencyBaseUrl + OrdersBase;
            internal const string OrderStatus = NummusOrders + "{0}/";
            internal const string NummusAccounts = RbCryptoCurrencyBaseUrl + AccountsBase;
            internal const string NummusHistoricals = MarketDataBase + "forex/historicals/{0}/?interval={1}&span={2}&bounds={3}";
            internal const string Holdings = RbCryptoCurrencyBaseUrl + "holdings/";
            internal const string NummusQuotes = MarketDataBase + "forex/quotes/{0}/";

            //General
            internal const string Accounts = RbApiBaseUrl + AccountsBase;
            internal const string Orders = RbApiBaseUrl + OrdersBase;
            internal const string Portfolios = RbApiBaseUrl + "portfolios/";
            internal const string Positions = RbApiBaseUrl + "positions/";
            internal const string TagsBase = RbApiBaseUrl + "midlands/tags/tag/";
            internal const string WatchLists = RbApiBaseUrl + "watchlists/";
            internal const string MarketDataBase = RbApiBaseUrl + "marketdata/";
            internal const string NewsBase = RbApiBaseUrl + "midlands/news/";
            internal const string Dividends = RbApiBaseUrl + "dividends/";
            internal const string FundamentalsBase = RbApiBaseUrl + "fundamentals/";
            internal const string InstrumentsBase = RbApiBaseUrl + "instruments/";
            internal const string Challenge = RbApiBaseUrl + "challenge/{0}/respond/";

            // not Implemented
            internal const string AchBase = RbApiBaseUrl + "ach/";
            internal const string Applications = RbApiBaseUrl + "applications/";
            internal const string Documents = RbApiBaseUrl + "documents/";
            internal const string DocumentRequests = RbApiBaseUrl + "upload/document_requests/";
            internal const string MarginUpgrades = RbApiBaseUrl + "margin/upgrades/";
            internal const string Markets = RbApiBaseUrl + "markets/";
            internal const string Notifications = RbApiBaseUrl + "notifications/";

            // Options
            internal const string OptionsBase = RbApiBaseUrl + "options/";
            internal const string OptionsChainBase = RbApiBaseUrl + "options/chains/";
            internal const string OptionsInstrumentsBase = RbApiBaseUrl + "options/instruments/";

            // User
            internal const string User = RbApiBaseUrl + "user/";
            internal const string InvestmentProfile = RbApiBaseUrl + "user/investment_profile/";

            // Quotes
            internal const string Quotes = RbApiBaseUrl + "quotes/";
            internal const string Historicals = RbApiBaseUrl + "quotes/historicals/";

            // Auth
            internal const string Oauth = RbApiBaseUrl + "oauth2/token/";
            internal const string OauthRevoke = RbApiBaseUrl + "oauth2/revoke_token/";

            // not Implemented
            internal const string MigrateToken = RbApiBaseUrl + "oauth2/migrate_token/";
            internal const string PasswordReset = RbApiBaseUrl + "password_reset/request/";
        }
    }
}
