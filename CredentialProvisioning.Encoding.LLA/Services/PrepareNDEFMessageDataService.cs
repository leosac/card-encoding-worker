using Leosac.CredentialProvisioning.Encoding.Key;
using LibLogicalAccess;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class PrepareNDEFMessageDataService(Encoding.Services.PrepareNdefMessageDataService properties) : EncodingService<Encoding.Services.PrepareNdefMessageDataService>(properties)
    {
        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingFragmentTemplateContent.FragmentTemplateProperty[]? templateProperties, EncodingAction currentAction)
        {
            var msg = new NdefMessage();
            foreach (var r in Properties.Records)
            {
                if (r is Encoding.Services.Ndef.TextRecord tr)
                {
                    msg.addTextRecord(tr.Text, tr.Language);
                }
                else if (r is Encoding.Services.Ndef.MimeMediaRecord mr)
                {
                    msg.addMimeMediaRecord(mr.MimeType, mr.Payload?.ToByteVector());
                }
                else if (r is Encoding.Services.Ndef.UriRecord ur)
                {
                    msg.addUriRecord(ur.Uri, (UriType)ur.Prefixe);
                }
            }

            var data = msg.encode();
            HandleBuffer(cardCtx, [.. data]);
        }
    }
}
