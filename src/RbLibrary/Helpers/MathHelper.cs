namespace Rb.Integration.Api.Helpers;
public static class MathHelper
{
	public static double RoundPrice(double price)
	{
		if (price <= 1e-2)
		{
			return Math.Round(price, 6);
		}

		if (price < 1e0)
		{
			return Math.Round(price, 4);
		}

		return Math.Round(price, 2);
	}
}
