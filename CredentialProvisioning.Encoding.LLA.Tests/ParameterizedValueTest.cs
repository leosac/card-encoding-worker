using System.Text.Json;
using Leosac.CredentialProvisioning.Encoding.Chip.DESFire;

namespace Leosac.CredentialProvisioning.Encoding.LLA.Tests
{
    [TestClass]
    public class ParameterizedValueTest
    {
        [TestMethod]
        public void LegacyNumericValueDeserializes()
        {
            var action = JsonSerializer.Deserialize<CreateApplication>("{\"AID\":123}");

            Assert.IsNotNull(action);
            Assert.AreEqual((uint)123, (uint)action.AID);
        }

        [TestMethod]
        public void PlaceholderValueDeserializes()
        {
            var action = JsonSerializer.Deserialize<SelectApplication>("{\"AID\":\"${ApplicationId}\"}");

            Assert.IsNotNull(action);
            Assert.AreEqual("ApplicationId", action.AID.ParameterName);
            Assert.AreEqual((uint)42, action.AID.Resolve(new Dictionary<string, object?> { ["ApplicationId"] = 42 }));
        }

        [TestMethod]
        public void FileNumberPlaceholderDeserializes()
        {
            var action = JsonSerializer.Deserialize<DeleteFile>("{\"FileNo\":\"${FileNumber}\"}");

            Assert.IsNotNull(action);
            Assert.AreEqual((byte)7, action.FileNo.Resolve(new Dictionary<string, object?> { ["FileNumber"] = 7 }));
        }
    }
}
