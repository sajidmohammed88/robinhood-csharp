namespace Rb.Integration.Api.Helpers;
public static class MathHelper
{
	public static double RoundPrice(double price)
	{
		double returnPrice;

		if (price <= 1e-2)
		{
			returnPrice = Math.Round(price, 6);
		}
		else if (price < 1e0)
		{
			returnPrice = Math.Round(price, 4);
		}
		else
		{
			returnPrice = Math.Round(price, 2);
		}

		return returnPrice;
	}
}
