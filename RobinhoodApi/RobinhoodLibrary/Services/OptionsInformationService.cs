using RobinhoodLibrary.Abstractions;
using RobinhoodLibrary.Data.Options;
using RobinhoodLibrary.Enum;
using RobinhoodLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobinhoodLibrary.Services
{
    /// <summary>
    /// Options information service that responsible on options endpoints call.
    /// </summary>
    /// <seealso cref="IOptionsInformationService" />
    public class OptionsInformationService : IOptionsInformationService
    {
        private readonly IHttpClientManager _httpClientManager;

        public OptionsInformationService(IHttpClientManager httpClientManager)
        {
            _httpClientManager = httpClientManager;
        }

        /// <inheritdoc />
        public async Task<Chain> GetChain(string instrumentId)
        {
            ChainResult chainResult = await _httpClientManager
                .GetAsync<ChainResult>(Constants.Routes.OptionsChainBase,
                    query: new Dictionary<string, string> { { "equity_instrument_ids", instrumentId } });

            return chainResult.Results.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<IList<Option>> GetOptionsByChainId(Guid chainId, IList<string> expirationDates,
            OptionType optionType)
        {
            IDictionary<string, string> query = new Dictionary<string, string>
            {
                {"chain_id", chainId.ToString()},
                {"expiration_dates", string.Join(",", expirationDates)},
                {"state", "active"},
                {"tradability", "tradable"},
                {"type", string.Join(",", optionType.ToString().ToLower())},
            };

            OptionResult instrumentOptions = await _httpClientManager
                .GetAsync<OptionResult>(Constants.Routes.OptionsInstrumentsBase, query: query);

            return await FillPaginatedOptions(instrumentOptions);
        }

        /// <inheritdoc />
        public async Task<IList<Option>> GetOwnedOptions()
        {
            OptionResult optionResult = await _httpClientManager
                .GetAsync<OptionResult>($"{Constants.Routes.OptionsBase}positions/?nonzero=true");

            return await FillPaginatedOptions(optionResult);
        }

        private async Task<IList<Option>> FillPaginatedOptions(OptionResult optionResult)
        {
            if (optionResult?.Results == null || !optionResult.Results.Any() || optionResult.Next == null)
            {
                return optionResult?.Results;
            }

            List<Option> options = new List<Option>();
            options.AddRange(optionResult.Results);

            while (optionResult.Next != null)
            {
                optionResult = await _httpClientManager.GetAsync<OptionResult>(optionResult.Next);
                options.AddRange(optionResult.Results);
            }

            return options;
        }

        /// <inheritdoc />
        public async Task<Guid> GetOptionChainId(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new HttpResponseException("The symbol request is null or empty");
            }

            InstrumentResult stockInfo = await _httpClientManager.GetAsync<InstrumentResult>(Constants.Routes.InstrumentsBase,
                query: new Dictionary<string, string> { { "symbol", symbol } });

            if (stockInfo?.Results == null || !stockInfo.Results.Any())
            {
                throw new HttpResponseException($"The symbol:{symbol} not exist");
            }

            Chain chain = await GetChain(stockInfo.Results[0].Id.ToString());

            return chain?.Id ?? Guid.Empty;
        }

        /// <inheritdoc />
        /// /!\ in the python code we call GetOptionMarketData to bid and ask price, but it's respond all time 403 status code.
        public async Task<Guid> GetOptionQuote(string symbol, string strike, string expirationDate,
            OptionType optionType, string state = "active")
        {
            IDictionary<string, string> query = new Dictionary<string, string>
            {
                {"chain_symbol", symbol},
                {"strike_price", strike},
                {"expiration_dates", expirationDate},
                {"type", optionType.ToString().ToLower()},
                {"state", state}
            };

            //symbol, strike, expirationDate, optionType should uniquely define an option
            OptionResult optionResult = await _httpClientManager.GetAsync<OptionResult>(Constants.Routes.OptionsInstrumentsBase, query: query);

            if (optionResult?.Results == null || !optionResult.Results.Any())
            {
                throw new HttpResponseException("The options quote not exist");
            }

            return optionResult.Results[0].Id;
        }

        /// <inheritdoc />
        public async Task<dynamic> GetOptionMarketData(string optionId)
        {
            return await _httpClientManager.GetAsync<dynamic>($"{Constants.Routes.MarketDataBase}options/{optionId}/");
            // /!\ gives 403:Forbidden, the same result for python lib.
        }
    }
}
