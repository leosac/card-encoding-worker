namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class CreateApplication : DESFireActionProperties
    {
        public override string Name => "Create Application";

        public uint AID { get; set; }

        public DESFireKeySettings KeySettings { get; set; }

        public byte MaxNbKeys { get; set; }
    }
}
