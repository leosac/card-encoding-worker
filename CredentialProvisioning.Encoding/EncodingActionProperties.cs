using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Leosac.CredentialProvisioning.Encoding
{
    public abstract class EncodingActionProperties
    {
        public static readonly string Discriminator = "ActionType";

        public class ActionTrigger
        {
            public EncodingActionProperties? CallAction { get; set; }

            public bool Throw { get; set; } = false;

            public string? ThrowCustomMessage { get; set; }
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public abstract string Name { get; }

        public string? Label { get; set; }

        public ActionTrigger OnSuccess { get; } = new ActionTrigger();

        public ActionTrigger OnFailure { get; } = new ActionTrigger() { Throw = true };

        public static IEnumerable<Type> GetAllTypes()
        {
            var bt = typeof(EncodingActionProperties);
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => bt.IsAssignableFrom(t) && !t.IsAbstract);
        }
    }
}
