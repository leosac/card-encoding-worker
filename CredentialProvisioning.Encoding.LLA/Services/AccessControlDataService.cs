using Leosac.CredentialProvisioning.Encoding.Key;
using Leosac.CredentialProvisioning.Encoding.Services;
using Leosac.CredentialProvisioning.Encoding.Services.AccessControl;

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

            var llaFormat = new LibLogicalAccess.CustomFormat();
            llaFormat.setName(Properties.FormatDefinition.Name);
            var llaFields = new List<LibLogicalAccess.DataField>();
            uint position = 0;
            foreach (var field in Properties.FormatDefinition.Fields)
            {
                if (field is NumberDataField nf)
                {
                    var llaField = new LibLogicalAccess.NumberDataField();
                    llaField.setValue(nf.Value);
                    SetValueDataFieldProperties(nf, llaField, ref position);
                    llaFields.Add(llaField);
                }
                else if (field is StringDataField sf)
                {
                    var llaField = new LibLogicalAccess.StringDataField();
                    llaField.setCharset(sf.Charset);
                    llaField.setPaddingChar(sf.PaddingChar);
                    llaField.setValue(sf.Value);
                    SetValueDataFieldProperties(sf, llaField, ref position);
                    llaFields.Add(llaField);
                }
                else if (field is BinaryDataField bf)
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
                else if (field is ParityDataField pf)
                {
                    var llaField = new LibLogicalAccess.ParityDataField();
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

        private void SetValueDataFieldProperties(ValueDataField field, LibLogicalAccess.ValueDataField llaField, ref uint position)
        {
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
