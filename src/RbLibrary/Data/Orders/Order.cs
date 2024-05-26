using Rb.Integration.Api.Data.Base;
using Rb.Integration.Api.Enum;

namespace Rb.Integration.Api.Data.Orders;

public class Order : BaseOrder
{
	public string Url { get; set; }

	public string Account { get; set; }

	public string Position { get; set; }

	public string Cancel { get; set; }

	public string Instrument { get; set; }

	public string Fees { get; set; }

	public Trigger Trigger { get; set; }

	public string StopPrice { get; set; }

	public string RejectReason { get; set; }

	public bool ExtendedHours { get; set; }

	public bool OverrideDtbpChecks { get; set; }

	public bool OverrideDayTradeChecks { get; set; }

	public string ResponseCategory { get; set; }

	public string StopTriggeredAt { get; set; }

	public string LastTrailPrice { get; set; }

	public DateTime? LastTrailPriceUpdatedAt { get; set; }

	public TotalNotional DollarBasedAmount { get; set; }

	public TotalNotional TotalNotional { get; set; }

	public TotalNotional ExecutedNotional { get; set; }

	public string InvestmentScheduleId { get; set; }

	public bool IsIpoAccessOrder { get; set; }

	public string IpoAccessCancellationReason { get; set; }

	public string IpoAccessLowerCollaredPrice { get; set; }

	public string IpoAccessUpperCollaredPrice { get; set; }

	public string IpoAccessUpperPrice { get; set; }

	public string IpoAccessLowerPrice { get; set; }

	public bool IsIpoAccessPriceFinalized { get; set; }
}
