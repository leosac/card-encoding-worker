namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class CreateApplicationEV1 : DESFireActionProperties
    {
        public CreateApplicationEV1()
        {
            KeyType = DESFireKeyType.DF_KEY_AES;
            FidSupport = FidSupport.FIDS_NO_ISO_FID;
        }

        public override string Name => "Create Application EV1";

        public uint AID { get; set; }

        public DESFireKeySettings KeySettings { get; set; }

        public byte MaxNbKeys { get; set; }

        public DESFireKeyType KeyType { get; set; }

        public FidSupport FidSupport { get; set; }

        public ushort IsoFID { get; set; }

        public string? IsoDFName { get; set; }
    }
}
