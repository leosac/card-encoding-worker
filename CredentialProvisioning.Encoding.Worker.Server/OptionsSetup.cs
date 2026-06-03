using Microsoft.Extensions.Options;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class OptionsSetup : IConfigureOptions<Options>
    {
        private const string SectionName = "Options";
        private readonly IConfiguration _configuration;

        public OptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void Configure(Options options)
        {
            var section = _configuration.GetSection(SectionName);
            if (section.Exists())
                section.Bind(options);
        }
    }
}
