using LibLogicalAccess;
using LibLogicalAccess.Card;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Chip.UltralightC
{
    public class Authenticate : UltralightCAction<Leosac.CredentialProvisioning.Encoding.Chip.UltralightC.Authenticate>
    {
        public Authenticate(Leosac.CredentialProvisioning.Encoding.Chip.UltralightC.Authenticate properties) : base(properties)
        {

        }

        public override void Run(MifareUltralightCCommands cmd, EncodingContext encodingCtx, LLACardContext cardCtx)
        {
            if (Properties.Key == null)
            {
                throw new EncodingException("A key must be defined.");
            }
            var key = encodingCtx.Keys?.Get(Properties.Key, cardCtx.Credential?.VolatileKeys);
            if (key == null)
            {
                throw new EncodingException("Cannot resolve the internal key reference.");
            }
            var llaKey = new TripleDESKey();
            CredentialKeyExt.SetKeyProperties(key, llaKey, cardCtx, Properties.Key?.Diversification);

            cmd.authenticate(llaKey);
        }
    }
}
