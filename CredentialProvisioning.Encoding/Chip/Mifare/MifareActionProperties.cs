namespace Leosac.CredentialProvisioning.Encoding.Chip.Mifare
{
    /// <summary>
    /// Base class for Mifare Classic Encoding Action Properties
    /// </summary>
    public abstract class MifareActionProperties : EncodingActionProperties
    {
    }

    /// <summary>
    /// Mifare Classic key type.
    /// </summary>
    public enum MifareKeyType : byte
    {
        /// <summary>
        /// Key A
        /// </summary>
        KeyA = 96,
        /// <summary>
        /// Key B
        /// </summary>
        KeyB = 97
    }

    /// <summary>
    /// The sector access bits pre-defined mode.
    /// </summary>
    public enum SectorAccessBits
    {
        /// <summary>
        /// Read with key A, write with key B
        /// </summary>
        ARead_BWrite,
        /// <summary>
        /// Read with key A, never write
        /// </summary>
        ARead_NeverWrite,
        /// <summary>
        /// Read and write with key B
        /// </summary>
        BRead_BWrite,
        /// <summary>
        /// Read with key B, never write
        /// </summary>
        BRead_NeverWrite,
        /// <summary>
        /// Never read and never write
        /// </summary>
        NeverRead_NeverWrite,
        /// <summary>
        /// Default configuration for NFC tags
        /// </summary>
        Nfc,
        /// <summary>
        /// Transport configuration, read and write with key A
        /// </summary>
        Transport
    }
}
