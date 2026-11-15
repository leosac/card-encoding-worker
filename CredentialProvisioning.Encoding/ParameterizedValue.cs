using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Leosac.CredentialProvisioning.Encoding
{
    [JsonConverter(typeof(ParameterizedValueJsonConverterFactory))]
    public readonly struct ParameterizedValue<T> where T : struct, IConvertible
    {
        public T? Value { get; }
        public string? ParameterName { get; }

        public ParameterizedValue(T value)
        {
            Value = value;
            ParameterName = null;
        }

        public ParameterizedValue(string parameterName)
        {
            Value = null;
            ParameterName = parameterName;
        }

        public T Resolve(IReadOnlyDictionary<string, object?> parameters)
        {
            if (Value.HasValue)
                return Value.Value;

            if (ParameterName == null || !parameters.TryGetValue(ParameterName, out var value) || value == null)
                throw new EncodingException($"Cannot resolve template parameter `{ParameterName}`.");

            if (value is JsonElement element)
                value = element.ValueKind == JsonValueKind.String ? element.GetString() : element.GetRawText();

            try
            {
                return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
            }
            catch (Exception ex) when (ex is InvalidCastException or FormatException or OverflowException)
            {
                throw new EncodingException($"Template parameter `{ParameterName}` has an invalid value for {typeof(T).Name}.", ex);
            }
        }

        public static implicit operator T(ParameterizedValue<T> value) => value.Value ?? throw new InvalidOperationException("The parameterized value has not been resolved.");
        public static implicit operator ParameterizedValue<T>(T value) => new(value);
    }

    public sealed class ParameterizedValueJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(ParameterizedValue<>);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var valueType = typeToConvert.GetGenericArguments()[0];
            return (JsonConverter)Activator.CreateInstance(typeof(ParameterizedValueJsonConverter<>).MakeGenericType(valueType))!;
        }
    }

    internal sealed class ParameterizedValueJsonConverter<T> : JsonConverter<ParameterizedValue<T>> where T : struct, IConvertible
    {
        public override ParameterizedValue<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString()!;
                if (value.StartsWith("${", StringComparison.Ordinal) && value.EndsWith('}'))
                    return new ParameterizedValue<T>(value[2..^1]);

                return new ParameterizedValue<T>((T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture));
            }

            if (reader.TokenType == JsonTokenType.Number)
                return new ParameterizedValue<T>((T)Convert.ChangeType(reader.GetDecimal(), typeof(T), CultureInfo.InvariantCulture));

            throw new JsonException($"Expected a {typeof(T).Name} or a template parameter.");
        }

        public override void Write(Utf8JsonWriter writer, ParameterizedValue<T> value, JsonSerializerOptions options)
        {
            if (value.ParameterName != null)
                writer.WriteStringValue($"${{{value.ParameterName}}}");
            else
                JsonSerializer.Serialize(writer, value.Value!.Value, options);
        }
    }
}
