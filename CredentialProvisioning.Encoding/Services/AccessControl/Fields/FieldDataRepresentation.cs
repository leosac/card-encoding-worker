namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl.Fields
{
    /// <summary>
    /// The field data representation (= byte-sex)
    /// </summary>
    public enum FieldDataRepresentation
    {
        /// <summary>
        /// Big Endian data representation.
        /// </summary>
        BigEndian = 4,
        /// <summary>
        /// Little Endian data representation.
        /// </summary>
        LittleEndian = 5,
        /// <summary>
        /// No specific data representation (will depends on the system/processor)
        /// </summary>
        None = 6
    }
}
