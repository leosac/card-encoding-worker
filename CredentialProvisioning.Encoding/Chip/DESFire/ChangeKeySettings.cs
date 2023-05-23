namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class ChangeKeySettings : DESFireActionProperties
    {
        public override string Name => "Change Key Settings";

        public DESFireKeySettings KeySettings { get; set; }
    }
}
