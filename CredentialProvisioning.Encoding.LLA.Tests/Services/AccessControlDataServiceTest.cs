using LibLogicalAccess;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Tests.Services
{
    [TestClass]
    public class AccessControlDataServiceTest
    {
        [TestMethod]
        [Ignore]
        public void AN10957Format()
        {
            var xml = "<CustomFormat type=\"16\"><Name>CA</Name><Fields><BinaryDataField><Name>MajorVersion</Name><Position>0</Position><IsFixedField>true</IsFixedField><IsIdentifier>false</IsIdentifier><DataRepresentation>4</DataRepresentation><DataType>3</DataType><Length>8</Length><Value>01</Value><Padding>0</Padding></BinaryDataField><BinaryDataField><Name>MinorVersion</Name><Position>8</Position><IsFixedField>true</IsFixedField><IsIdentifier>false</IsIdentifier><DataRepresentation>4</DataRepresentation><DataType>3</DataType><Length>8</Length><Value>00</Value><Padding>0</Padding></BinaryDataField><NumberDataField><Name>FacilityCode</Name><Position>16</Position><IsFixedField>true</IsFixedField><IsIdentifier>false</IsIdentifier><DataRepresentation>5</DataRepresentation><DataType>2</DataType><Length>40</Length><Value>0</Value></NumberDataField><NumberDataField><Name>PACSNumber</Name><Position>56</Position><IsFixedField>false</IsFixedField><IsIdentifier>false</IsIdentifier><DataRepresentation>5</DataRepresentation><DataType>2</DataType><Length>64</Length><Value>0</Value></NumberDataField><NumberDataField><Name>ReissueCode</Name><Position>120</Position><IsFixedField>true</IsFixedField><IsIdentifier>false</IsIdentifier><DataRepresentation>5</DataRepresentation><DataType>2</DataType><Length>8</Length><Value>0</Value></NumberDataField><NumberDataField><Name>PINCode</Name><Position>128</Position><IsFixedField>true</IsFixedField><IsIdentifier>false</IsIdentifier><DataRepresentation>5</DataRepresentation><DataType>2</DataType><Length>32</Length><Value>0</Value></NumberDataField><NumberDataField><Name>Uid</Name><Position>160</Position><IsFixedField>false</IsFixedField><IsIdentifier>true</IsIdentifier><DataRepresentation>4</DataRepresentation><DataType>3</DataType><Length>64</Length><Value>0</Value></NumberDataField><BinaryDataField><Name>UidPad</Name><Position>224</Position><IsFixedField>true</IsFixedField><IsIdentifier>false</IsIdentifier><DataRepresentation>4</DataRepresentation><DataType>3</DataType><Length>96</Length><Value/><Padding>0</Padding></BinaryDataField></Fields></CustomFormat>";
            var format = new CustomFormat();
            format.unSerialize(xml, string.Empty);

            var pacsNumberField = format.getFieldFromName("PACSNumber") as NumberDataField;
            pacsNumberField.setValue(1000);

            var uidField = format.getFieldFromName("Uid") as NumberDataField;
            uidField.setValue(1000);

            var data = format.getLinearData();
            Assert.IsNotNull(data);
        }
    }
}