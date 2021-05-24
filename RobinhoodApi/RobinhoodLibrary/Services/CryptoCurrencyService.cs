using RobinhoodLibrary.Abstractions;
using RobinhoodLibrary.Data.Base;
using RobinhoodLibrary.Data.Crypto;
using RobinhoodLibrary.Data.Crypto.Request;
using RobinhoodLibrary.Exceptions;
using RobinhoodLibrary.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RobinhoodLibrary.Services
{
    /// <summary>
    /// The robinhood crypto currency service.
    /// </summary>
    /// <seealso cref="ICryptoCurrencyService" />
    public class CryptoCurrencyService : ICryptoCurrencyService
    {
        private readonly IHttpClientManager _httpClientManager;
        private readonly IPaginator _paginator;

        public CryptoCurrencyService(IHttpClientManager httpClientManager, IPaginator paginator)
        {
            _httpClientManager = httpClientManager;
            _paginator = paginator;
        }

        /// <inheritdoc />
        public async Task<IList<CurrencyPair>> GetCurrencyPairs()
        {
            BaseResult<CurrencyPair> currencyPairResult = await _httpClientManager.GetAsync<BaseResult<CurrencyPair>>(Constants.Routes.CurrencyPairs);

            return await _paginator.PaginateResultAsync(currencyPairResult);
        }

        /// <inheritdoc />
        public async Task<Quotes> GetQuotes(string pair)
        {
            if (string.IsNullOrEmpty(pair) || !RbHelper.Pairs.ContainsKey(pair))
            {
                throw new CryptoCurrencyException("The pair is null, empty or not exist.");
            }

            return await _httpClientManager.GetAsync<Quotes>(string.Format(Constants.Routes.NummusQuotes, RbHelper.Pairs[pair]));
        }

        /// <inheritdoc />
        public async Task<IList<CryptoAccount>> GetAccounts()
        {
            BaseResult<CryptoAccount> cryptoAccountResult = await _httpClientManager.GetAsync<BaseResult<CryptoAccount>>(Constants.Routes.NummusAccounts);

            if (cryptoAccountResult?.Results == null || !cryptoAccountResult.Results.Any())
            {
                throw new HttpResponseException("The crypto currency account for the user need to be approved.");
            }

            return await _paginator.PaginateResultAsync(cryptoAccountResult);
        }

        /// <inheritdoc />
        public async Task<CryptoOrder> Trade(string pair, CryptoOrderRequest orderRequest)
        {
            if (string.IsNullOrEmpty(pair) || !RbHelper.Pairs.ContainsKey(pair))
            {
                throw new CryptoCurrencyException("The pair is null, empty or not exist.");
            }

            string accountId = (await GetAccounts()).First().Id;
            var trade = await _httpClientManager.PostAsync<CryptoOrder>(Constants.Routes.NummusOrders,
                RbHelper.BuildTradeContent(orderRequest, accountId, pair));

            return trade.Data;
        }

        /// <inheritdoc />
        public async Task<IList<CryptoOrder>> GetTradeHistory()
        {
            BaseResult<CryptoOrder> cryptoOrderResult = await _httpClientManager.GetAsync<BaseResult<CryptoOrder>>(Constants.Routes.NummusOrders);
            return await _paginator.PaginateResultAsync(cryptoOrderResult);
        }

        /// <inheritdoc />
        public async Task<CryptoOrder> GetOrderStatus(string orderId)
        {
            return await _httpClientManager.GetAsync<CryptoOrder>(string.Format(Constants.Routes.OrderStatus, orderId));
        }

        /// <inheritdoc />
        public async Task<bool> CancelCryptoOrder(string orderId)
        {
            CryptoOrder order = await GetOrderStatus(orderId);
            if (order == null)
            {
                throw new HttpResponseException($"The order {orderId} not exist.");
            }

            if (order.CancelUrl == null)
            {
                throw new HttpResponseException($"The order {orderId} can't be canceled.");
            }

            var (statusCode, _) = await _httpClientManager.PostAsync(order.CancelUrl, null, (null, null));

            return statusCode == HttpStatusCode.OK;
        }

        /// <inheritdoc />
        public async Task<CryptoHistoricalData> Historicals(string pair, string interval, string span, string bounds)
        {
            if (string.IsNullOrEmpty(pair) || !RbHelper.Pairs.ContainsKey(pair))
            {
                throw new CryptoCurrencyException("The pair is null, empty or not exist.");
            }

            return await _httpClientManager.GetAsync<CryptoHistoricalData>(
                string.Format(Constants.Routes.NummusHistoricals, RbHelper.Pairs[pair], interval, span, bounds));
        }

        /// <inheritdoc />
        public async Task<IList<Holding>> Holdings()
        {
            BaseResult<Holding> result = await _httpClientManager.GetAsync<BaseResult<Holding>>(Constants.Routes.Holdings);

            return await _paginator.PaginateResultAsync(result);
        }
    }
}
