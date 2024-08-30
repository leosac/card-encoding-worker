using LibLogicalAccess;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.Mifare
{
    public class ChangeKeys : MifareAction<Leosac.CredentialProvisioning.Encoding.Chip.Mifare.ChangeKeys>
    {
        public ChangeKeys(Leosac.CredentialProvisioning.Encoding.Chip.Mifare.ChangeKeys properties) : base(properties)
        {

        }

        public override void Run(MifareCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            MifareKey? llaKeyA = null, llaKeyB = null;
            if (Properties.KeyA != null)
            {
                var keyA = encodingCtx.Keys?.Get(Properties.KeyA, cardCtx.Credential?.VolatileKeys);
                if (keyA == null)
                {
                    throw new EncodingException("Cannot resolve the internal key A reference.");
                }

                llaKeyA = new MifareKey();
                CredentialKeyExt.SetKeyProperties(keyA, llaKeyA, cardCtx, Properties.KeyA?.Diversification);
            }
            if (Properties.KeyB != null)
            {
                var keyB = encodingCtx.Keys?.Get(Properties.KeyB, cardCtx.Credential?.VolatileKeys);
                if (keyB == null)
                {
                    throw new EncodingException("Cannot resolve the internal key B reference.");
                }

                llaKeyB = new MifareKey();
                CredentialKeyExt.SetKeyProperties(keyB, llaKeyB, cardCtx, Properties.KeyB?.Diversification);
            }

            var sab = new MifareAccessInfo.SectorAccessBits();
            switch (Properties.SectorAccessBits)
            {
                case Encoding.Chip.Mifare.SectorAccessBits.ARead_BWrite:
                    sab.setAReadBWriteConfiguration();
                    break;
                case Encoding.Chip.Mifare.SectorAccessBits.ARead_NeverWrite:
                    sab.setAReadNeverWriteConfiguration();
                    break;
                case Encoding.Chip.Mifare.SectorAccessBits.BRead_BWrite:
                    sab.setBReadBWriteConfiguration();
                    break;
                case Encoding.Chip.Mifare.SectorAccessBits.BRead_NeverWrite:
                    sab.setBReadNeverWriteConfiguration();
                    break;
                case Encoding.Chip.Mifare.SectorAccessBits.NeverRead_NeverWrite:
                    sab.setNeverReadNeverWriteConfiguration();
                    break;
                case Encoding.Chip.Mifare.SectorAccessBits.Nfc:
                    sab.setNfcConfiguration();
                    break;
                default:
                    sab.setTransportConfiguration();
                    break;
            }

            cmd.changeKeys(llaKeyA, llaKeyB, Properties.Sector, sab);
        }
    }
}
