using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    /// <summary>
    /// JSON serialization for `[Flags]` based `enum's` as `string[]`
    /// </summary>
    /// <see href="https://github.com/dotnet/runtime/issues/31081#issuecomment-848697673">based on this model</see>
    public class EnumWithFlagsJsonConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, System.Enum
    {
        private readonly JsonNamingPolicy? _namingPolicy;
        private readonly Dictionary<TEnum, string> _enumToString = new Dictionary<TEnum, string>();
        private readonly Dictionary<string, TEnum> _stringToEnum = new Dictionary<string, TEnum>();

        public EnumWithFlagsJsonConverter(JsonNamingPolicy? namingPolicy = null)
        {
            var type = typeof(TEnum);
            var values = System.Enum.GetValues<TEnum>();
            _namingPolicy = namingPolicy;

            foreach (var value in values)
            {
                var enumMember = type.GetMember(value.ToString())[0];
                var attr = enumMember.GetCustomAttributes(typeof(EnumMemberAttribute), false)
                  .Cast<EnumMemberAttribute>()
                  .FirstOrDefault();

                _stringToEnum.Add(GetFormattedName(value.ToString()), value);

                if (attr?.Value != null)
                {
                    _enumToString.Add(value, GetFormattedName(attr.Value));
                    _stringToEnum.Add(GetFormattedName(attr.Value), value);
                }
                else
                {
                    _enumToString.Add(value, GetFormattedName(value.ToString()));
                }
            }
        }

        private string GetFormattedName(string name)
        {
            return _namingPolicy != null ? _namingPolicy.ConvertName(name) : name;
        }

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return default(TEnum);
                case JsonTokenType.StartArray:
                    {
                        TEnum ret = default(TEnum);
                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndArray)
                                break;
                            var stringValue = reader.GetString();
                            if (_stringToEnum.TryGetValue(stringValue, out var _enumValue))
                            {
                                ret = Or(ret, _enumValue);
                            }
                        }
                        return ret;
                    }
                case JsonTokenType.String:
                    {
                        TEnum ret = default(TEnum);
                        var stringValues = reader.GetString();
                        if (!string.IsNullOrEmpty(stringValues))
                        {
                            var stringsValue = stringValues.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                            foreach (var stringValue in stringsValue)
                            {
                                if (_stringToEnum.TryGetValue(stringValue, out var _enumValue))
                                {
                                    ret = Or(ret, _enumValue);
                                }
                            }
                        }
                        return ret;
                    }
                default:
                    throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            var values = System.Enum.GetValues<TEnum>();
            writer.WriteStartArray();
            foreach (var _value in values)
            {
                if (value.HasFlag(_value))
                {
                    var v = Convert.ToInt32(_value);
                    if (v == 0)
                    {
                        // handle "0" case which HasFlag matches to all values
                        // --> only write "0" case if it is the only value present
                        if (value.Equals(_value))
                        {
                            writer.WriteStringValue(_enumToString[_value]);
                        }
                    }
                    else
                    {
                        writer.WriteStringValue(_enumToString[_value]);
                    }
                }
            }
            writer.WriteEndArray();
        }

        /// <summary>
        /// Combine two enum flag values into single enum value.
        /// </summary>
        // <see href="https://stackoverflow.com/a/24172851/5219886">based on this SO</see>
        static TEnum Or(TEnum a, TEnum b)
        {
            if (Enum.GetUnderlyingType(a.GetType()) != typeof(ulong))
                return (TEnum)Enum.ToObject(a.GetType(), Convert.ToInt64(a) | Convert.ToInt64(b));
            else
                return (TEnum)Enum.ToObject(a.GetType(), Convert.ToUInt64(a) | Convert.ToUInt64(b));
        }
    }
}
