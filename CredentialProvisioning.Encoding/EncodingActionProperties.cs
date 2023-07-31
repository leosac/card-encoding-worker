using System.Reflection;
using System.Text.Json.Serialization;

namespace Leosac.CredentialProvisioning.Encoding
{
    /// <summary>
    /// Base class for Encoding Action Properties
    /// </summary>
    public abstract class EncodingActionProperties
    {
        /// <summary>
        /// Discriminator for Encoding Action Properties serialization
        /// </summary>
        public const string Discriminator = "$type";

        /// <summary>
        /// Action Trigger definition
        /// </summary>
        public class ActionTrigger
        {
            /// <summary>
            /// The following action to execute.
            /// </summary>
            public EncodingActionProperties? CallAction { get; set; }

            /// <summary>
            /// True to throw an exception and end the fragment production, false otherwise.
            /// </summary>
            public bool Throw { get; set; } = false;

            /// <summary>
            /// An optional custom message to throw (if enabled).
            /// </summary>
            public string? ThrowCustomMessage { get; set; }
        }

        /// <summary>
        /// The action name.
        /// </summary>
        [JsonIgnore]
        public abstract string Name { get; }

        /// <summary>
        /// The optional action indicative label.
        /// </summary>
        public string? Label { get; set; }

        /// <summary>
        /// The optional action group, mainly used to regroup visually several operations.
        /// </summary>
        public string? Group { get; set; }

        /// <summary>
        /// The card services to execute before the action.
        /// </summary>
        public EncodingServiceProperties[]? ServicesBefore { get; set; }

        /// <summary>
        /// The card services to execute after the action.
        /// </summary>
        public EncodingServiceProperties[]? ServicesAfter { get; set; }

        /// <summary>
        /// The action to trigger on action execution success.
        /// </summary>
        public ActionTrigger OnSuccess { get; set; } = new ActionTrigger();

        /// <summary>
        /// The action to trigger on action execution failure.
        /// </summary>
        public ActionTrigger OnFailure { get; set;  } = new ActionTrigger() { Throw = true };

        /// <summary>
        /// Get all encoding actions properties types from the executing assemblyss.
        /// </summary>
        /// <returns>The encoding actions properties types.</returns>
        public static IEnumerable<Type> GetAllTypes()
        {
            var bt = typeof(EncodingActionProperties);
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => bt.IsAssignableFrom(t) && !t.IsAbstract);
        }
    }
}
