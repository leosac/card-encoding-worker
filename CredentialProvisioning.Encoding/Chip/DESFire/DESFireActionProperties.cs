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
        Plain = 0,
        /// <summary>
        /// MACced communication
        /// </summary>
        MAC = 1,
        /// <summary>
        /// Encrypted communication
        /// </summary>
        Encrypt = 3,
        /// <summary>
        /// Unknown mode. Will try to be determinated automatically (may not be allowed)
        /// </summary>
        Unknown = 0xFF
    }

    /// <summary>
    /// Key Reference for Access Rights
    /// </summary>
    public enum TaskAccessRights
    {
        /// <summary>
        /// Key 0
        /// </summary>
        Key0 = 0,
        /// <summary>
        /// Key 1
        /// </summary>
        Key1 = 1,
        /// <summary>
        /// Key 2
        /// </summary>
        Key2 = 2,
        /// <summary>
        /// Key 3
        /// </summary>
        Key3 = 3,
        /// <summary>
        /// Key 4
        /// </summary>
        Key4 = 4,
        /// <summary>
        /// Key 5
        /// </summary>
        Key5 = 5,
        /// <summary>
        /// Key 6
        /// </summary>
        Key6 = 6,
        /// <summary>
        /// Key 7
        /// </summary>
        Key7 = 7,
        /// <summary>
        /// Key 8
        /// </summary>
        Key8 = 8,
        /// <summary>
        /// Key 9
        /// </summary>
        Key9 = 9,
        /// <summary>
        /// Key 10
        /// </summary>
        Key10 = 10,
        /// <summary>
        /// Key 11
        /// </summary>
        Key11 = 11,
        /// <summary>
        /// Key 12
        /// </summary>
        Key12 = 12,
        /// <summary>
        /// Key 13
        /// </summary>
        Key13 = 13,
        /// <summary>
        /// No key required
        /// </summary>
        Free = 14,
        /// <summary>
        /// Access right disabled
        /// </summary>
        Never = 15
    }

    /// <summary>
    /// DESFire Key Type.
    /// </summary>
    public enum DESFireKeyType
    {
        /// <summary>
        /// DES / 2K3DES
        /// </summary>
        DES = 0,
        /// <summary>
        /// 3K3DES
        /// </summary>
        TK3DES = 0x40,
        /// <summary>
        /// AES 128
        /// </summary>
        AES128 = 0x80
    }

    /// <summary>
    /// DESFire Key Settings
    /// </summary>
    [Flags]
    public enum DESFireKeySettings
    {
        /// <summary>
        /// Allow Master Key change
        /// </summary>
        AllowChangeMasterKey = 1,
        /// <summary>
        /// Free listing without Master Key
        /// </summary>
        FreeListingWithoutMasterKey = 2,
        /// <summary>
        /// Free Create and Delete without Master Key
        /// </summary>
        FreeCreateDeleteWithoutMasterKey = 4,
        /// <summary>
        /// Configuration is changeable
        /// </summary>
        ConfigurationChangeable = 8,
        /// <summary>
        /// Change keys with Master Key. Deprecated by changeKey parameter.
        /// </summary>
        ChangeKeyWithMasterKey = 0,
        /// <summary>
        /// Change keys with targeted key number. Deprecated by changeKey parameter.
        /// </summary>
        ChangeKeyWithTargetedKeyNo = 224,
        /// <summary>
        /// Change key is frozen. Deprecated by changeKey parameter.
        /// </summary>
        ChangeKeyFrozen = 240,
        /// <summary>
        /// Default key settings
        /// </summary>
        Default = 11
    }

    /// <summary>
    /// FID Support
    /// </summary>
    public enum FidSupport
    {
        /// <summary>
        /// ISO FID disabled
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// ISO FID enabled
        /// </summary>
        Enabled = 0x20
    }

    /// <summary>
    /// DESFire File Access Rights
    /// </summary>
    public struct DESFireAccessRights
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
        public TaskAccessRights ReadAccess { get; set; }
        /// <summary>
        /// Write access
        /// </summary>
        public TaskAccessRights WriteAccess { get; set; }
        /// <summary>
        /// Read and Write access
        /// </summary>
        public TaskAccessRights ReadAndWriteAccess { get; set; }
        /// <summary>
        /// Access Rights change access
        /// </summary>
        public TaskAccessRights ChangeAccess { get; set; }
    }
}
