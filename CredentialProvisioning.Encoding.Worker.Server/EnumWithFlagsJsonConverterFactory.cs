using System.Text.Json.Serialization;
using System.Text.Json;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    /// <summary>
    /// JSON serialization factory for `[Flags]` based `enum's` as `string[]`
    /// </summary>
    /// <see href="https://stackoverflow.com/a/59430729/5219886">based on this model</see>
    public class EnumWithFlagsJsonConverterFactory : JsonConverterFactory
    {
        private readonly JsonNamingPolicy? _namingPolicy;

        public EnumWithFlagsJsonConverterFactory(JsonNamingPolicy? namingPolicy = null)
        {
            _namingPolicy = namingPolicy;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            // https://github.com/dotnet/runtime/issues/42602#issue-706711292
            return typeToConvert.IsEnum && typeToConvert.IsDefined(typeof(FlagsAttribute), false);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(EnumWithFlagsJsonConverter<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType, [_namingPolicy]);
        }
    }
}
