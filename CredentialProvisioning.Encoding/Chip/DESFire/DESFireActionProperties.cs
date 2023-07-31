namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    /// <summary>
    /// Base class for DESFire Encoding Action Properties
    /// </summary>
    public abstract class DESFireActionProperties : EncodingActionProperties
    {
    }

    /*
     * We redefine Enums and Structs to avoid a direct dependency on LLA at this level.
     * This may have to be refactored if the redefinition list get too big.
     */

    /// <summary>
    /// The communication encryption mode.
    /// </summary>
    public enum EncryptionMode
    {
        /// <summary>
        /// Plain communication
        /// </summary>
        CM_PLAIN = 0,
        /// <summary>
        /// MACced communication
        /// </summary>
        CM_MAC = 1,
        /// <summary>
        /// Encrypted communication
        /// </summary>
        CM_ENCRYPT = 3,
        /// <summary>
        /// Unknown mode. Will try to be determinated automatically (may not be allowed)
        /// </summary>
        CM_UNKNOWN = 0xFF
    }

    /// <summary>
    /// Key Reference for Access Rights
    /// </summary>
    public enum TaskAccessRights
    {
        /// <summary>
        /// Key 0
        /// </summary>
        AR_KEY0,
        /// <summary>
        /// Key 1
        /// </summary>
        AR_KEY1,
        /// <summary>
        /// Key 2
        /// </summary>
        AR_KEY2,
        /// <summary>
        /// Key 3
        /// </summary>
        AR_KEY3,
        /// <summary>
        /// Key 4
        /// </summary>
        AR_KEY4,
        /// <summary>
        /// Key 5
        /// </summary>
        AR_KEY5,
        /// <summary>
        /// Key 6
        /// </summary>
        AR_KEY6,
        /// <summary>
        /// Key 7
        /// </summary>
        AR_KEY7,
        /// <summary>
        /// Key 8
        /// </summary>
        AR_KEY8,
        /// <summary>
        /// Key 9
        /// </summary>
        AR_KEY9,
        /// <summary>
        /// Key 10
        /// </summary>
        AR_KEY10,
        /// <summary>
        /// Key 11
        /// </summary>
        AR_KEY11,
        /// <summary>
        /// Key 12
        /// </summary>
        AR_KEY12,
        /// <summary>
        /// Key 13
        /// </summary>
        AR_KEY13,
        /// <summary>
        /// No key required
        /// </summary>
        AR_FREE,
        /// <summary>
        /// Access right disabled
        /// </summary>
        AR_NEVER
    }

    /// <summary>
    /// DESFire Key Type.
    /// </summary>
    public enum DESFireKeyType
    {
        /// <summary>
        /// DES / 2K3DES
        /// </summary>
        DF_KEY_DES = 0,
        /// <summary>
        /// 3K3DES
        /// </summary>
        DF_KEY_3K3DES = 0x40,
        /// <summary>
        /// AES 128
        /// </summary>
        DF_KEY_AES = 0x80
    }

    /// <summary>
    /// DESFire Key Settings
    /// </summary>
    public enum DESFireKeySettings
    {
        /// <summary>
        /// Allow Master Key change
        /// </summary>
        KS_ALLOW_CHANGE_MK = 1,
        /// <summary>
        /// Free listing without Master Key
        /// </summary>
        KS_FREE_LISTING_WITHOUT_MK = 2,
        /// <summary>
        /// Free Create and Delete without Master Key
        /// </summary>
        KS_FREE_CREATE_DELETE_WITHOUT_MK = 4,
        /// <summary>
        /// Configuration is changeable
        /// </summary>
        KS_CONFIGURATION_CHANGEABLE = 8,
        /// <summary>
        /// Change keys with Master Key
        /// </summary>
        KS_CHANGE_KEY_WITH_MK = 0,
        /// <summary>
        /// Change keys with targated key number
        /// </summary>
        KS_CHANGE_KEY_WITH_TARGETED_KEYNO = 224,
        /// <summary>
        /// Change key is frozen
        /// </summary>
        KS_CHANGE_KEY_FROZEN = 240,
        /// <summary>
        /// Default key settings
        /// </summary>
        KS_DEFAULT = 11
    }

    /// <summary>
    /// FID Support
    /// </summary>
    public enum FidSupport
    {
        /// <summary>
        /// ISO FID disabled
        /// </summary>
        FIDS_NO_ISO_FID = 0,
        /// <summary>
        /// ISO FID enabled
        /// </summary>
        FIDS_ISO_FID = 0x20
    }

    /// <summary>
    /// DESFire File Access Rights
    /// </summary>
    public readonly struct DESFireAccessRights
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="read">Read access</param>
        /// <param name="write">Write access</param>
        /// <param name="readAndWrite">Read and Write access</param>
        /// <param name="change">Access Rights change access</param>
        public DESFireAccessRights(TaskAccessRights read, TaskAccessRights write, TaskAccessRights readAndWrite, TaskAccessRights change)
        {
            ReadAccess = read;
            WriteAccess = write;
            ReadAndWriteAccess = readAndWrite;
            ChangeAccess = change;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="accesses">Array of strings which can be parsed to DESFireAccessRights.</param>
        public DESFireAccessRights(string[] accesses)
        {
            ReadAccess = (TaskAccessRights)Enum.Parse(typeof(TaskAccessRights), accesses[0]);
            WriteAccess = (TaskAccessRights)Enum.Parse(typeof(TaskAccessRights), accesses[1]);
            ReadAndWriteAccess = (TaskAccessRights)Enum.Parse(typeof(TaskAccessRights), accesses[2]);
            ChangeAccess = (TaskAccessRights)Enum.Parse(typeof(TaskAccessRights), accesses[3]);
        }

        /// <summary>
        /// Read access
        /// </summary>
        public TaskAccessRights ReadAccess { get; }
        /// <summary>
        /// Write access
        /// </summary>
        public TaskAccessRights WriteAccess { get; }
        /// <summary>
        /// Read and Write access
        /// </summary>
        public TaskAccessRights ReadAndWriteAccess { get; }
        /// <summary>
        /// Access Rights change access
        /// </summary>
        public TaskAccessRights ChangeAccess { get; }
    }
}
