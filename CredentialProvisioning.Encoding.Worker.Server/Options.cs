using CommandLine;
using Leosac.CredentialProvisioning.Server.Shared;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class Options
    {
        [Option('r', "Template Repository", Required = false, HelpText = "Folder where template files are located")]
        public string? TemplateRepository { get; set; }

        [Option('m', "managementApi", Required = false, HelpText = "Enable Worker Management API")]
        public bool? ManagementApi { get; set; }

        [Option('k', "apiKey", Required = false, Default = null, HelpText = "API key. Undefined means unsecure API calls.")]
        public string? APIKey { get; set; }

        public JwtSettings? JWT { get; set; }
    }
}
