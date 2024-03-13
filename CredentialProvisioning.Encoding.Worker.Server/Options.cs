using Leosac.CredentialProvisioning.Server.Shared;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class Options
    {
        public string? TemplateRepository { get; set; }

        public string? KeyStore { get; set; }

        public bool? ManagementApi { get; set; }

        public string? APIKey { get; set; }

        public string? DataIntegrityKey { get; set; }

        public bool? EnableSwagger { get; set; }

        public ReaderType ReaderType { get; set; } = ReaderType.Remote;

        public string ContactlessReader { get; set; } = string.Empty;

        public string SAMReader { get; set; } = "SAM";

        public JwtSettings JWT { get; set; } = new();
    }
}
