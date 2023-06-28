namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.DESFire
{
    public static class DESFireExt
    {
        public static LibLogicalAccess.Card.DESFireAccessRights ConvertForLLA(this Leosac.CredentialProvisioning.Encoding.Chip.DESFire.DESFireAccessRights rights)
        {
            LibLogicalAccess.Card.DESFireAccessRights r = new LibLogicalAccess.Card.DESFireAccessRights();
            r.readAccess = (LibLogicalAccess.Card.TaskAccessRights)rights.ReadAccess;
            r.writeAccess = (LibLogicalAccess.Card.TaskAccessRights)rights.WriteAccess;
            r.readAndWriteAccess = (LibLogicalAccess.Card.TaskAccessRights)rights.ReadAndWriteAccess;
            r.changeAccess = (LibLogicalAccess.Card.TaskAccessRights)rights.ChangeAccess;
            return r;
        }
    }
}
