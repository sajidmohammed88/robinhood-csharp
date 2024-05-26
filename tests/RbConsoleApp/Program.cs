using Microsoft.Extensions.DependencyInjection;

using Rb.Integration.Api.Abstractions;
using Rb.Integration.Api.Data.Authentication;
using Rb.Integration.Api.Data.Dividends;
using Rb.Integration.Api.Data.Fundamentals;
using Rb.Integration.Api.Data.Orders;
using Rb.Integration.Api.Data.Portfolios;
using Rb.Integration.Api.Data.Positions;
using Rb.Integration.Api.Data.User;
using Rb.Integration.Api.Enum;
using Rb.Integration.Api.Exceptions;

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RobinhoodConsoleApp;

public static partial class Program
{
	private static IRobinhood _robinhood;

	public static async Task Main(string[] args)
	{
		CreateHostBuilder(args).Build();

		_robinhood = _serviceProvider.GetRequiredService<IRobinhood>();

		AuthenticationResponse authResponse = await _robinhood.LoginAsync();

		if (authResponse.IsChallenge)
		{
			do
			{
				Challenge challenge = authResponse.Challenge;

				Console.WriteLine($"Input challenge code from {challenge.Type} ({challenge.RemainingAttempts}/{challenge.RemainingRetries}):");
				string code = Console.ReadLine();

				authResponse = await _robinhood.ChallengeOauth2Async(challenge.Id, code);
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

				mfaResponse = await _robinhood.MfaOath2Async(code);
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
			else
			{
				message = !string.IsNullOrEmpty(authResponse.Detail) ? authResponse.Detail : "Unknown login error";
			}

			Console.WriteLine(message);
			throw new AuthenticationException(message);
		}

		_robinhood.ConfigureManager(authResponse);

		User user = await _robinhood.GetUserAsync();
		InvestmentProfile investmentProfile = await _robinhood.GetInvestmentProfileAsync();

		//crypto currency
		await FetchCryptoDataAsync();

		//quote data
		await FetchQuoteDataAsync();

		//options
		await FetchOptionsAsync();

		//fundamentals
		Fundamental fundamental = await _robinhood.GetFundamentalsAsync("AAPL");

		//Portfolio
		IList<Portfolio> portfolios = await _robinhood.GetPortfolioAsync();
		IList<Dividends> dividends = await _robinhood.GetDividendsAsync();

		//Positions
		IList<Position> positions = await _robinhood.GetPositionsAsync();
		IList<Position> securities = await _robinhood.GetOwnedSecuritiesAsync();

		//orders
		IList<Order> ordersHistory = await _robinhood.GetOrdersHistoryAsync();
		Order orderHistory = await _robinhood.GetOrderHistoryAsync(new Guid("6081bf8f-cc7c-4960-bed9-04440614aa83"));
		IList<Order> openedOrders = await _robinhood.GetOpenOrders();
		bool isOrderCanceled = await _robinhood.CancelOrderAsync(new Guid("6081c6f3-0d58-4532-a9a8-773ced6bec70"));

		Order placeBuyOrder = await _robinhood.PlaceBuyOrderAsync("https://api.robinhood.com/instruments/6df56bd0-0bf2-44ab-8875-f94fd8526942/", "F", "1.0");
		Order placeSellOrder = await _robinhood.PlaceSellOrderAsync("https://api.robinhood.com/instruments/6df56bd0-0bf2-44ab-8875-f94fd8526942/", "F", "1.0");
		Order marketBuyOrder = await _robinhood
			.PlaceMarketBuyOrderAsync("https://api.robinhood.com/instruments/6df56bd0-0bf2-44ab-8875-f94fd8526942/", "F", TimeInForce.Gfd, 1);
		Order marketSellOrder = await _robinhood
			.PlaceMarketSellOrderAsync("https://api.robinhood.com/instruments/6df56bd0-0bf2-44ab-8875-f94fd8526942/", "F", TimeInForce.Gfd, 1);
		Order limitBuyOrder = await _robinhood
			.PlaceLimitBuyOrderAsync("https://api.robinhood.com/instruments/6df56bd0-0bf2-44ab-8875-f94fd8526942/", "F", TimeInForce.Gfd, "1.1", 1);
		Order limitSellOrder = await _robinhood
			.PlaceLimitSellOrderAsync("https://api.robinhood.com/instruments/6df56bd0-0bf2-44ab-8875-f94fd8526942/", "F", TimeInForce.Gfd, "20", 1);
		Order stopMarketBuyOrder = await _robinhood
			.PlaceStopLossBuyOrderAsync("https://api.robinhood.com/instruments/6df56bd0-0bf2-44ab-8875-f94fd8526942/", "F", TimeInForce.Gfd, "2.1", 1);
		Order stopMarketSellOrder = await _robinhood
			.PlaceStopLossSellOrderAsync("https://api.robinhood.com/instruments/6df56bd0-0bf2-44ab-8875-f94fd8526942/", "F", TimeInForce.Gfd, "21", 1);
		Order stopLimitBuyOrder = await _robinhood
			.PlaceStopLimitBuyOrderAsync("https://api.robinhood.com/instruments/6df56bd0-0bf2-44ab-8875-f94fd8526942/", "F", TimeInForce.Gfd, "1.5", "21", 1);
		Order stopLimitSellOrder = await _robinhood
			.PlaceStopLimitSellOrderAsync("https://api.robinhood.com/instruments/6df56bd0-0bf2-44ab-8875-f94fd8526942/", "F", TimeInForce.Gfd, "15", "21", 1);

		//await _robinhood.Logout();
	}
}
