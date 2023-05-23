namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class DeleteApplication : DESFireActionProperties
    {
        public override string Name => "Delete Application";

        public uint AID { get; set; }
    }
}
