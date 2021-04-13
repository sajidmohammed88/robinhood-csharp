using Microsoft.Extensions.DependencyInjection;
using RobinhoodLibrary.Abstractions;
using RobinhoodLibrary.Data.Authentication;
using RobinhoodLibrary.Data.Fundamentals;
using RobinhoodLibrary.Data.Orders;
using RobinhoodLibrary.Data.Portfolios;
using RobinhoodLibrary.Data.Positions;
using RobinhoodLibrary.Data.User;
using RobinhoodLibrary.Enum;
using RobinhoodLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RobinhoodConsoleApp
{
    public partial class Program
    {
        private static IRobinhood _robinhood;

        public static async Task Main(string[] args)
        {
            CreateHostBuilder(args).Build();

            _robinhood = _serviceProvider.GetRequiredService<IRobinhood>();

            AuthenticationResponse authResponse = await _robinhood.Login();

            if (authResponse.IsChallenge)
            {
                do
                {
                    Challenge challenge = authResponse.Challenge;

                    Console.WriteLine($"Input challenge code from {challenge.ChallengeType} ({challenge.RemainingAttempts}/{challenge.RemainingRetries}):");
                    string code = Console.ReadLine();

                    authResponse = await _robinhood.ChallengeOauth2(challenge.Id, code);
                } while (authResponse.IsChallenge && authResponse.Challenge.CanRetry);
            }

            if (authResponse.MfaRequired)
            {
                int attempts = 3;
                (HttpStatusCode statusCode, AuthenticationResponse mfaAuth) mfaResponse;
                do
                {
                    Console.WriteLine($"Input the MFA code:");
                    string code = Console.ReadLine();

                    mfaResponse = await _robinhood.MfaOath2(code);
                    attempts--;
                } while (attempts > 0 && mfaResponse.statusCode != HttpStatusCode.OK);

                authResponse = mfaResponse.mfaAuth;
            }

            if (!authResponse.IsOauthValid)
            {
                string message;
                if (!string.IsNullOrEmpty(authResponse.Error))
                {
                    message = authResponse.Error;
                }
                else if (!string.IsNullOrEmpty(authResponse.Detail))
                {
                    message = authResponse.Detail;
                }
                else
                {
                    message = "Unknown login error";
                }

                Console.WriteLine(message);
                throw new AuthenticationException(message);
            }

            _robinhood.ConfigureManager(authResponse);

            User user = await _robinhood.GetUser();
            InvestmentProfile investmentProfile = await _robinhood.GetInvestmentProfile();

            //quote data
            await FetchQuoteData();

            //options
            await FetchOptions();

            //fundamentals
            Fundamental fundamental = await _robinhood.GetFundamentals("AAPL");

            //Portfolio
            IList<Portfolio> portfolios = await _robinhood.GetPortfolio();
            dynamic dividends = await _robinhood.GetDividends();

            //Positions
            IList<Position> positions = await _robinhood.GetPositions();
            IList<Position> securities = await _robinhood.GetOwnedSecurities();

            //orders
            IList<Order> ordersHistory = await _robinhood.GetOrdersHistory();
            Order orderHistory = await _robinhood.GetOrderHistory(new Guid("60743aef-8b9e-4c17-b8b4-b32e1facb242"));
            IList<Order> openedOrders = await _robinhood.GetOpenOrders();
            await _robinhood.CancelOrder(new Guid("60743aef-8b9e-4c17-b8b4-b32e1facb242"));

            dynamic x = await _robinhood
                .PlaceMarketBuyOrder("https://api.robinhood.com/instruments/450dfc6d-5510-4d40-abfb-f633b7d9be3e/",
                    "AAPL", TimeInForce.Gfd, 1);

            //await _robinhood.Logout();
        }
    }
}
