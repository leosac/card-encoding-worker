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

        [Option('r', "readerType", Required = false, Default = ReaderType.Remote, HelpText = "Type of reader technology. Remote = use readers from a remote client over WebSocket / Local = use local PC/SC reader resources.")]
        public ReaderType ReaderType { get; set; } = ReaderType.Remote;

        [Option('c', "contactlessReader", Required = false, Default = "", HelpText = "The contactless reader alias/name.")]
        public string ContactlessReader { get; set; } = string.Empty;

        [Option('a', "samReader", Required = false, Default = "SAM", HelpText = "The SAM reader alias/name.")]
        public string SAMReader { get; set; } = "SAM";

        public JwtSettings? JWT { get; set; }
    }
}
