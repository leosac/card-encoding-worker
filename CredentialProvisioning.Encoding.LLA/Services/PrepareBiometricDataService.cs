using Leosac.CredentialProvisioning.Encoding.Key;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class PrepareBiometricDataService(Encoding.Services.PrepareBiometricDataService properties) : EncodingService<Encoding.Services.PrepareBiometricDataService>(properties)
    {
        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingFragmentTemplateContent.FragmentTemplateProperty[]? templateProperties, EncodingAction currentAction)
        {
            var cardId = string.Empty;
            var userName = string.Empty;
            var firstTemplate = new byte[0];
            var secondTemplate = new byte[0];
            var duressTemplate = new byte[0];

            if (!string.IsNullOrEmpty(Properties.CardIdField))
            {
                cardId = cardCtx.GetFieldValue(Properties.CardIdField)?.ToString() ?? string.Empty;
            }
            if (!string.IsNullOrEmpty(Properties.UsernameField))
            {
                userName = cardCtx.GetFieldValue(Properties.UsernameField)?.ToString() ?? string.Empty;
            }
            if (!string.IsNullOrEmpty(Properties.Finger1Field))
            {
                firstTemplate = cardCtx.GetBinaryFieldValue(Properties.Finger1Field);
            }
            if (!string.IsNullOrEmpty(Properties.Finger2Field))
            {
                secondTemplate = cardCtx.GetBinaryFieldValue(Properties.Finger2Field);
            }
            if (!string.IsNullOrEmpty(Properties.DuressFingerField))
            {
                duressTemplate = cardCtx.GetBinaryFieldValue(Properties.DuressFingerField);
            }

            var buf = new List<byte>();
            if (Properties.Product == Encoding.Services.PrepareBiometricDataService.BiometricProduct.MorphoAccess || Properties.Product == Encoding.Services.PrepareBiometricDataService.BiometricProduct.MorphoAccessSIGMA)
            {
                // Format to Sagem Contactless card buffer
                // NEED TO RESPECT THE TLV SAGEM FORMAT

                if (Properties.Product == Encoding.Services.PrepareBiometricDataService.BiometricProduct.MorphoAccessSIGMA)
                {
                    // Version field (?) = TL + 1 byte
                    var verBuf = new byte[4];
                    verBuf[0] = 0x35; // T
                    verBuf[1] = 0x01; // L
                    verBuf[2] = 0x00; // L
                    verBuf[3] = 0x02; // V
                    buf.AddRange(verBuf);
                }

                // Id field = TL + 24 bytes
                var idBuf = new byte[27];
                idBuf[0] = 0x32; // T
                idBuf[1] = 0x18; // L
                idBuf[2] = 0x00; // L
                for (var i = 0; i < cardId.Length; ++i)
                {
                    idBuf[i + 3] = (byte)cardId[i]; // V
                }
                buf.AddRange(idBuf);

                // Name field = TL + 20 bytes
                var nameBuf = new byte[23];
                nameBuf[0] = 0x20; // T
                nameBuf[1] = 0x14; // L
                nameBuf[2] = 0x00; // L
                for (var i = 0; i < userName.Length; ++i)
                {
                    nameBuf[i + 3] = (byte)userName[i]; // V
                }
                buf.AddRange(nameBuf);

                if (Properties.Format == Encoding.Services.PrepareBiometricDataService.TemplateFormat.Morpho_CFV)
                {
                    if (firstTemplate != null && firstTemplate.Length > 0)
                    {
                        // Template 1 field = TL + 170 bytes
                        buf.Add(0x30); // T
                        buf.Add(0xAA); // L
                        buf.Add(0x00); // L
                        buf.AddRange(firstTemplate); // V
                    }

                    if (secondTemplate != null && secondTemplate.Length > 0)
                    {
                        // Template 2 field = TL + 170 bytes
                        buf.Add(0x31); // T
                        buf.Add(0xAA); // L
                        buf.Add(0x00); // L
                        buf.AddRange(secondTemplate); // V
                    }
                }
                else
                {
                    firstTemplate = FormatTemplateData(firstTemplate);
                    secondTemplate = FormatTemplateData(secondTemplate);
                    duressTemplate = FormatTemplateData(duressTemplate);

                    if (firstTemplate != null && firstTemplate.Length > 0)
                    {
                        // Template 1 field = TL + template length
                        buf.Add(0x08); // T
                        buf.Add((byte)firstTemplate.Length); // L
                        buf.Add((byte)((firstTemplate.Length & 0xff00) >> 8)); // L
                        buf.AddRange(firstTemplate); // V
                    }

                    if (!ArrayIsEmpty(secondTemplate))
                    {
                        // Template 2 field = TL + template length
                        buf.Add(0x08); // T
                        buf.Add((byte)secondTemplate.Length); // L
                        buf.Add((byte)((secondTemplate.Length & 0xff00) >> 8)); // L
                        buf.AddRange(secondTemplate); // V
                    }

                    if (!ArrayIsEmpty(duressTemplate))
                    {
                        // Duress Template field = TL + template length
                        buf.Add(0x38); // T
                        buf.Add((byte)duressTemplate.Length); // L
                        buf.Add((byte)((duressTemplate.Length & 0xff00) >> 8)); // L
                        buf.AddRange(duressTemplate); // V
                    }
                }
            }
        }

        private bool ArrayIsEmpty(byte[] finger, byte emptyByte = 0x00)
        {
            if (finger != null && finger.Length > 0)
            {
                for (int i = 0; i < finger.Length; ++i)
                {
                    if (finger[i] != emptyByte)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private byte[]? FormatTemplateData(byte[] template)
        {
            if (template != null && template.Length > 0)
            {
                var typedtpl = new byte[template.Length + 3];
                typedtpl[0] = (byte)Properties.Format;
                typedtpl[1] = (byte)template.Length;
                typedtpl[2] = (byte)((template.Length & 0xff00) >> 8);
                Array.Copy(template, 0, typedtpl, 3, template.Length);
                template = typedtpl;
            }

            return template;
        }
    }
}
