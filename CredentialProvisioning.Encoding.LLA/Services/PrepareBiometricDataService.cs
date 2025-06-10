using Leosac.CredentialProvisioning.Encoding.Key;
using System.Security.Cryptography;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public class PrepareBiometricDataService(Encoding.Services.PrepareBiometricDataService properties) : EncodingService<Encoding.Services.PrepareBiometricDataService>(properties)
    {
        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingFragmentTemplateContent.FragmentTemplateProperty[]? templateProperties, EncodingAction currentAction)
        {
            var cardId = string.Empty;
            var userName = string.Empty;
            if (Properties.Templates == null)
            {
                Properties.Templates = [];
            }
            var templates = new byte[Properties.Templates.Length][];
            var duressTemplate = new byte[0];

            if (!string.IsNullOrEmpty(Properties.CardIdField))
            {
                cardId = cardCtx.GetFieldValue(Properties.CardIdField)?.ToString() ?? string.Empty;
            }
            if (!string.IsNullOrEmpty(Properties.UsernameField))
            {
                userName = cardCtx.GetFieldValue(Properties.UsernameField)?.ToString() ?? string.Empty;
            }
            for (int i = 0; i < templates.Length; ++i)
            {
                var tplField = Properties.Templates[i];
                if (string.IsNullOrEmpty(tplField))
                {
                    tplField = "BioData_" + i.ToString();
                }
                templates[i] = cardCtx.GetBinaryFieldValue(tplField) ?? [];
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

                byte[]? firstTemplate = templates.Length >= 1 ? templates[0] : null;
                byte[]? secondTemplate = templates.Length >= 2 ? templates[1] : null;
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
                        buf.Add((byte)secondTemplate!.Length); // L
                        buf.Add((byte)((secondTemplate.Length & 0xff00) >> 8)); // L
                        buf.AddRange(secondTemplate); // V
                    }

                    if (!ArrayIsEmpty(duressTemplate))
                    {
                        // Duress Template field = TL + template length
                        buf.Add(0x38); // T
                        buf.Add((byte)duressTemplate!.Length); // L
                        buf.Add((byte)((duressTemplate.Length & 0xff00) >> 8)); // L
                        buf.AddRange(duressTemplate); // V
                    }
                }
            }
            else if (Properties.Product == Encoding.Services.PrepareBiometricDataService.BiometricProduct.STid)
            {
                int totallen = 1;
                bool exempt = false;
                if (!string.IsNullOrEmpty(Properties.FingerExemptionField))
                {
                    var bstr = cardCtx.GetFieldValue(Properties.FingerExemptionField)?.ToString()?.ToLowerInvariant();
                    exempt = (!string.IsNullOrEmpty(bstr) && (bstr == "1" || bstr == "true" || bstr == "y" || bstr == "o" || bstr == "yes" || bstr == "oui"));
                }
                
                if (exempt)
                {
                    var csn = cardCtx.GetFieldValue("CSN")?.ToString();
                    if (!string.IsNullOrEmpty(csn))
                    {
                        var tpl = CalculateExemptionHash(csn);
                        templates = [tpl];
                    }
                }
                for (int i = 0; i < templates.Length; ++i)
                {
                    totallen += templates[i].Length + 1;
                }

                buf.Add((byte)(totallen >> 8));
                buf.Add((byte)totallen);
                buf.Add((byte)templates.Length);
                foreach (var tpl in templates)
                {
                    buf.Add((byte)tpl.Length);
                    buf.AddRange(tpl);
                }
            }
            else
            {
                throw new EncodingException("Unknown Biometric product.");
            }

            HandleBuffer(cardCtx, [.. buf]);
        }

        private bool ArrayIsEmpty(byte[]? finger, byte emptyByte = 0x00)
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

        private byte[]? FormatTemplateData(byte[]? template)
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

        private byte[] CalculateExemptionHash(string csn)
        {
            var sha = SHA256.Create();
            var salt = new byte[] { 0xA4, 0xB7, 0x78, 0xD5, 0x36, 0xE5, 0x44, 0x57, 0xAB, 0x31, 0xE0, 0x15, 0x53, 0x09, 0x46, 0xFA };
            var bcsn = Convert.FromHexString(csn);
            var data = new byte[salt.Length + bcsn.Length];
            Array.Copy(salt, 0, data, 0, salt.Length);
            Array.Copy(bcsn, 0, data, salt.Length, bcsn.Length);
            return sha.ComputeHash(data);
        }
    }
}
