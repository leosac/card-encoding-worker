namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class SelectApplication : DESFireActionProperties
    {
        public override string Name => "Select Application";

        public uint AID { get; set; }
    }
}
