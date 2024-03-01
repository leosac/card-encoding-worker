using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.IO;

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
                    var typeName = GetSubTypeDiscriminator(derivedType);
                    jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(derivedType, typeName));
                }
            }
        }

        public static string GetSubTypeDiscriminator(Type subType, bool fullName = true)
        {
            string? namespacePrefix = null;
            if (subType.IsAssignableTo(typeof(EncodingActionProperties)))
            {
                namespacePrefix = "Leosac.CredentialProvisioning.Encoding.Chip.";
            }
            else if (subType.IsAssignableTo(typeof(EncodingServiceProperties)))
            {
                namespacePrefix = "Leosac.CredentialProvisioning.Encoding.Services.";
            }

            var typeName = subType.FullName;
            if (!string.IsNullOrEmpty(namespacePrefix) && typeName.StartsWith(namespacePrefix))
            {
                typeName = typeName.Remove(0, namespacePrefix.Length);
            }
            else
            {
                if (!fullName)
                {
                    typeName = subType.Name;
                }
            }

            return typeName;
        }
    }
}
