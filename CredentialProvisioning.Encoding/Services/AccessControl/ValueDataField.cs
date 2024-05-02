namespace Leosac.CredentialProvisioning.Encoding.Services.AccessControl
{
    public abstract class ValueDataField : AccessControlDataField
    {
        /// <summary>
        /// True if the field always have a fixed value, false otherwise.
        /// </summary>
        public bool IsFixedField { get; set; }

        /// <summary>
        /// True if the field is an identifier, false otherwise.
        /// </summary>
        public bool IsIdentifier { get; set; }

        /// <summary>
        /// The field length (in bits).
        /// </summary>
        public uint Length { get; set; }

        /// <summary>
        /// The field data type
        /// </summary>
        public FieldDataType DataType { get; set; } = FieldDataType.Binary;

        /// <summary>
        /// The field data representation
        /// </summary>
        public FieldDataRepresentation DataRepresentation { get; set; } = FieldDataRepresentation.BigEndian;
    }
}
