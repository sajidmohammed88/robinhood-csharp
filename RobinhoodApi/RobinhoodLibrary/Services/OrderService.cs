using RobinhoodLibrary.Abstractions;
using RobinhoodLibrary.Data.Orders;
using RobinhoodLibrary.Data.Orders.Request;
using RobinhoodLibrary.Data.Quote;
using RobinhoodLibrary.Data.User;
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
    /// Order service that responsible on orders endpoints call.
    /// </summary>
    /// <seealso cref="IOrderService" />
    public class OrderService : IOrderService
    {
        private readonly IQuoteDataService _quoteDataService;
        private readonly IHttpClientManager _httpClientManager;

        public OrderService(IQuoteDataService quoteDataService, IHttpClientManager httpClientManager)
        {
            _quoteDataService = quoteDataService;
            _httpClientManager = httpClientManager;
        }

        /// <inheritdoc />
        public async Task<IList<Order>> GetOrdersHistory()
        {
            OrderResult orderResult = await _httpClientManager.GetAsync<OrderResult>(Constants.Routes.OrdersBase);

            List<Order> orders = new List<Order>();
            if (orderResult?.Results == null || !orderResult.Results.Any())
            {
                return orders;
            }
            orders.AddRange(orderResult.Results);

            while (orderResult.Next != null)
            {
                orderResult = await _httpClientManager.GetAsync<OrderResult>(orderResult.Next);
                orders.AddRange(orderResult.Results);
            }

            return orders;
        }

        /// <inheritdoc />
        public async Task<Order> GetOrderHistory(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                throw new HttpResponseException("The order identifier is empty.");
            }

            return await _httpClientManager.GetAsync<Order>($"{Constants.Routes.OrdersBase}{orderId}/");
        }

        /// <inheritdoc />
        public async Task<dynamic> SubmitBuyOrder(OrderRequest orderRequest)
        {
            // Used for default price input
            // Price is required, so we use the current ask price if it is not specified
            QuoteData quote = await _quoteDataService.GetQuoteData(orderRequest.Symbol);
            RbHelper.CheckOrderRequest(orderRequest, quote.AskPrice, quote.LastTradePrice);
            Account account = await _quoteDataService.GetAccount();

            try
            {
                return await _httpClientManager.PostAsync<dynamic>(Constants.Routes.OrdersBase,
                        RbHelper.BuildOrderContent(orderRequest, account.Url));
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Exception when placing an order, reason : {ex.Message}");
            }
        }

        /// <inheritdoc />
        public async Task<dynamic> SubmitSellOrder(OrderRequest orderRequest)
        {
            // Used for default price input
            // Price is required, so we use the current ask price if it is not specified
            QuoteData quote = await _quoteDataService.GetQuoteData(orderRequest.Symbol);
            RbHelper.CheckOrderRequest(orderRequest, quote.BidPrice, quote.LastTradePrice);
            Account account = await _quoteDataService.GetAccount();

            try
            {
                return await _httpClientManager.PostAsync<dynamic>(Constants.Routes.OrdersBase,
                        RbHelper.BuildOrderContent(orderRequest, account.Url));
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Exception when placing an order, reason : {ex.Message}");
            }
        }

        /// <inheritdoc />
        public async Task<dynamic> PlaceOrder(OrderRequest orderRequest)
        {
            if (orderRequest == null)
            {
                throw new RequestCheckException("The order request is null in call to PlaceOrder");
            }

            if (orderRequest.Price == null)
            {
                QuoteData quote = await _quoteDataService.GetQuoteData(orderRequest.Symbol);
                orderRequest.Price = quote.BidPrice;
                if (quote.BidPrice == null || quote.BidPrice == "0.0")
                {
                    orderRequest.Price = quote.LastTradePrice;
                }
            }

            Account account = await _quoteDataService.GetAccount();

            try
            {
                return await _httpClientManager.PostAsync<dynamic>(Constants.Routes.OrdersBase,
                        RbHelper.BuildOrderContent(orderRequest, account.Url));
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Exception when placing an order, reason : {ex.Message}");
            }
        }
    }
}
