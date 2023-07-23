using CommandLine;
using Leosac.CredentialProvisioning.Server.Shared;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class Options
    {
        [Option('r', "templateRepository", Required = false, HelpText = "Folder where template files are located")]
        public string? TemplateRepository { get; set; }

        [Option('s', "keyStore", Required = false, HelpText = "File key store to load")]
        public string? KeyStore { get; set; }

        [Option('m', "managementApi", Required = false, HelpText = "Enable Worker Management API")]
        public bool? ManagementApi { get; set; }

        [Option('k', "apiKey", Required = false, Default = null, HelpText = "API key. Undefined means unsecure API calls.")]
        public string? APIKey { get; set; }

        [Option('i', "dataIntegrityKey", Required = false, Default = null, HelpText = "The public key for data integrity verification. Undefined means data integrity is not checked.")]
        public string? DataIntegrityKey { get; set; }

        public JwtSettings? JWT { get; set; }
    }
}
