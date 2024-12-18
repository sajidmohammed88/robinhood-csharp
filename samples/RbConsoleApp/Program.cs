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

		if (authResponse.MfaRequired == true)
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

		try
		{
			Order orderHistory = await _robinhood.GetOrderHistoryAsync(new Guid("67621c7c-deab-4708-9474-ea47c0a04783"));
		}
		catch (HttpResponseException ex)
		{
			Console.WriteLine(ex.Message);
		}

		IList<Order> openedOrders = await _robinhood.GetOpenOrders();

		try
		{
			bool isOrderCanceled = await _robinhood.CancelOrderAsync(new Guid("67621c7c-deab-4708-9474-ea47c0a04783"));
		}
		catch (HttpResponseException ex)
		{
			Console.WriteLine(ex.Message);
		}

		Order buyMarket = await _robinhood.PlaceOrderBuyMarketAsync(symbol: "F", quantity: 1);
		Order sellMarket = await _robinhood.PlaceOrderSellMarketAsync(symbol: "F", quantity: 1);

		Order buyLimit = await _robinhood.PlaceOrderBuyLimitAsync(symbol: "F", quantity: 1, limitPrice: 9.9);
		Order sellLimit = await _robinhood.PlaceOrderSellLimitAsync(symbol: "F", quantity: 1, limitPrice: 9.9);

		Order buyStopLoss =  await _robinhood.PlaceOrderBuyStopLossAsync(symbol: "F", quantity: 1, stopPrice: 9.9);
		Order sellStopLoss = await _robinhood.PlaceOrderSellStopLossAsync(symbol: "F", quantity: 1, stopPrice: 9.9);

		Order buyStopLimit = await _robinhood.PlaceOrderBuyStopLimitAsync(symbol: "F", quantity: 1, stopPrice: 9.9, limitPrice: 10.0);
		Order sellStopLimit = await _robinhood.PlaceOrderSellStopLimitAsync(symbol: "F", quantity: 1, stopPrice: 9.9, limitPrice: 10.0);

		//await _robinhood.Logout();
	}
}
