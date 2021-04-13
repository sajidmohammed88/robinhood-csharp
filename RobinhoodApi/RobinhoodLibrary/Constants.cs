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
            internal const string BaseUrl = "https://api.robinhood.com";

            //General
            internal const string Accounts = "accounts/";
            internal const string OrdersBase = "orders/";
            internal const string Portfolios = "portfolios/";
            internal const string Positions = "positions/";
            internal const string TagsBase = "midlands/tags/tag/";
            internal const string WatchLists = "watchlists/";
            internal const string MarketDataBase = "marketdata/";
            internal const string NewsBase = "midlands/news/";
            internal const string Dividends = "dividends/";
            internal const string FundamentalsBase = "fundamentals/";
            internal const string InstrumentsBase = "instruments/";
            internal const string Challenge = "challenge/{0}/respond/";

            // not Implemented
            internal const string AchBase = "ach/";
            internal const string Applications = "applications/";
            internal const string Documents = "documents/";
            internal const string DocumentRequests = "upload/document_requests/";
            internal const string MarginUpgrades = "margin/upgrades/";
            internal const string Markets = "markets/";
            internal const string Notifications = "notifications/";

            // Options
            internal const string OptionsBase = "options/";
            internal const string OptionsChainBase = "options/chains/";
            internal const string OptionsInstrumentsBase = "options/instruments/";

            // User
            internal const string User = "user/";
            internal const string InvestmentProfile = "user/investment_profile/";

            // Quotes
            internal const string Quotes = "quotes/";
            internal const string Historicals = "quotes/historicals/";

            // Auth
            internal const string Oauth = "oauth2/token/";
            internal const string OauthRevoke = "oauth2/revoke_token/";

            // not Implemented
            internal const string MigrateToken = "oauth2/migrate_token/";
            internal const string PasswordReset = "password_reset/request/";
        }
    }
}
