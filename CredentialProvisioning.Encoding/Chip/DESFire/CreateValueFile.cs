namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Create a new value file.
    /// </summary>
    public class CreateValueFile : CreateFile
    {
        /// <summary>
        /// See <see cref="EncodingActionProperties.Name" />.
        /// </summary>
        public override string Name => "Create Value File";

        /// <summary>
        /// The lower limit.
        /// </summary>
        public int LowerLimit { get; set; }

        /// <summary>
        /// The upper limit.
        /// </summary>
        public int UpperLimit { get; set; }

        /// <summary>
        /// The file initial value.
        /// </summary>
        public int InitialValue { get; set; }

        /// <summary>
        /// True to enable limited credit (on a same transaction with an initial debit operation), false otherwise.
        /// </summary>
        public bool LimitedCreditEnabled { get; set; }
    }
}
