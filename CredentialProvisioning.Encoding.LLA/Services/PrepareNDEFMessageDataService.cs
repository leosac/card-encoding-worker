using Leosac.CredentialProvisioning.Encoding.Key;
using LibLogicalAccess;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class PrepareNdefMessageDataService(Encoding.Services.PrepareNdefMessageDataService properties) : EncodingService<Encoding.Services.PrepareNdefMessageDataService>(properties)
    {
        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingFragmentTemplateContent.FragmentTemplateProperty[]? templateProperties, EncodingAction currentAction)
        {
            var msg = new NdefMessage();
            foreach (var r in Properties.Records)
            {
                string? fieldName = null;
                if (!string.IsNullOrEmpty(r.FromField))
                {
                    fieldName = GetCredentialFieldName(r.FromField);
                }

                if (r is Encoding.Services.Ndef.TextRecord tr)
                {
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        var v = cardCtx.GetFieldValue(fieldName)?.ToString();
                        msg.addTextRecord(v ?? string.Empty, tr.Language);
                    }
                    else
                    {
                        msg.addTextRecord(tr.Text, tr.Language);
                    }
                }
                else if (r is Encoding.Services.Ndef.MimeMediaRecord mr)
                {
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        msg.addMimeMediaRecord(mr.MimeType, [.. cardCtx.GetBinaryFieldValue(fieldName) ?? []]);
                    }
                    else
                    {
                        msg.addMimeMediaRecord(mr.MimeType, mr.Payload?.ToByteVector());
                    }
                }
                else if (r is Encoding.Services.Ndef.UriRecord ur)
                {
                    if (!string.IsNullOrEmpty(fieldName))
                    {
                        var v = cardCtx.GetFieldValue(fieldName)?.ToString();
                        msg.addUriRecord(v ?? string.Empty, (UriType)ur.Prefixe);
                    }
                    else
                    {
                        msg.addUriRecord(ur.Uri, (UriType)ur.Prefixe);
                    }
                }
            }

            var data = msg.encode();
            HandleBuffer(cardCtx, [.. data]);
        }
    }
}
