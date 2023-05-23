namespace Leosac.CredentialProvisioning.Encoding.Chip.DESFire
{
    public class CreateValueFile : CreateFile
    {
        public override string Name => "Create Value File";

        public int LowerLimit { get; set; }

        public int UpperLimit { get; set; }

        public int InitialValue { get; set; }

        public bool LimitedCreditEnabled { get; set; }
    }
}
