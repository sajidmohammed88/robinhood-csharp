namespace System.Text.Json;

public static class CustomJsonSerializerOptions
{
	/// <summary>
	/// static constructor for <see cref="CustomJsonSerializerOptions"/>
	/// </summary>
	static CustomJsonSerializerOptions()
	{
		Current = new Lazy<JsonSerializerOptions>(() => new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
			Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, false) }
		});
	}

	/// <summary>
	/// gets the singleton <see cref="JsonSerializerOptions"/> instance
	/// </summary>
	public static Lazy<JsonSerializerOptions> Current { get; }
}
