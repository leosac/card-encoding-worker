using System.Reflection;

namespace Leosac.CredentialProvisioning.Encoding
{
    public abstract class EncodingActionProperties
    {
        public const string Discriminator = "typeName";

        public class ActionTrigger
        {
            public EncodingActionProperties? CallAction { get; set; }

            public bool Throw { get; set; } = false;

            public string? ThrowCustomMessage { get; set; }
        }

        public abstract string Name { get; }

        public string? Label { get; set; }

        public string? Group { get; set; }

        public EncodingServiceProperties[]? ServicesBefore { get; set; }

        public EncodingServiceProperties[]? ServicesAfter { get; set; }

        public ActionTrigger OnSuccess { get; set; } = new ActionTrigger();

        public ActionTrigger OnFailure { get; set;  } = new ActionTrigger() { Throw = true };

        public static IEnumerable<Type> GetAllTypes()
        {
            var bt = typeof(EncodingActionProperties);
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => bt.IsAssignableFrom(t) && !t.IsAbstract);
        }
    }
}
