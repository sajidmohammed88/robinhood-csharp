using RobinhoodLibrary.Abstractions;
using RobinhoodLibrary.Data.Base;
using RobinhoodLibrary.Data.Options;
using RobinhoodLibrary.Data.Quote;
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
        private readonly IPaginator _paginator;

        public OptionsInformationService(IHttpClientManager httpClientManager, IPaginator paginator)
        {
            _httpClientManager = httpClientManager;
            _paginator = paginator;
        }

        /// <inheritdoc />
        public async Task<Chain> GetChain(string instrumentId)
        {
            BaseResult<Chain> chainResult = await _httpClientManager
                .GetAsync<BaseResult<Chain>>(Constants.Routes.OptionsChainBase,
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

            BaseResult<Option> instrumentOptions = await _httpClientManager
                .GetAsync<BaseResult<Option>>(Constants.Routes.OptionsInstrumentsBase, query: query);

            return await _paginator.PaginateResultAsync(instrumentOptions);
        }

        /// <inheritdoc />
        public async Task<IList<Option>> GetOwnedOptions()
        {
            BaseResult<Option> optionResult = await _httpClientManager
                .GetAsync<BaseResult<Option>>($"{Constants.Routes.OptionsBase}positions/?nonzero=true");

            return await _paginator.PaginateResultAsync(optionResult);
        }

        /// <inheritdoc />
        public async Task<Guid> GetOptionChainId(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                throw new HttpResponseException("The symbol request is null or empty");
            }

            BaseResult<Instrument> stockInfo = await _httpClientManager.GetAsync<BaseResult<Instrument>>(Constants.Routes.InstrumentsBase,
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
            BaseResult<Option> optionResult = await _httpClientManager.GetAsync<BaseResult<Option>>(Constants.Routes.OptionsInstrumentsBase, query: query);

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
