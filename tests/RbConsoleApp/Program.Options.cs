using Rb.Integration.Api.Data.Options;
using Rb.Integration.Api.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobinhoodConsoleApp;

public static partial class Program
{
	public static async Task FetchOptions()
	{
		IList<Option> options = await _robinhood.GetOptions("AAPL",
			[
				"2021-04-16",
				"2021-04-23"
			], OptionType.Call);

		IList<Option> ownedOptions = await _robinhood.GetOwnedOptions();

		Guid chainId = await _robinhood.GetOptionChainId("AAPL");

		Guid optionQuote = await _robinhood.GetOptionQuote("AAPL", "105.0000", "2021-04-16", OptionType.Call);

		dynamic marketDatas = await _robinhood.GetOptionMarketData(options.First().Id);
	}
}
