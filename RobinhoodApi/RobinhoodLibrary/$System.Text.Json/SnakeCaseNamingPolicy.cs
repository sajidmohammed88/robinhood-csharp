using Newtonsoft.Json.Serialization;

namespace System.Text.Json
{
    internal class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        private readonly SnakeCaseNamingStrategy _snakeCaseNamingStrategy = new SnakeCaseNamingStrategy();
        public static SnakeCaseNamingPolicy Instance { get; } = new SnakeCaseNamingPolicy();

        public override string ConvertName(string name)
        {
            return _snakeCaseNamingStrategy.GetPropertyName(name, false);
        }
    }
}
