using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;

namespace IdentityIssuerValidation
{
    public class IssuerTokenValidator : IIssuerTokenValidator
    {
        private readonly IOptionsMonitor<IssuerValidationOptions> _options;

        public IssuerTokenValidator(IOptionsMonitor<IssuerValidationOptions> options)
        {
            _options = options;
        }

        public string Validate(string issuer)
        {
            var acceptedTenants = _options.CurrentValue.AcceptedTenants ?? Enumerable.Empty<string>();
            var validIssuers = acceptedTenants.Select(tid => $"https://login.microsoftonline.com/{tid}");

            if (validIssuers.Contains(issuer))
            {
                return issuer;
            }
            else
            {
                throw new SecurityTokenInvalidIssuerException("The sign-in user's account does not belong to one of the tenants that this Web App accepts users from.")
                {
                    InvalidIssuer = issuer
                };
            }
        }
    }
}
