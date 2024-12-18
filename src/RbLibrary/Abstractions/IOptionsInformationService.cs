namespace Rb.Integration.Api.Abstractions;

/// <summary>
/// Options information service that responsible on options endpoints call.
/// </summary>
public interface IOptionsInformationService
{
	/// <summary>
	/// Get the chain.
	/// </summary>
	/// <param name="instrumentId">The instrument identifier.</param>
	/// <returns>The chain.</returns>
	Task<Chain> GetChainAsync(string instrumentId);

	/// <summary>
	/// Gets the options by chain identifier.
	/// </summary>
	/// <param name="chainId">The chain identifier.</param>
	/// <param name="expirationDates">The expiration dates.</param>
	/// <param name="optionType">Type of the option.</param>
	/// <returns>instrument options chain.</returns>
	Task<IList<Option>> GetOptionsByChainIdAsync(Guid? chainId, IList<string> expirationDates, OptionType optionType);

	/// <summary>
	/// Get the owned options.
	/// </summary>
	/// <returns>The owned options.</returns>
	Task<IList<Option>> GetOwnedOptionsAsync();

	/// <summary>
	/// Gets the option chain identifier by symbol.
	/// </summary>
	/// <param name="symbol">The symbol.</param>
	/// <returns>The chain identifier.</returns>
	Task<Guid> GetOptionChainIdAsync(string symbol);

	/// <summary>
	/// Gets the option quote.
	/// </summary>
	/// <param name="symbol">The symbol.</param>
	/// <param name="strike">The strike.</param>
	/// <param name="expirationDate">The expiration date.</param>
	/// <param name="optionType">Type of the option.</param>
	/// <param name="state">The state.</param>
	/// <returns>Option quote</returns>
	Task<Guid?> GetOptionQuoteAsync(string symbol, string strike, string expirationDate, OptionType optionType,
		string state = "active");

	/// <summary>
	/// Get a list of market data for a given option id.
	/// </summary>
	/// <param name="optionId">The option identifier.</param>
	/// <returns>The market data list.</returns>
	Task<dynamic> GetOptionMarketDataAsync(string optionId);
}
