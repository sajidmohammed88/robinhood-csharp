using System.Text.Json.Serialization;

namespace System.Text.Json
{
    public static class CustomJsonSerializerOptions
    {
        /// <summary>
        /// static constructor for <see cref="CustomJsonSerializerOptions"/>
        /// </summary>
        static CustomJsonSerializerOptions()
        {
            Current = new JsonSerializerOptions
            {
                PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance,
                Converters = { new JsonStringEnumConverter(SnakeCaseNamingPolicy.Instance, false) }
            };
        }

        /// <summary>
        /// gets the singleton <see cref="JsonSerializerOptions"/> instance
        /// </summary>
        public static JsonSerializerOptions Current { get; }
    }
}
