using RobinhoodLibrary.Abstractions;
using RobinhoodLibrary.Data.News;
using RobinhoodLibrary.Data.Quote;
using RobinhoodLibrary.Data.User;
using RobinhoodLibrary.Enum;
using RobinhoodLibrary.Exceptions;
using RobinhoodLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RobinhoodLibrary.Services
{
    /// <summary>
    /// Quote data service that responsible on quote data endpoints call.
    /// </summary>
    public class QuoteDataService : IQuoteDataService
    {
        private readonly IHttpClientManager _httpClientManager;

        public QuoteDataService(IHttpClientManager httpClientManager)
        {
            _httpClientManager = httpClientManager;
        }

        /// <inheritdoc />
        public async Task<QuoteData> GetQuoteData(string stock)
        {
            if (string.IsNullOrEmpty(stock))
            {
                throw new HttpResponseException("Invalid request, reason : given stock is null or empty");
            }

            try
            {
                return await _httpClientManager.GetAsync<QuoteData>($"{Constants.Routes.Quotes}{stock}/");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Invalid ticker (stock symbol), reason : {ex.Message}");
            }
        }

        /// <inheritdoc />
        public async Task<IList<string>> GetQuoteWithSpecifiedKeys(string stock, string keys)
        {
            if (string.IsNullOrEmpty(stock) || string.IsNullOrEmpty(keys))
            {
                throw new HttpResponseException("Invalid request, reason : given stock or key are null or empty");
            }

            QuoteData quoteData = await GetQuoteData(stock);

            return RbHelper.GetValueByKeys(keys, quoteData);
        }

        /// <inheritdoc />
        public async Task<IList<QuoteData>> GetQuotesData(IList<string> stocks)
        {
            if (stocks == null || !stocks.Any())
            {
                throw new HttpResponseException("Invalid request, reason : given stocks is null or empty");
            }

            try
            {
                QuotesResult quotesData = await _httpClientManager
                    .GetAsync<QuotesResult>($"{Constants.Routes.Quotes}?symbols={string.Join(",", stocks)}");
                return quotesData.Results;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Invalid ticker (stock symbol), reason : {ex.Message}");
            }
        }

        /// <inheritdoc />
        public async Task<IDictionary<string, IList<string>>> GetQuotesWithSpecifiedKeys(IList<string> stocks, string keys)
        {
            if (stocks == null || !stocks.Any() || string.IsNullOrEmpty(keys))
            {
                throw new HttpResponseException("Invalid request, reason : given stocks or keys are null or empty");
            }

            IList<QuoteData> quotesData = await GetQuotesData(stocks);

            IDictionary<string, IList<string>> result = new Dictionary<string, IList<string>>();

            foreach (QuoteData quoteData in quotesData)
            {
                result.Add(quoteData.Symbol, RbHelper.GetValueByKeys(keys, quoteData));
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<IList<HistoricalsData>> GetHistoricalQuotes(IList<string> stocks, string interval, Span span,
            Bounds bounds = Bounds.Regular)
        {
            /*Note: valid interval/ span configs
                  interval = 5minute | 10minute + span = day, week
                  interval = day + span = year
                  interval = week*/

            Dictionary<string, string> query = new Dictionary<string, string>
            {
                {"symbols", string.Join(",",stocks).ToUpper()},
                {"interval", interval.ToLower()},
                {"span", span.ToString().ToLower()},
                {"bounds", bounds.ToString().ToLower()}
            };

            HistoricalsResult historicalResult = await _httpClientManager.GetAsync<HistoricalsResult>(Constants.Routes.Historicals, query: query);

            return historicalResult.Results;
        }

        /// <inheritdoc />
        public async Task<IList<NewsData>> GetNews(string stock)
        {
            NewsResult newsResult = await _httpClientManager.GetAsync<NewsResult>($"{Constants.Routes.NewsBase}{stock}/");

            if (newsResult?.Results == null || !newsResult.Results.Any() || newsResult.Next == null)
            {
                return newsResult?.Results;
            }

            List<NewsData> newsData = new List<NewsData>();
            newsData.AddRange(newsResult.Results);
            while (newsResult.Next != null)
            {
                newsResult = await _httpClientManager.GetAsync<NewsResult>(newsResult.Next);
                newsData.AddRange(newsResult.Results);
            }

            return newsData;
        }

        /// <inheritdoc />
        public async Task<Instrument> GetInstrument(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new HttpResponseException("The instrument url is null or empty");
            }

            return await _httpClientManager.GetAsync<Instrument>(url);
        }

        /// <inheritdoc />
        public async Task<IList<string>> GetTickersByTag(string tag)
        {
            /*tag - Tags may include but are not limited to:
                *top-movers
                * etf
                * 100-most-popular
                * mutual-fund
                * finance
                * cap-weighted
                * investment-trust-or-fund */

            InstrumentsTag instrumentsTag = await _httpClientManager.GetAsync<InstrumentsTag>($"{Constants.Routes.TagsBase}{tag}/");
            if (instrumentsTag?.Instruments == null || !instrumentsTag.Instruments.Any())
            {
                throw new HttpResponseException("The instruments gotten by tag is null or empty");
            }

            IList<string> tickers = new List<string>();
            foreach (string instrumentUrl in instrumentsTag.Instruments)
            {
                Instrument instrument = await GetInstrument(instrumentUrl);

                if (!string.IsNullOrEmpty(instrument?.Symbol))
                {
                    tickers.Add(instrument.Symbol);
                }
            }

            return tickers;
        }

        /// <inheritdoc />
        public async Task<Account> GetAccount()
        {
            AccountResult accountResult = await _httpClientManager.GetAsync<AccountResult>(Constants.Routes.Accounts);

            if (accountResult?.Results == null || !accountResult.Results.Any() || accountResult.Results.All(a => a.Deactivated))
            {
                throw new HttpResponseException("The account for the user need to be approved or disabled");
            }

            return accountResult.Results.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<IList<Instrument>> GetWatchLists()
        {
            WatchlistResult watchListResult = await _httpClientManager.GetAsync<WatchlistResult>(Constants.Routes.WatchLists);
            if (watchListResult?.Results == null || !watchListResult.Results.Any())
            {
                throw new HttpResponseException("No watch list exist");
            }

            WatchlistDataResult watchlistDataResult = await _httpClientManager.GetAsync<WatchlistDataResult>(watchListResult.Results[0].Url);
            List<WatchlistData> watchlistData = new List<WatchlistData>();

            if (watchlistDataResult?.Results == null || !watchlistDataResult.Results.Any())
            {
                throw new HttpResponseException($"No data exist for fetched url : {watchListResult.Results[0].Url}");
            }

            watchlistData.AddRange(watchlistDataResult.Results);

            while (watchlistDataResult.Next != null)
            {
                watchlistDataResult = await _httpClientManager.GetAsync<WatchlistDataResult>(watchlistDataResult.Next);
                watchlistData.AddRange(watchlistDataResult.Results);
            }

            IList<Instrument> result = new List<Instrument>();
            foreach (WatchlistData data in watchlistData)
            {
                result.Add(await _httpClientManager.GetAsync<Instrument>(data.Instrument));
            }

            return result;
        }

        /// <inheritdoc />
        /// /!\ gives BadRequest, the same result for the python library.
        public async Task<dynamic> GetStockMarketData(IList<string> instruments)
        {
            if (instruments == null || !instruments.Any())
            {
                throw new HttpResponseException("Invalid request, reason : given instruments is null or empty");
            }

            return await _httpClientManager
                .GetAsync<dynamic>($"{RbHelper.BuildUrlMarketData()}quotes/?instruments={string.Join(",", instruments)}");
        }

        /// <inheritdoc />
        /// /!\ gives 404, the route not exist in python, but exist in ruby code.
        public async Task<dynamic> GetPopularity(string stock)
        {
            QuoteData quoteData = await GetQuoteData(stock);
            if (string.IsNullOrEmpty(quoteData?.Instrument))
            {
                throw new HttpResponseException($"The stock {stock} don't have instrument");
            }

            string instrumentId = quoteData.Instrument.Split("/")[4];

            return await _httpClientManager.GetAsync<dynamic>($"{Constants.Routes.InstrumentsBase}{instrumentId}/popularity/");
        }
    }
}
