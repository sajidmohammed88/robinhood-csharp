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

            if (orderResult?.Results == null || !orderResult.Results.Any() || orderResult.Next == null)
            {
                return orderResult?.Results;
            }
            List<Order> orders = new List<Order>();
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
        public async Task<Order> SubmitBuyOrder(OrderRequest orderRequest)
        {
            return await SubmitOrder(orderRequest, true);
        }

        /// <inheritdoc />
        public async Task<Order> SubmitSellOrder(OrderRequest orderRequest)
        {
            return await SubmitOrder(orderRequest, false);
        }

        /// <summary>
        /// Submits the order for sell or buy..
        /// </summary>
        /// <param name="orderRequest">The order request.</param>
        /// <param name="isBuy">if set to <c>true</c> is buy,otherwise false.</param>
        /// <returns>The order.</returns>
        /// <exception cref="HttpRequestException">Exception when placing an order, reason : {ex.Message}</exception>
        private async Task<Order> SubmitOrder(OrderRequest orderRequest, bool isBuy)
        {
            RbHelper.CheckOrderRequest(orderRequest);
            Account account = await _quoteDataService.GetAccount();
            if (orderRequest.Price == null)
            {
                QuoteData quote = await _quoteDataService.GetQuoteData(orderRequest.Symbol);
                orderRequest.Price = (isBuy ? quote.AskPrice : quote.BidPrice) ?? quote.LastTradePrice;
            }

            try
            {
                var submitResult = await _httpClientManager.PostAsync<Order>(Constants.Routes.OrdersBase,
                    RbHelper.BuildOrderContent(orderRequest, account.Url));
                return submitResult.Data;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Exception when placing an order, reason : {ex.Message}");
            }
        }

        /// <inheritdoc />
        public async Task<Order> PlaceOrder(OrderRequest orderRequest)
        {
            if (orderRequest == null)
            {
                throw new RequestCheckException("The order request is null in call to PlaceOrder");
            }

            Account account = await _quoteDataService.GetAccount();
            if (orderRequest.Price == null || orderRequest.Price == "0.0")
            {
                QuoteData quote = await _quoteDataService.GetQuoteData(orderRequest.Symbol);
                orderRequest.Price = quote.BidPrice;
                if (quote.BidPrice == null || quote.BidPrice == "0.0")
                {
                    orderRequest.Price = quote.LastTradePrice;
                }
            }

            try
            {
                var response = await _httpClientManager.PostAsync<Order>(Constants.Routes.OrdersBase,
                        RbHelper.BuildOrderContent(orderRequest, account.Url));
                return response.Data;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Exception when placing an order, reason : {ex.Message}");
            }
        }
    }
}
