using CommandLine;
using Microsoft.Extensions.Options;

namespace Leosac.CredentialProvisioning.Encoding.Worker.LLAServer
{
    public class OptionsSetup : IConfigureOptions<Options>
    {
        private const string SectionName = "Options";
        private readonly IConfiguration _configuration;
        private readonly string[]? _args;

        public OptionsSetup(IConfiguration configuration, string[]? args = null)
        {
            _configuration = configuration;
            _args = args;
        }


        public void Configure(Options options)
        {
            var section = _configuration.GetSection(SectionName);
            if (section.Exists())
                section.Bind(options);

            if (_args != null)
            {
                // Override options from config file by options from command lines
                Parser.Default.ParseArguments(() => { return options; }, _args);
            }
        }
    }
}
