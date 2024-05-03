using Leosac.CredentialProvisioning.Encoding.Key;
using Leosac.CredentialProvisioning.Encoding.Services;
using Leosac.CredentialProvisioning.Encoding.Services.AccessControl;
using Leosac.CredentialProvisioning.Encoding.Services.AccessControl.Fields;
using Leosac.CredentialProvisioning.Encoding.Services.AccessControl.Formats;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Services
{
    public abstract class AccessControlDataService<T>(T properties) : EncodingService<T>(properties) where T: AccessControlDataService, new()
    {
        public override void Run(CardContext cardCtx, KeyProvider? keystore, EncodingAction currentAction)
        {
            var format = GetFormat() ?? throw new EncodingException("Cannot parse the access control format.");
            Run(cardCtx, keystore, format);
        }

        private LibLogicalAccess.Format? GetFormat()
        {
            if (!string.IsNullOrEmpty(Properties.Format))
            {
                var cfc = new LibLogicalAccess.CardsFormatComposite();
                return cfc.createFormatFromXml(Properties.Format, string.Empty);
            }
            if (Properties.FormatDefinition == null)
            {
                return null;
            }

            if (Properties.FormatDefinition is Wiegand26 w26)
            {
                var llaFormat = new LibLogicalAccess.Wiegand26Format();
                llaFormat.setFacilityCode(w26.FacilityCode);
                return llaFormat;
            }
            else if (Properties.FormatDefinition is Wiegand34)
            {
                return new LibLogicalAccess.Wiegand34Format();
            }
            else if (Properties.FormatDefinition is Wiegand34WithFacility w34)
            {
                var llaFormat = new LibLogicalAccess.Wiegand34WithFacilityFormat();
                llaFormat.setFacilityCode(w34.FacilityCode);
                return llaFormat;
            }
            else if (Properties.FormatDefinition is Wiegand35 w35)
            {
                var llaFormat = new LibLogicalAccess.Wiegand35Format();
                llaFormat.setCompanyCode(w35.CompanyCode);
                return llaFormat;
            }
            else if (Properties.FormatDefinition is Wiegand37)
            {
                return new LibLogicalAccess.Wiegand37Format();
            }
            else if (Properties.FormatDefinition is Wiegand37WithFacility w37)
            {
                var llaFormat = new LibLogicalAccess.Wiegand37WithFacilityFormat();
                llaFormat.setFacilityCode(w37.FacilityCode);
                return llaFormat;
            }
            else if (Properties.FormatDefinition is Binary32)
            {
                var llaFormat = new LibLogicalAccess.CustomFormat();
                llaFormat.setName("32-bit Format");
                var llaField = new LibLogicalAccess.NumberDataField();
                llaField.setName("Uid");
                llaField.setValue(0);
                llaField.setPosition(0);
                llaField.setDataLength(32);
                llaField.setDataRepresentation(new LibLogicalAccess.BigEndianDataRepresentation());
                llaField.setDataType(new LibLogicalAccess.BinaryDataType());
                llaField.setIsFixedField(false);
                llaField.setIsIdentifier(true);
                llaFormat.setFieldList(new LibLogicalAccess.DataFieldVector([llaField]));
                return llaFormat;
            }
            else if (Properties.FormatDefinition is Binary56)
            {
                var llaFormat = new LibLogicalAccess.CustomFormat();
                llaFormat.setName("56-bit Format");
                var llaField = new LibLogicalAccess.NumberDataField();
                llaField.setName("Uid");
                llaField.setValue(0);
                llaField.setPosition(0);
                llaField.setDataLength(56);
                llaField.setDataRepresentation(new LibLogicalAccess.BigEndianDataRepresentation());
                llaField.setDataType(new LibLogicalAccess.BinaryDataType());
                llaField.setIsFixedField(false);
                llaField.setIsIdentifier(true);
                llaFormat.setFieldList(new LibLogicalAccess.DataFieldVector([llaField]));
                return llaFormat;
            }
            else if (Properties.FormatDefinition is Binary64)
            {
                var llaFormat = new LibLogicalAccess.CustomFormat();
                llaFormat.setName("64-bit Format");
                var llaField = new LibLogicalAccess.NumberDataField();
                llaField.setName("Uid");
                llaField.setValue(0);
                llaField.setPosition(0);
                llaField.setDataLength(64);
                llaField.setDataRepresentation(new LibLogicalAccess.BigEndianDataRepresentation());
                llaField.setDataType(new LibLogicalAccess.BinaryDataType());
                llaField.setIsFixedField(false);
                llaField.setIsIdentifier(true);
                llaFormat.setFieldList(new LibLogicalAccess.DataFieldVector([llaField]));
                return llaFormat;
            }
            else if (Properties.FormatDefinition is Custom c)
            {
                var llaFormat = new LibLogicalAccess.CustomFormat();
                llaFormat.setName(c.Name);
                var llaFields = new List<LibLogicalAccess.DataField>();
                uint position = 0;
                foreach (var field in c.Fields)
                {
                    if (field is Number nf)
                    {
                        var llaField = new LibLogicalAccess.NumberDataField();
                        llaField.setValue(nf.Value);
                        SetValueDataFieldProperties(nf, llaField, ref position);
                        llaFields.Add(llaField);
                    }
                    else if (field is Encoding.Services.AccessControl.Fields.String sf)
                    {
                        var llaField = new LibLogicalAccess.StringDataField();
                        llaField.setCharset(sf.Charset);
                        llaField.setPaddingChar(sf.PaddingChar);
                        llaField.setValue(sf.Value);
                        SetValueDataFieldProperties(sf, llaField, ref position);
                        llaFields.Add(llaField);
                    }
                    else if (field is Binary bf)
                    {
                        var llaField = new LibLogicalAccess.BinaryDataField();
                        llaField.setPaddingChar(bf.PaddingChar);
                        if (!string.IsNullOrEmpty(bf.Value))
                        {
                            llaField.setValue(new LibLogicalAccess.ByteVector(Convert.FromHexString(bf.Value)));
                        }
                        SetValueDataFieldProperties(bf, llaField, ref position);
                        llaFields.Add(llaField);
                    }
                    else if (field is Parity pf)
                    {
                        var llaField = new LibLogicalAccess.ParityDataField();
                        SetDataFieldProperties(pf, llaField);
                        llaField.setParityType((LibLogicalAccess.ParityType)pf.ParityType);
                        llaField.setPosition(position++);
                        llaFields.Add(llaField);
                    }
                    else
                    {
                        throw new EncodingException("Unknown data field type.");
                    }
                }
                llaFormat.setFieldList(new LibLogicalAccess.DataFieldVector(llaFields));
                return llaFormat;
            }

            return null;
        }

        private void SetDataFieldProperties(AccessControlDataField field, LibLogicalAccess.DataField llaField)
        {
            llaField.setName(field.Name);
        }

        private void SetValueDataFieldProperties(ValueDataField field, LibLogicalAccess.ValueDataField llaField, ref uint position)
        {
            SetDataFieldProperties(field, llaField);
            switch (field.DataRepresentation)
            {
                case FieldDataRepresentation.BigEndian:
                    llaField.setDataRepresentation(new LibLogicalAccess.BigEndianDataRepresentation());
                    break;
                case FieldDataRepresentation.LittleEndian:
                    llaField.setDataRepresentation(new LibLogicalAccess.LittleEndianDataRepresentation());
                    break;
                case FieldDataRepresentation.None:
                    llaField.setDataRepresentation(new LibLogicalAccess.NoDataRepresentation());
                    break;
                default:
                    throw new EncodingException("Unknown field data representation.");
            }

            switch (field.DataType)
            {
                case FieldDataType.BcdByte:
                    llaField.setDataType(new LibLogicalAccess.BCDByteDataType());
                    break;
                case FieldDataType.BcdNibble:
                    llaField.setDataType(new LibLogicalAccess.BCDNibbleDataType());
                    break;
                case FieldDataType.Binary:
                    llaField.setDataType(new LibLogicalAccess.BinaryDataType());
                    break;
                default:
                    throw new EncodingException("Unknown field data type.");
            }
            llaField.setDataLength(field.Length);
            llaField.setPosition(position);
            llaField.setIsFixedField(field.IsFixedField);
            llaField.setIsIdentifier(field.IsIdentifier);
            position += field.Length;
        }

        protected abstract void Run(CardContext cardCtx, KeyProvider? keystore, LibLogicalAccess.Format format);
    }
}
