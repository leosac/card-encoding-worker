namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public abstract class DESFireActionProperties : EncodingActionProperties
    {
    }

    /*
     * We redefine Enums and Structs to avoid a direct dependency on LLA at this level.
     * This may have to be refactored if the redefinition list get too big.
     */

    public enum EncryptionMode
    {
        CM_PLAIN = 0,
        CM_MAC = 1,
        CM_ENCRYPT = 3,
        CM_UNKNOWN = 0xFF
    }

    public enum TaskAccessRights
    {
        AR_KEY0,
        AR_KEY1,
        AR_KEY2,
        AR_KEY3,
        AR_KEY4,
        AR_KEY5,
        AR_KEY6,
        AR_KEY7,
        AR_KEY8,
        AR_KEY9,
        AR_KEY10,
        AR_KEY11,
        AR_KEY12,
        AR_KEY13,
        AR_FREE,
        AR_NEVER
    }

    public enum DESFireKeyType
    {
        DF_KEY_DES = 0,
        DF_KEY_3K3DES = 0x40,
        DF_KEY_AES = 0x80
    }

    public enum DESFireKeySettings
    {
        KS_ALLOW_CHANGE_MK = 1,
        KS_FREE_LISTING_WITHOUT_MK = 2,
        KS_FREE_CREATE_DELETE_WITHOUT_MK = 4,
        KS_CONFIGURATION_CHANGEABLE = 8,
        KS_CHANGE_KEY_WITH_MK = 0,
        KS_CHANGE_KEY_WITH_TARGETED_KEYNO = 224,
        KS_CHANGE_KEY_FROZEN = 240,
        KS_DEFAULT = 11
    }

    public enum FidSupport
    {
        FIDS_NO_ISO_FID = 0,
        FIDS_ISO_FID = 0x20
    }

    public readonly struct DESFireAccessRights
    {
        public DESFireAccessRights(TaskAccessRights read, TaskAccessRights write, TaskAccessRights readAndWrite, TaskAccessRights change)
        {
            ReadAccess = read;
            WriteAccess = write;
            ReadAndWriteAccess = readAndWrite;
            ChangeAccess = change;
        }

        public DESFireAccessRights(string[] accesses)
        {
            ReadAccess = (TaskAccessRights)Enum.Parse(typeof(TaskAccessRights), accesses[0]);
            WriteAccess = (TaskAccessRights)Enum.Parse(typeof(TaskAccessRights), accesses[1]);
            ReadAndWriteAccess = (TaskAccessRights)Enum.Parse(typeof(TaskAccessRights), accesses[2]);
            ChangeAccess = (TaskAccessRights)Enum.Parse(typeof(TaskAccessRights), accesses[3]);
        }

        public TaskAccessRights ReadAccess { get; }
        public TaskAccessRights WriteAccess { get; }
        public TaskAccessRights ReadAndWriteAccess { get; }
        public TaskAccessRights ChangeAccess { get; }
    }
}
