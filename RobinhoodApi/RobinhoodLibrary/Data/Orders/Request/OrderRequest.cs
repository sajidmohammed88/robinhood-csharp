﻿using RobinhoodApi.Data.Base;
using RobinhoodApi.Enum;

namespace RobinhoodApi.Data.Orders.Request;

public class OrderRequest : BaseOrderRequest
{
	public string InstrumentUrl { get; set; }

	public string Symbol { get; set; }

	public Trigger Trigger { get; set; }

	public string StopPrice { get; set; }
}
