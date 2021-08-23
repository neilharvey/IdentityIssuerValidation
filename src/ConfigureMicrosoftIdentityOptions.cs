using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;

namespace IdentityIssuerValidation
{
    public class ConfigureMicrosoftIdentityOptions : IConfigureOptions<MicrosoftIdentityOptions>
    {
        private readonly IIssuerTokenValidator _issuerTokenValidator;

        public ConfigureMicrosoftIdentityOptions(IIssuerTokenValidator issuerTokenValidator)
        {
            _issuerTokenValidator = issuerTokenValidator;
        }

        public void Configure(MicrosoftIdentityOptions options)
        {
            options.TokenValidationParameters.IssuerValidator =
                (issuer, _, _) => _issuerTokenValidator.Validate(issuer);
        }
    }
}
