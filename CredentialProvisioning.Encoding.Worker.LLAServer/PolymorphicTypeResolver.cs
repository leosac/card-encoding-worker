using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLAServer
{
    public class PolymorphicTypeResolver : DefaultJsonTypeInfoResolver
    {
        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            var jsonTypeInfo = base.GetTypeInfo(type, options);

            var basePointType = typeof(EncodingActionProperties);
            if (jsonTypeInfo.Type == basePointType)
            {
                jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                {
                    TypeDiscriminatorPropertyName = EncodingActionProperties.Discriminator,
                    IgnoreUnrecognizedTypeDiscriminators = true,
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
                };
                var actions = EncodingActionProperties.GetAllTypes();
                foreach (var actionType in actions)
                {
                    jsonTypeInfo.PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(actionType, actionType.FullName));
                }
            }

            return jsonTypeInfo;
        }
    }
}
