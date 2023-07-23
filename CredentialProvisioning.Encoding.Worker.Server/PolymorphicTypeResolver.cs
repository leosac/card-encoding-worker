using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
    {
        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            var jsonTypeInfo = base.GetTypeInfo(type, options);

            HandlePolymorphism(jsonTypeInfo, typeof(EncodingActionProperties), EncodingActionProperties.Discriminator, EncodingActionProperties.GetAllTypes());
            HandlePolymorphism(jsonTypeInfo, typeof(EncodingServiceProperties), EncodingServiceProperties.Discriminator, EncodingServiceProperties.GetAllTypes());

            return jsonTypeInfo;
        }

        public void HandlePolymorphism(JsonTypeInfo jsonTypeInfo, Type basePointType, string discriminator, IEnumerable<Type> derivedTypes)
        {
            if (jsonTypeInfo.Type == basePointType)
            {
                jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                {
                    TypeDiscriminatorPropertyName = discriminator,
                    IgnoreUnrecognizedTypeDiscriminators = false,
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
                };
                foreach (var derivedType in derivedTypes)
                {
                    jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(derivedType, derivedType.FullName));
                }
            }
        }
    }
}
