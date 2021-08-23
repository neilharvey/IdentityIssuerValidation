using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace IdentityIssuerValidation.Tests
{
    public class IssuerTokenValidatorTests
    {
        [Fact]
        public void Validate_AcceptedTenantsContainsIssuer_ReturnsIssuer()
        {
            var issuer = "https://login.microsoftonline.com/c68d7a1e-25b6-4d6e-8a6d-323014b698cf";

            var options = new IssuerValidationOptions()
            {
                AcceptedTenants = new List<string>() { "c68d7a1e-25b6-4d6e-8a6d-323014b698cf" }
            };

            var validator = GetValidator(options);

            var result = validator.Validate(issuer);

            Assert.Equal(issuer, result);
        }

        [Fact]
        public void Validate_AcceptedTenantsDoesNotContainIssuer_ThrowsSecurityTokenException()
        {
            var issuer = "https://login.microsoftonline.com/5109990c-50be-4e49-a3ee-9ee7a82bcc54";

            var options = new IssuerValidationOptions()
            {
                AcceptedTenants = new List<string>()
                {
                    "e222f529-bc31-49c4-8190-3c929dbfdfe7",
                    "193675ae-f0c6-4ad3-b825-ffad0cd3302e"
                }
            };

            var validator = GetValidator(options);

            Assert.Throws<SecurityTokenInvalidIssuerException>(() => validator.Validate(issuer));
        }

        // An alternative implementation might wish to treat missing configuration as allow all.
        [Fact]
        public void Validate_NullTenants_ThrowsSecurityTokenException()
        {
            var options = new IssuerValidationOptions()
            {
                AcceptedTenants = null
            };

            var validator = GetValidator(options);

            Assert.Throws<SecurityTokenInvalidIssuerException>(() => validator.Validate("Anything"));
        }

        private static IssuerTokenValidator GetValidator(IssuerValidationOptions options)
        {
            var optionsMonitor = new Mock<IOptionsMonitor<IssuerValidationOptions>>();
            optionsMonitor.Setup(x => x.CurrentValue).Returns(options);
            var validator = new IssuerTokenValidator(optionsMonitor.Object);
            return validator;
        }
    }
}
