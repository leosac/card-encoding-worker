using System.Reflection;

namespace Leosac.CredentialProvisioning.Encoding
{
    public abstract class EncodingServiceProperties
    {
        public const string Discriminator = "typeName";

        public EncodingServiceBufferBehavior BufferBehavior { get; set; }

        public string? FieldNamePrefix { get; set; }

        public static IEnumerable<Type> GetAllTypes()
        {
            var bt = typeof(EncodingServiceProperties);
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => bt.IsAssignableFrom(t) && !t.IsAbstract);
        }
    }
}
