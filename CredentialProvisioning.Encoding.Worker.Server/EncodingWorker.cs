using Leosac.CredentialProvisioning.Core.Contexts;
using Leosac.CredentialProvisioning.Core.Models;
using Leosac.CredentialProvisioning.Encoding.Key;
using Leosac.CredentialProvisioning.Encoding.LLA;
using Leosac.CredentialProvisioning.Worker;
using Leosac.ServerHelper;
using LibLogicalAccess;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public class EncodingWorker(ILogger<EncodingWorker> logger, KeyProvider keystore) : WorkerBase<EncodingFragmentTemplateContent>()
    {
        protected readonly ILogger<EncodingWorker> _logger = logger;
        protected JwtService? _completionJwtService = null;

        public KeyProvider KeyStore { get; private set; } = keystore;

        protected override CredentialContext<EncodingFragmentTemplateContent> CreateCredentialContext(string templateId, EncodingFragmentTemplateContent template, IList<WorkerCredentialBase> credentials)
        {
            return new EncodingContext() { TemplateId = templateId, TemplateContent = template, Keys = KeyStore, Credentials = [.. credentials] };
        }

        protected override CredentialProcess<EncodingFragmentTemplateContent> CreateProcess()
        {
            return new EncodingProcess(typeof(LLADeviceContext).Assembly, _logger);
        }

        public void SetupCompletionTokenJWT(JwtSettings jwt)
        {
            _completionJwtService = new JwtService(jwt);
        }

        protected override string? GenerateCompletionToken(IDictionary<string, object> data)
        {
            if (_completionJwtService == null)
            {
                return null;
            }

            var claims = new List<System.Security.Claims.Claim>(_completionJwtService.CreateBaseClaims());
            if (data != null)
            {
                foreach (var k in data.Keys)
                {
                    if (data[k] != null)
                    {
                        claims.Add(new System.Security.Claims.Claim(k, data[k].ToString()!));
                    }
                }
            }
            return _completionJwtService.CreateToken([.. claims]);
        }
    }
}
