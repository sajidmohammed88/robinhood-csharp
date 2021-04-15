using RobinhoodLibrary.Data.Options;
using RobinhoodLibrary.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobinhoodConsoleApp
{
    public partial class Program
    {
        public static async Task FetchOptions()
        {
            IList<Option> options = await _robinhood.GetOptions("AAPL",
                new List<string>
                {
                    "2021-04-16",
                    "2021-04-23"
                }, OptionType.Call);

            IList<Option> ownedOptions = await _robinhood.GetOwnedOptions();

            Guid chainId = await _robinhood.GetOptionChainId("AAPL");

            Guid optionQuote = await _robinhood.GetOptionQuote("AAPL", "105.0000", "2021-04-16", OptionType.Call);

            dynamic marketDatas = await _robinhood.GetOptionMarketData(options.First().Id);
        }
    }
}
